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
            Character.Radar.Clear.Execute();
            Character.CleanSubscribe();
        }

        void IState.Exit() { }

        void IState.Tick() { }
    }
}