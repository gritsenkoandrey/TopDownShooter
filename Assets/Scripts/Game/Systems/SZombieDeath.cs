using CodeBase.ECSCore;
using CodeBase.Game.Components;
using CodeBase.Game.StateMachine.Zombie;
using CodeBase.Infrastructure.Factories.Game;
using CodeBase.Infrastructure.Progress;
using CodeBase.Infrastructure.SaveLoad;
using CodeBase.Utils;
using UniRx;

namespace CodeBase.Game.Systems
{
    public sealed class SZombieDeath : SystemComponent<CZombie>
    {
        private readonly IGameFactory _gameFactory;
        private readonly IProgressService _progressService;
        private readonly ISaveLoadService _saveLoadService;

        public SZombieDeath(IGameFactory gameFactory, IProgressService progressService, ISaveLoadService saveLoadService)
        {
            _gameFactory = gameFactory;
            _progressService = progressService;
            _saveLoadService = saveLoadService;
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
                        _gameFactory.Enemies.Remove(component);
                        _gameFactory.CreateDeathFx(component.Position.AddY(1f));
                    }
                })
                .AddTo(component.LifetimeDisposable);
        }
    }
}