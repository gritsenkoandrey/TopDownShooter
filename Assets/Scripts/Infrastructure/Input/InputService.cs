using UnityEngine;

namespace CodeBase.Infrastructure.Input
{
    public sealed class InputService : IInputService
    {
        private readonly Joystick _joystick;
        
        public Vector2 Value => _joystick.Value;

        public InputService(Joystick joystick)
        {
            _joystick = joystick;
        }
    }
}