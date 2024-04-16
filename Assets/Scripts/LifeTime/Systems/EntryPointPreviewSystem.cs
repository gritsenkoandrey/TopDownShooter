using CodeBase.Infrastructure.Factories.Systems;
using CodeBase.Utils;
using JetBrains.Annotations;
using VContainer;

namespace CodeBase.LifeTime.Systems
{
    [UsedImplicitly(ImplicitUseKindFlags.InstantiatedNoFixedConstructorSignature)]
    public sealed class EntryPointPreviewSystem : EntryPointBase
    {
        private readonly IObjectResolver _objectResolver;
        private readonly ISystemFactory _systemFactory;

        public EntryPointPreviewSystem(IObjectResolver objectResolver, ISystemFactory systemFactory)
        {
            _objectResolver = objectResolver;
            _systemFactory = systemFactory;
        }
        
        public override void Initialize()
        {
            base.Initialize();
            
            Systems = _systemFactory.CreatePreviewSystems();
            Systems.Foreach(_objectResolver.Inject);
        }
    }
}