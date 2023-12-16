using CodeBase.ECSCore;
using CodeBase.Game.Interfaces;
using CodeBase.Infrastructure.CameraMain;
using CodeBase.Infrastructure.Factories.Game;
using CodeBase.Infrastructure.Input;

namespace CodeBase.Game.StateMachine.Character
{
    public sealed class CharacterStateNone : CharacterState, IState
    {
        public CharacterStateNone(IStateMachine stateMachine, ICharacter character, ICameraService cameraService, 
            IJoystickService joystickService, IGameFactory gameFactory) 
            : base(stateMachine, character, cameraService, joystickService, gameFactory)
        {
        }

        void IState.Enter()
        {
            Character.Animator.OnRun.Execute(0f);
            Character.Entity.CleanSubscribe();
        }

        void IState.Exit() { }

        void IState.Tick() { }
    }
}