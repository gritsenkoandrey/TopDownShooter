using System;
using CodeBase.ECSCore;
using CodeBase.Game.ComponentsUi;
using CodeBase.Infrastructure.DailyTasks;
using CodeBase.Utils;
using UniRx;
using VContainer;

namespace CodeBase.Game.SystemsUi
{
    public sealed class STaskUpdate : SystemComponent<CTask>
    {
        private IDailyTaskService _dailyTaskService;

        [Inject]
        private void Construct(IDailyTaskService dailyTaskService)
        {
            _dailyTaskService = dailyTaskService;
        }
        
        protected override void OnEnableComponent(CTask component)
        {
            base.OnEnableComponent(component);

            component.Task
                .SkipLatestValueOnSubscribe()
                .First()
                .Subscribe(task =>
                {
                    SubscribeOnUpdateTask(component, task);
                })
                .AddTo(component.LifetimeDisposable);
        }

        private void SubscribeOnUpdateTask(CTask component, Task task)
        {
            task
                .ObserveEveryValueChanged(t => t.Score)
                .Subscribe(score =>
                {
                    if (score >= task.MaxScore && task.IsDone == false)
                    {
                        component.GetButton.interactable = true;
                    }
                    else
                    {
                        component.GetButton.interactable = false;
                    }

                    if (task.IsDone)
                    {
                        component.ButtonText.text = ButtonSettings.DoneText;
                        component.ButtonText.color = component.ColorDone;
                    }

                    component.ProgressText.text = Format(task);
                    component.FillProgress.fillAmount = Mathematics.Remap(0f, task.MaxScore, 0f, 1f, task.Score);
                })
                .AddTo(component.LifetimeDisposable);

            component.GetButton
                .OnClickAsObservable()
                .First()
                .Subscribe(_ =>
                {
                    component.GetButton.transform.PunchTransform();
                    component.GetButton.interactable = false;
                    component.ButtonText.text = ButtonSettings.DoneText;
                    component.ButtonText.color = component.ColorDone;

                    _dailyTaskService.Complete(task.Type);
                })
                .AddTo(component.LifetimeDisposable);
        }

        private string Format(Task task)
        {
            return task.Type == DailyTaskType.PlayMinutes ? 
                string.Format(FormatText.TaskProgress, Math.Clamp(task.Score, 0, task.MaxScore).SecondsToTime(), task.MaxScore.SecondsToTime()) : 
                string.Format(FormatText.TaskProgress, Math.Clamp(task.Score, 0, task.MaxScore).Trim(), task.MaxScore.Trim());
        }
    }
}