using CodeBase.ECSCore;
using CodeBase.Game.Components;
using CodeBase.Game.StateMachine.Character;
using CodeBase.Infrastructure.CameraMain;
using CodeBase.Infrastructure.Factories.Game;
using CodeBase.Infrastructure.Input;
using UniRx;

namespace CodeBase.Game.Systems
{
    public sealed class SCharacterInitStateMachine : SystemComponent<CCharacter>
    {
        private readonly ICameraService _cameraService;
        private readonly IJoystickService _joystickService;
        private readonly IGameFactory _gameFactory;

        public SCharacterInitStateMachine(ICameraService cameraService, IJoystickService joystickService, IGameFactory gameFactory)
        {
            _cameraService = cameraService;
            _joystickService = joystickService;
            _gameFactory = gameFactory;
        }

        protected override void OnEnableComponent(CCharacter component)
        {
            base.OnEnableComponent(component);

            InitializeStateMachine(component);
        }

        private void InitializeStateMachine(CCharacter component)
        {
            component.StateMachine.SetStateMachine(new CharacterStateMachine(component, _cameraService, _joystickService, _gameFactory));

            component.StateMachine.UpdateStateMachine
                .Subscribe(_ => component.StateMachine.StateMachine.Tick())
                .AddTo(component.LifetimeDisposable);
        }
    }
}