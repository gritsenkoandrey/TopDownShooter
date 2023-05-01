using CodeBase.ECSCore;
using CodeBase.Game.Components;
using CodeBase.Infrastructure.Input;

namespace CodeBase.Game.Systems
{
    public sealed class SCharacterInput : SystemComponent<CCharacter>
    {
        private readonly IJoystickService _joystickService;

        public SCharacterInput(IJoystickService joystickService)
        {
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

        protected override void OnLateUpdate()
        {
            base.OnLateUpdate();
            
            foreach (CCharacter character in Entities)
            {
                character.Move.Input = _joystickService.Value;
                
                _joystickService.Execute();
            }
        }

        protected override void OnEnableComponent(CCharacter component)
        {
            base.OnEnableComponent(component);
        }

        protected override void OnDisableComponent(CCharacter component)
        {
            base.OnDisableComponent(component);
        }
    }
}