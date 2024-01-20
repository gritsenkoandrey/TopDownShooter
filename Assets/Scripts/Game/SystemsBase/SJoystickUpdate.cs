using CodeBase.ECSCore;
using CodeBase.Infrastructure.Input;

namespace CodeBase.Game.SystemsBase
{
    public sealed class SJoystickUpdate : SystemBase
    {
        private readonly IJoystickService _joystickService;

        public SJoystickUpdate(IJoystickService joystickService)
        {
            _joystickService = joystickService;
        }
        
        protected override void OnUpdate()
        {
            base.OnUpdate();
            
            _joystickService.Execute();
        }
    }
}