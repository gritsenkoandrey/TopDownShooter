using CodeBase.ECSCore;
using CodeBase.Game.Components;
using CodeBase.Game.Enums;
using CodeBase.Infrastructure.Factories.Effects;
using CodeBase.Infrastructure.Loot;
using CodeBase.Infrastructure.Models;
using CodeBase.Utils;
using Cysharp.Threading.Tasks;
using VContainer;

namespace CodeBase.Game.StateMachine.Unit
{
    public sealed class UnitStateDeath : UnitState, IState
    {
        private IEffectFactory _effectFactory;
        private ILootService _lootService;
        private LevelModel _levelModel;

        public UnitStateDeath(IStateMachine stateMachine, CUnit unit) : base(stateMachine, unit)
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
            Unit.Agent.Agent.ResetPath();
            Unit.Animator.OnDeath.Execute();
            Unit.Shadow.SetActive(false);
            Unit.CleanSubscribe();
            
            _lootService.GenerateEnemyLoot(Unit);
            _levelModel.RemoveEnemy(Unit);
            _effectFactory.CreateEffect(EffectType.Death, Unit.Position.AddY(Unit.Height)).Forget();
            _effectFactory.CreateEffect(EffectType.Blood, Unit.Position).Forget();
        }

        public void Exit() { }

        public void Tick() { }
    }
}