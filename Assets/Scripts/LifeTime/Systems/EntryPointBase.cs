using System;
using CodeBase.ECSCore;
using CodeBase.Utils;
using VContainer.Unity;

namespace CodeBase.LifeTime.Systems
{
    public abstract class EntryPointBase : IEntryPointSystem
    {
        protected SystemBase[] Systems = Array.Empty<SystemBase>();

        public virtual void Initialize()
        {
        }
        
        void IStartable.Start() => Systems.Foreach(Enable);

        void ITickable.Tick()
        {
            for (int i = 0; i < Systems.Length; i++)
            {
                Systems[i].Update();
            }
        }
        void IFixedTickable.FixedTick()
        {
            for (int i = 0; i < Systems.Length; i++)
            {
                Systems[i].FixedUpdate();
            }
        }
        void ILateTickable.LateTick()
        {
            for (int i = 0; i < Systems.Length; i++)
            {
                Systems[i].LateUpdate();
            }
        }
        
        void IDisposable.Dispose()
        {
            Systems.Foreach(Disable);
            Systems = Array.Empty<SystemBase>();
        }

        private void Enable(SystemBase system) => system.EnableSystem();
        private void Disable(SystemBase system) => system.DisableSystem();
    }
}