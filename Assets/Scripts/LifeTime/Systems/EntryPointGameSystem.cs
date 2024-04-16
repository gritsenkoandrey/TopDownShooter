using CodeBase.Infrastructure.Factories.Systems;
using CodeBase.Utils;
using JetBrains.Annotations;
using VContainer;

namespace CodeBase.LifeTime.Systems
{
    [UsedImplicitly(ImplicitUseKindFlags.InstantiatedNoFixedConstructorSignature)]
    public sealed class EntryPointGameSystem : EntryPointBase
    {
        private readonly IObjectResolver _objectResolver;
        private readonly ISystemFactory _systemFactory;

        public EntryPointGameSystem(IObjectResolver objectResolver, ISystemFactory systemFactory)
        {
            _objectResolver = objectResolver;
            _systemFactory = systemFactory;
        }
        
        public override void Initialize()
        {
            base.Initialize();
            
            Systems = _systemFactory.CreateGameSystems();
            Systems.Foreach(_objectResolver.Inject);
        }
    }
}