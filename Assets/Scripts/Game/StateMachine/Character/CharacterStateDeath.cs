using CodeBase.ECSCore;
using CodeBase.Game.Components;

namespace CodeBase.Game.StateMachine.Character
{
    public sealed class CharacterStateDeath : CharacterState, IState
    {
        public CharacterStateDeath(IStateMachine stateMachine, CCharacter character) : base(stateMachine, character)
        {
        }

        void IState.Enter()
        {
            Character.Animator.OnDeath.Execute();
            Character.CleanSubscribe();
            Character.Radar.Clear.Execute();
        }

        void IState.Exit() { }

        void IState.Tick() { }
    }
}