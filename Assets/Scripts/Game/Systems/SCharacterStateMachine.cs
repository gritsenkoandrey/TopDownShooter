using CodeBase.ECSCore;
using CodeBase.Game.Components;
using CodeBase.Game.StateMachine.Character;
using CodeBase.Infrastructure.CameraMain;
using CodeBase.Infrastructure.Input;
using UniRx;

namespace CodeBase.Game.Systems
{
    public sealed class SCharacterStateMachine : SystemComponent<CCharacter>
    {
        private readonly ICameraService _cameraService;
        private readonly IJoystickService _joystickService;

        public SCharacterStateMachine(ICameraService cameraService, IJoystickService joystickService)
        {
            _cameraService = cameraService;
            _joystickService = joystickService;
        }
        
        protected override void OnEnableSystem()
        {
            base.OnEnableSystem();
        }

        protected override void OnDisableSystem()
        {
            base.OnDisableSystem();
        }

        protected override void OnUpdate()
        {
            base.OnUpdate();

            foreach (CCharacter character in Entities)
            {
                character.UpdateStateMachine.Execute();
            }
        }

        protected override void OnEnableComponent(CCharacter component)
        {
            base.OnEnableComponent(component);

            InitializeStateMachine(component);
        }

        private void InitializeStateMachine(CCharacter component)
        {
            CharacterStateMachine stateMachine = new CharacterStateMachine(component);

            stateMachine.Construct(_cameraService, _joystickService);

            component.UpdateStateMachine
                .Subscribe(_ => stateMachine.Tick())
                .AddTo(component.LifetimeDisposable);
        }

        protected override void OnDisableComponent(CCharacter component)
        {
            base.OnDisableComponent(component);
        }
    }
}