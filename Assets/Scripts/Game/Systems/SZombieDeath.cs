using CodeBase.ECSCore;
using CodeBase.Game.Components;
using CodeBase.Game.StateMachine.Zombie;
using CodeBase.Infrastructure.Factories.Effects;
using CodeBase.Infrastructure.Factories.Game;
using CodeBase.Infrastructure.Models;
using CodeBase.Infrastructure.Progress;
using CodeBase.Infrastructure.SaveLoad;
using CodeBase.Utils;
using UniRx;

namespace CodeBase.Game.Systems
{
    public sealed class SZombieDeath : SystemComponent<CZombie>
    {
        private readonly IProgressService _progressService;
        private readonly ISaveLoadService _saveLoadService;
        private readonly IEffectFactory _effectFactory;
        private readonly LevelModel _levelModel;

        public SZombieDeath(IProgressService progressService, ISaveLoadService saveLoadService, IEffectFactory effectFactory, LevelModel levelModel)
        {
            _progressService = progressService;
            _saveLoadService = saveLoadService;
            _effectFactory = effectFactory;
            _levelModel = levelModel;
        }
        protected override void OnEnableComponent(CZombie component)
        {
            base.OnEnableComponent(component);
            
            component.Health.CurrentHealth
                .SkipLatestValueOnSubscribe()
                .Subscribe(_ =>
                {
                    if (!component.Health.IsAlive)
                    {
                        component.StateMachine.StateMachine.Enter<ZombieStateDeath>();

                        _progressService.PlayerProgress.Money.Value += component.Stats.Money;
                        _saveLoadService.SaveProgress();
                        _levelModel.RemoveEnemy(component);
                        _effectFactory.CreateDeathFx(component.Position.AddY(1f));
                    }
                })
                .AddTo(component.LifetimeDisposable);
        }
    }
}