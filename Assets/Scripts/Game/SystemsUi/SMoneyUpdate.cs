using CodeBase.ECSCore;
using CodeBase.Game.ComponentsUi;
using CodeBase.Infrastructure.Progress;
using CodeBase.Utils;
using UniRx;
using VContainer;

namespace CodeBase.Game.SystemsUi
{
    public sealed class SMoneyUpdate : SystemComponent<CMoneyUpdate>
    {
        private IProgressService _progressService;

        [Inject]
        private void Construct(IProgressService progressService)
        {
            _progressService = progressService;
        }

        protected override void OnEnableComponent(CMoneyUpdate component)
        {
            base.OnEnableComponent(component);

            void SetMoneyText(int money) => component.TextCountMoney.text = money.Trim();

            _progressService.MoneyData.Data
                .Subscribe(SetMoneyText)
                .AddTo(component.LifetimeDisposable);
        }
    }
}