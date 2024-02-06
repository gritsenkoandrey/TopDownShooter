using CodeBase.Game.Components;
using CodeBase.Game.StateMachine;
using CodeBase.Game.StateMachine.Character;
using CodeBase.Game.StateMachine.Unit;
using CodeBase.Infrastructure.CameraMain;
using CodeBase.Infrastructure.Input;
using CodeBase.Infrastructure.Models;
using CodeBase.Infrastructure.States;
using JetBrains.Annotations;
using VContainer;

namespace CodeBase.Infrastructure.Factories.StateMachine
{
    [UsedImplicitly(ImplicitUseKindFlags.InstantiatedNoFixedConstructorSignature)]
    public sealed class StateMachineFactory : IStateMachineFactory
    {
        private readonly IObjectResolver _objectResolver;
        private readonly IJoystickService _joystickService;
        private readonly ICameraService _cameraService;
        private readonly LevelModel _levelModel;

        public StateMachineFactory(IObjectResolver objectResolver, IJoystickService joystickService, 
            ICameraService cameraService, LevelModel levelModel)
        {
            _objectResolver = objectResolver;
            _joystickService = joystickService;
            _cameraService = cameraService;
            _levelModel = levelModel;
        }

        IGameStateMachine IStateMachineFactory.CreateGameStateMachine()
        {
            GameStateMachine gameStateService = new GameStateMachine();

            foreach (IExitState state in gameStateService.States.Values)
            {
                _objectResolver.Inject(state);
            }
            
            return gameStateService;
        }

        IStateMachine IStateMachineFactory.CreateCharacterStateMachine(CCharacter character) => 
            new CharacterStateMachine(character, _joystickService, _cameraService, _levelModel);

        IStateMachine IStateMachineFactory.CreateUnitStateMachine(CUnit unit) => new UnitStateMachine(unit, _levelModel);
    }
}