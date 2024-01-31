using CodeBase.ECSCore;
using CodeBase.Infrastructure.Input;
using VContainer;

namespace CodeBase.Game.SystemsBase
{
    public sealed class SJoystickUpdate : SystemBase
    {
        private IJoystickService _joystickService;

        [Inject]
        public void Construct(IJoystickService joystickService)
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