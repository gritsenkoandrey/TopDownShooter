using UnityEngine;

namespace CodeBase.Infrastructure.Input
{
    public interface IJoystickService
    {
        Vector2 GetAxis();
        void Init();
        void Enable(bool isEnable);
        void Execute();
    }
}