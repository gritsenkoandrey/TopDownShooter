using CodeBase.ECSCore;
using CodeBase.Game.Components;
using CodeBase.Game.StateMachine.Character;
using CodeBase.Infrastructure.CameraMain;
using CodeBase.Infrastructure.Input;
using CodeBase.Infrastructure.Models;
using UniRx;

namespace CodeBase.Game.Systems
{
    public sealed class SCharacterInitStateMachine : SystemComponent<CCharacter>
    {
        private readonly ICameraService _cameraService;
        private readonly IJoystickService _joystickService;
        private readonly LevelModel _levelModel;

        public SCharacterInitStateMachine(ICameraService cameraService, IJoystickService joystickService, LevelModel levelModel)
        {
            _cameraService = cameraService;
            _joystickService = joystickService;
            _levelModel = levelModel;
        }

        protected override void OnEnableComponent(CCharacter component)
        {
            base.OnEnableComponent(component);

            InitializeStateMachine(component);
        }

        private void InitializeStateMachine(CCharacter component)
        {
            component.StateMachine.SetStateMachine(new CharacterStateMachine(component, _cameraService, _joystickService, _levelModel));

            component.StateMachine.UpdateStateMachine
                .Subscribe(_ => component.StateMachine.StateMachine.Tick())
                .AddTo(component.LifetimeDisposable);
        }
    }
}