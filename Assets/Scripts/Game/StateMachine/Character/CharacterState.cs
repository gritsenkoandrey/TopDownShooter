using CodeBase.Game.Interfaces;
using CodeBase.Infrastructure.CameraMain;
using CodeBase.Infrastructure.Input;
using CodeBase.Infrastructure.Models;

namespace CodeBase.Game.StateMachine.Character
{
    public abstract class CharacterState
    {
        protected readonly ICameraService CameraService;
        protected readonly IJoystickService JoystickService;
        protected readonly IStateMachine StateMachine;
        protected readonly ICharacter Character;
        protected readonly LevelModel LevelModel;

        protected CharacterState(IStateMachine stateMachine, ICharacter character, ICameraService cameraService, 
            IJoystickService joystickService, LevelModel levelModel)
        {
            StateMachine = stateMachine;
            Character = character;
            CameraService = cameraService;
            JoystickService = joystickService;
            LevelModel = levelModel;
        }
    }
}