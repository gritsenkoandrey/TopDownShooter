using CodeBase.ECSCore;
using CodeBase.Game.Components;
using CodeBase.Game.Interfaces;
using CodeBase.Game.StateMachine.Unit;
using CodeBase.Infrastructure.Factories.Effects;
using CodeBase.Infrastructure.Models;
using CodeBase.Infrastructure.Progress;
using CodeBase.Utils;
using Cysharp.Threading.Tasks;
using UniRx;

namespace CodeBase.Game.Systems
{
    public sealed class SUnitDeath : SystemComponent<CUnit>
    {
        private readonly IProgressService _progressService;
        private readonly IEffectFactory _effectFactory;
        private readonly LevelModel _levelModel;

        public SUnitDeath(IProgressService progressService, IEffectFactory effectFactory, LevelModel levelModel)
        {
            _progressService = progressService;
            _effectFactory = effectFactory;
            _levelModel = levelModel;
        }
        protected override void OnEnableComponent(CUnit component)
        {
            base.OnEnableComponent(component);

            SubscribeOnDeathZombie(component);
        }

        private void SubscribeOnDeathZombie(CUnit component)
        {
            component.Health.CurrentHealth
                .SkipLatestValueOnSubscribe()
                .Where(_ => IsDeath(component))
                .First()
                .Subscribe(_ => Death(component))
                .AddTo(component.LifetimeDisposable);
        }

        private bool IsDeath(CUnit component) => !component.Health.IsAlive;

        private void Death(CUnit component)
        {
            component.StateMachine.StateMachine.Enter<UnitStateDeath>();

            _progressService.MoneyData.Data.Value += component.Money;
            _levelModel.RemoveEnemy(component);
            _effectFactory.CreateDeathFx(component.Position.AddY(component.Height)).Forget();
        }
    }
}