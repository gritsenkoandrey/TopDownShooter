using CodeBase.ECSCore;
using CodeBase.Game.ComponentsUi;
using CodeBase.Infrastructure.Progress;
using CodeBase.Utils;
using UniRx;

namespace CodeBase.Game.SystemsUi
{
    public sealed class SMoneyUpdate : SystemComponent<CMoneyUpdate>
    {
        private readonly IProgressService _progressService;

        public SMoneyUpdate(IProgressService progressService)
        {
            _progressService = progressService;
        }

        protected override void OnEnableComponent(CMoneyUpdate component)
        {
            base.OnEnableComponent(component);

            _progressService.PlayerProgress.Money
                .Subscribe(value => component.TextCountMoney.text = value.Trim())
                .AddTo(component.LifetimeDisposable);
        }
    }
}