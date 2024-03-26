using System;
using CodeBase.ECSCore;
using CodeBase.Game.SystemsBase;
using CodeBase.Game.SystemsUi;
using CodeBase.Utils;
using JetBrains.Annotations;
using VContainer;
using VContainer.Unity;

namespace CodeBase.LifeTime.Systems
{
    [UsedImplicitly(ImplicitUseKindFlags.InstantiatedNoFixedConstructorSignature)]
    public sealed class EntryPointPreviewSystem : IEntryPointSystem
    {
        private SystemBase[] _systems = Array.Empty<SystemBase>();

        private readonly IObjectResolver _objectResolver;

        public EntryPointPreviewSystem(IObjectResolver objectResolver) => _objectResolver = objectResolver;

        void IInitializable.Initialize()
        {
            _systems = new SystemBase[]
            {
                new SCharacterPreview(),
                new SShopCharacterRenderer(),
                new SShopSwipeButtons(),
                new SShopMediator(),
                new SShopUpgradeWindow(),
                new SUpgradeButton(),
                new SMoneyUpdate(),
                new SShopPrice(),
                new SShopBuyButton(),
                new SShopElementsChangeState(),
                new SHapticController(),
                new SHapticButton(),
                new STaskUpdate(),
                new SShopTaskProvider(),
                new SDailyTaskUpdate(),
            };
            
            _systems.Foreach(_objectResolver.Inject);
        }
        
        void IStartable.Start() => _systems.Foreach(Enable);

        void ITickable.Tick()
        {
            for (int i = 0; i < _systems.Length; i++)
            {
                _systems[i].Update();
            }
        }

        void IFixedTickable.FixedTick()
        {
            for (int i = 0; i < _systems.Length; i++)
            {
                _systems[i].FixedUpdate();
            }
        }

        void ILateTickable.LateTick()
        {
            for (int i = 0; i < _systems.Length; i++)
            {
                _systems[i].LateUpdate();
            }
        }

        void IDisposable.Dispose()
        {
            _systems.Foreach(Disable);
            _systems = Array.Empty<SystemBase>();
        }
        
        private void Enable(SystemBase system) => system.EnableSystem();
        private void Disable(SystemBase system) => system.DisableSystem();
    }
}