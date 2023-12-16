using CodeBase.Game.Interfaces;
using CodeBase.Infrastructure.CameraMain;
using CodeBase.Infrastructure.Factories.Game;
using CodeBase.Infrastructure.Input;

namespace CodeBase.Game.StateMachine.Character
{
    public abstract class CharacterState
    {
        protected readonly ICameraService CameraService;
        protected readonly IJoystickService JoystickService;
        protected readonly IGameFactory GameFactory;
        protected readonly IStateMachine StateMachine;
        protected readonly ICharacter Character;

        protected CharacterState(IStateMachine stateMachine, ICharacter character, ICameraService cameraService, 
            IJoystickService joystickService, IGameFactory gameFactory)
        {
            StateMachine = stateMachine;
            Character = character;
            CameraService = cameraService;
            JoystickService = joystickService;
            GameFactory = gameFactory;
        }
    }
}