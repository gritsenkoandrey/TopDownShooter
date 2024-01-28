using CodeBase.ECSCore;
using CodeBase.Game.Components;
using CodeBase.Infrastructure.CameraMain;
using CodeBase.Infrastructure.Input;
using CodeBase.Infrastructure.Models;

namespace CodeBase.Game.StateMachine.Character
{
    public sealed class CharacterStateNone : CharacterState, IState
    {
        public CharacterStateNone(IStateMachine stateMachine, CCharacter character, ICameraService cameraService, 
            IJoystickService joystickService, LevelModel levelModel) 
            : base(stateMachine, character, cameraService, joystickService, levelModel)
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