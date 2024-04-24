using System;
using CodeBase.ECSCore;
using CodeBase.Utils;
using VContainer.Unity;

namespace CodeBase.LifeTime.Systems
{
    public abstract class EntryPointBase : IEntryPointSystem
    {
        protected ISystem[] Systems = Array.Empty<ISystem>();

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
            Systems = Array.Empty<ISystem>();
        }

        private void Enable(ISystem system) => system.EnableSystem();
        
        private void Disable(ISystem system)
        {
            system.DisableSystem();
            system.Dispose();
        }
    }
}