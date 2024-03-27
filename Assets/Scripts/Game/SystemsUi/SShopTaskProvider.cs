using System;
using CodeBase.ECSCore;
using CodeBase.Game.ComponentsUi;
using CodeBase.Infrastructure.DailyTasks;
using CodeBase.Utils;
using UniRx;
using VContainer;

namespace CodeBase.Game.SystemsUi
{
    public sealed class SShopTaskProvider : SystemComponent<CShopTaskProvider>
    {
        private IDailyTaskService _dailyTaskService;

        [Inject]
        private void Construct(IDailyTaskService dailyTaskService)
        {
            _dailyTaskService = dailyTaskService;
        }
        
        protected override void OnEnableComponent(CShopTaskProvider component)
        {
            base.OnEnableComponent(component);
            
            CreateTasks(component);
            UpdateTimeRemaining(component);
        }

        private void CreateTasks(CShopTaskProvider component)
        {
            _dailyTaskService.Create(component).Forget();
        }

        private void UpdateTimeRemaining(CShopTaskProvider component)
        {
            int time = _dailyTaskService.GetRemainingUpdateTime();

            Observable.Timer(TimeSpan.FromSeconds(1f))
                .DoOnSubscribe(() => component.Text.text = string.Format(FormatText.TaskTime, time.SecondsToTime()))
                .Repeat()
                .Where(_ => time > 0)
                .Subscribe(_ =>
                {
                    time--;
                    component.Text.text = string.Format(FormatText.TaskTime, time.SecondsToTime());

                    if (time == 0)
                    {
                        time = _dailyTaskService.GetRemainingUpdateTime();

                        CreateTasks(component);
                    }
                })
                .AddTo(component.LifetimeDisposable);
        }
    }
}