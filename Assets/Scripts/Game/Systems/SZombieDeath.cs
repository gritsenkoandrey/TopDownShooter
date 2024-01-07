using CodeBase.ECSCore;
using CodeBase.Game.Components;
using CodeBase.Game.Interfaces;
using CodeBase.Game.StateMachine.Zombie;
using CodeBase.Infrastructure.Factories.Effects;
using CodeBase.Infrastructure.Models;
using CodeBase.Infrastructure.Progress;
using CodeBase.Utils;
using UniRx;

namespace CodeBase.Game.Systems
{
    public sealed class SZombieDeath : SystemComponent<CZombie>
    {
        private readonly IProgressService _progressService;
        private readonly IEffectFactory _effectFactory;
        private readonly LevelModel _levelModel;

        public SZombieDeath(IProgressService progressService, IEffectFactory effectFactory, LevelModel levelModel)
        {
            _progressService = progressService;
            _effectFactory = effectFactory;
            _levelModel = levelModel;
        }
        protected override void OnEnableComponent(CZombie component)
        {
            base.OnEnableComponent(component);

            SubscribeOnDeathZombie(component);
        }

        private void SubscribeOnDeathZombie(CZombie component)
        {
            component.Health.CurrentHealth
                .SkipLatestValueOnSubscribe()
                .Where(_ => IsDeath(component))
                .First()
                .Subscribe(_ => Death(component))
                .AddTo(component.LifetimeDisposable);
        }

        private bool IsDeath(IEnemy component) => !component.Health.IsAlive;

        private void Death(IEnemy component)
        {
            component.StateMachine.StateMachine.Enter<ZombieStateDeath>();

            _progressService.MoneyData.Data.Value += component.Stats.Money;
            _levelModel.RemoveEnemy(component);
            _effectFactory.CreateDeathFx(component.Position.AddY(1f));
        }
    }
}