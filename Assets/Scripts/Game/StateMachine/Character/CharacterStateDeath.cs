using CodeBase.ECSCore;
using CodeBase.Game.Components;
using CodeBase.Game.Enums;
using CodeBase.Infrastructure.Factories.Effects;
using CodeBase.Utils;
using Cysharp.Threading.Tasks;
using VContainer;

namespace CodeBase.Game.StateMachine.Character
{
    public sealed class CharacterStateDeath : CharacterState, IState
    {
        private IEffectFactory _effectFactory;
        
        public CharacterStateDeath(IStateMachine stateMachine, CCharacter character) : base(stateMachine, character)
        {
        }

        [Inject]
        private void Construct(IEffectFactory effectFactory)
        {
            _effectFactory = effectFactory;
        }

        void IState.Enter()
        {
            Character.Animator.OnDeath.Execute();
            Character.CleanSubscribe();
            
            _effectFactory.CreateEffect(EffectType.Death, Character.Position.AddY(Character.Height)).Forget();
        }

        void IState.Exit() { }

        void IState.Tick() { }
    }
}