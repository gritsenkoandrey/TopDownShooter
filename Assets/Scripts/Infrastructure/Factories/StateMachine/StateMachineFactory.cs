using CodeBase.Game.Components;
using CodeBase.Game.StateMachine;
using CodeBase.Game.StateMachine.Character;
using CodeBase.Game.StateMachine.Unit;
using CodeBase.Infrastructure.CameraMain;
using CodeBase.Infrastructure.Input;
using CodeBase.Infrastructure.Models;
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

        public StateMachineFactory(IJoystickService joystickService, ICameraService cameraService, LevelModel levelModel)
        {
            _joystickService = joystickService;
            _cameraService = cameraService;
            _levelModel = levelModel;
        }
        
        IStateMachine IStateMachineFactory.CreateCharacterStateMachine(CCharacter character) => 
            new CharacterStateMachine(character, _joystickService, _cameraService, _levelModel);

        IStateMachine IStateMachineFactory.CreateUnitStateMachine(CUnit unit) => new UnitStateMachine(unit, _levelModel);
    }
}