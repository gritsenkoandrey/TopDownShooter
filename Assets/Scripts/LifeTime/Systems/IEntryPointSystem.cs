using System;
using VContainer.Unity;

namespace CodeBase.LifeTime.Systems
{
    public interface IEntryPointSystem : IInitializable, ITickable, IFixedTickable, ILateTickable, IDisposable
    {
    }
}