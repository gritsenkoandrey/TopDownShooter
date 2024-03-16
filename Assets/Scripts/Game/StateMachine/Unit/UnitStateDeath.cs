using CodeBase.ECSCore;
using CodeBase.Game.Components;
using CodeBase.Game.Enums;
using CodeBase.Infrastructure.Factories.Effects;
using CodeBase.Infrastructure.Models;
using CodeBase.Utils;
using Cysharp.Threading.Tasks;
using VContainer;

namespace CodeBase.Game.StateMachine.Unit
{
    public sealed class UnitStateDeath : UnitState, IState
    {
        private IEffectFactory _effectFactory;
        private LevelModel _levelModel;
        private LootModel _lootModel;

        public UnitStateDeath(IStateMachine stateMachine, CUnit unit) : base(stateMachine, unit)
        {
        }

        [Inject]
        private void Construct(IEffectFactory effectFactory, LevelModel levelModel, LootModel lootModel)
        {
            _effectFactory = effectFactory;
            _levelModel = levelModel;
            _lootModel = lootModel;
        }

        public void Enter()
        {
            Unit.Agent.Agent.ResetPath();
            Unit.Radar.Clear.Execute();
            Unit.Animator.OnDeath.Execute();
            Unit.CleanSubscribe();
            
            _lootModel.GenerateEnemyLoot(Unit);
            _levelModel.RemoveEnemy(Unit);
            _effectFactory.CreateEffect(EffectType.Death, Unit.Position.AddY(Unit.Height)).Forget();
        }

        public void Exit() { }

        public void Tick() { }
    }
}