using System;

namespace CodeBase.ECSCore
{
    public interface ISystem : IDisposable
    {
        void EnableSystem();
        void DisableSystem();
        void Update();
        void FixedUpdate();
        void LateUpdate();
    }
}