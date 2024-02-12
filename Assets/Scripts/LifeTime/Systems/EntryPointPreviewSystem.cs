using System;
using CodeBase.ECSCore;
using CodeBase.Game.SystemsUi;
using CodeBase.Utils;
using JetBrains.Annotations;
using VContainer;

namespace CodeBase.LifeTime.Systems
{
    [UsedImplicitly(ImplicitUseKindFlags.InstantiatedNoFixedConstructorSignature)]
    public sealed class EntryPointPreviewSystem : IEntryPointSystem
    {
        private SystemBase[] _systems = Array.Empty<SystemBase>();

        private readonly IObjectResolver _objectResolver;

        public EntryPointPreviewSystem(IObjectResolver objectResolver) => _objectResolver = objectResolver;

        public void Initialize()
        {
            _systems = new SystemBase[]
            {
                new SCharacterPreviewRotation(),
                new SCharacterPreviewMediator(),
            };
            
            _systems.Foreach(_objectResolver.Inject);
            _systems.Foreach(Enable);
        }
        
        public void Tick()
        {
            for (int i = 0; i < _systems.Length; i++)
            {
                _systems[i].Update();
            }
        }

        public void FixedTick()
        {
            for (int i = 0; i < _systems.Length; i++)
            {
                _systems[i].FixedUpdate();
            }
        }

        public void LateTick()
        {
            for (int i = 0; i < _systems.Length; i++)
            {
                _systems[i].LateUpdate();
            }
        }

        public void Dispose()
        {
            _systems.Foreach(Disable);
            _systems = Array.Empty<SystemBase>();
        }
        
        private void Enable(SystemBase system) => system.EnableSystem();
        private void Disable(SystemBase system) => system.DisableSystem();
    }
}