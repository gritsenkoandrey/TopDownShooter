using UnityEngine;

namespace CodeBase.Infrastructure.Input
{
    public interface IJoystickService
    {
        Vector2 GetAxis();
        float GetDeadZone();
        void Init();
        void Enable(bool isEnable);
        void Execute();
    }
}