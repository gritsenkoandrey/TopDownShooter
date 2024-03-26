using System;
using VContainer.Unity;

namespace CodeBase.LifeTime.Systems
{
    public interface IEntryPointSystem : IInitializable, IStartable, ITickable, IFixedTickable, ILateTickable, IDisposable
    {
    }
}