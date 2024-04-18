using CodeBase.ECSCore;
using CodeBase.Game.Components;

namespace CodeBase.Game.StateMachine.Character
{
    public sealed class CharacterStateNone : CharacterState, IState
    {
        public CharacterStateNone(IStateMachine stateMachine, CCharacter character) : base(stateMachine, character)
        {
        }

        void IState.Enter()
        {
            Character.Animator.OnRun.Execute(0f);
            Character.Radar.Clear.Execute();
            Character.CleanSubscribe();
        }

        void IState.Exit() { }

        void IState.Tick() { }
    }
}