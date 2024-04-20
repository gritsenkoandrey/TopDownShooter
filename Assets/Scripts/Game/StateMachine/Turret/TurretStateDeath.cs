using CodeBase.ECSCore;
using CodeBase.Game.Components;
using CodeBase.Game.Enums;
using CodeBase.Infrastructure.Factories.Effects;
using CodeBase.Infrastructure.Loot;
using CodeBase.Infrastructure.Models;
using CodeBase.Utils;
using Cysharp.Threading.Tasks;
using VContainer;

namespace CodeBase.Game.StateMachine.Turret
{
    public sealed class TurretStateDeath : TurretState, IState
    {
        private IEffectFactory _effectFactory;
        private ILootService _lootService;
        private LevelModel _levelModel;

        public TurretStateDeath(IStateMachine stateMachine, CTurret turret) : base(stateMachine, turret)
        {
        }

        [Inject]
        private void Construct(IEffectFactory effectFactory, ILootService lootService, LevelModel levelModel)
        {
            _effectFactory = effectFactory;
            _lootService = lootService;
            _levelModel = levelModel;
        }

        public void Enter()
        {
            Turret.Radar.Clear.Execute();
            Turret.Weapon.SetActive(false);
            Turret.CleanSubscribe();
            
            _lootService.GenerateEnemyLoot(Turret);
            _levelModel.RemoveEnemy(Turret);
            _effectFactory.CreateEffect(EffectType.Death, Turret.Position.AddY(Turret.Height)).Forget();
        }

        public void Exit() { }

        public void Tick() { }
    }
}