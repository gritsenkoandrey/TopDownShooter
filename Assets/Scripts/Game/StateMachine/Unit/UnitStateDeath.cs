using CodeBase.ECSCore;
using CodeBase.Game.Components;
using CodeBase.Game.Enums;
using CodeBase.Infrastructure.Factories.Effects;
using CodeBase.Infrastructure.Models;
using CodeBase.Infrastructure.Progress;
using CodeBase.Utils;
using Cysharp.Threading.Tasks;
using VContainer;

namespace CodeBase.Game.StateMachine.Unit
{
    public sealed class UnitStateDeath : UnitState, IState
    {
        private IProgressService _progressService;
        private IEffectFactory _effectFactory;
        private LevelModel _levelModel;

        public UnitStateDeath(IStateMachine stateMachine, CUnit unit) : base(stateMachine, unit)
        {
        }

        [Inject]
        private void Construct(IProgressService progressService, IEffectFactory effectFactory, LevelModel levelModel)
        {
            _progressService = progressService;
            _effectFactory = effectFactory;
            _levelModel = levelModel;
        }

        public void Enter()
        {
            Unit.Agent.Agent.ResetPath();
            Unit.Radar.Clear.Execute();
            Unit.Animator.OnDeath.Execute();
            Unit.CleanSubscribe();
            
            _progressService.MoneyData.Data.Value += Unit.Money;
            _levelModel.RemoveEnemy(Unit);
            _effectFactory.CreateEffect(EffectType.Death, Unit.Position.AddY(Unit.Height)).Forget();
        }

        public void Exit() { }

        public void Tick() { }
    }
}