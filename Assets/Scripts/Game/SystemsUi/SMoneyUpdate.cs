using CodeBase.ECSCore;
using CodeBase.Game.ComponentsUi;
using CodeBase.Infrastructure.Progress;
using CodeBase.Infrastructure.Services;
using UniRx;

namespace CodeBase.Game.SystemsUi
{
    public sealed class SMoneyUpdate : SystemComponent<CMoneyUpdate>
    {
        private IProgressService _progressService;
        
        protected override void OnEnableSystem()
        {
            base.OnEnableSystem();

            _progressService = AllServices.Container.Single<IProgressService>();
        }

        protected override void OnDisableSystem()
        {
            base.OnDisableSystem();
        }

        protected override void OnTick()
        {
            base.OnTick();
        }

        protected override void OnEnableComponent(CMoneyUpdate component)
        {
            base.OnEnableComponent(component);

            _progressService.PlayerProgress.Money
                .Subscribe(value =>
                {
                    component.TextCountMoney.text = value.ToString();
                })
                .AddTo(component.LifetimeDisposable);
        }

        protected override void OnDisableComponent(CMoneyUpdate component)
        {
            base.OnDisableComponent(component);
        }
    }
}