using CodeBase.Infrastructure;
using CodeBase.Infrastructure.AssetData;
using CodeBase.Infrastructure.Curtain;
using CodeBase.Infrastructure.Factories.Game;
using CodeBase.Infrastructure.Factories.UI;
using CodeBase.Infrastructure.Loader;
using CodeBase.Infrastructure.Progress;
using CodeBase.Infrastructure.SaveLoad;
using CodeBase.Infrastructure.States;
using CodeBase.Infrastructure.StaticData;
using VContainer;
using VContainer.Unity;

namespace CodeBase.LifeTime
{
    public sealed class BootstrapScope : LifetimeScope
    {
        protected override void Awake()
        {
            base.Awake();
            
            DontDestroyOnLoad(this);
        }

        protected override void Configure(IContainerBuilder builder)
        {
            RegisterLoadingCurtain(builder);
            RegisterSceneLoader(builder);
            RegisterGameStateMachine(builder);
            RegisterAssetProvider(builder);
            RegisterStaticData(builder);
            RegisterProgress(builder);
            RegisterGameFactory(builder);
            RegisterUiFactory(builder);
            RegisterSaveLoad(builder);
            
            builder.RegisterEntryPoint<BootstrapEntryPoint>().Build();
        }

        private void RegisterLoadingCurtain(IContainerBuilder builder) => builder.RegisterComponentInHierarchy<LoadingCurtain>();
        private void RegisterSceneLoader(IContainerBuilder builder) => builder.Register<ISceneLoader, SceneLoader>(Lifetime.Singleton);
        private void RegisterGameStateMachine(IContainerBuilder builder) => builder.Register<IGameStateMachine, GameStateMachine>(Lifetime.Singleton);
        private void RegisterAssetProvider(IContainerBuilder builder) => builder.Register<IAsset, AssetProvider>(Lifetime.Singleton);
        private void RegisterStaticData(IContainerBuilder builder) => builder.Register<IStaticDataService, StaticDataService>(Lifetime.Singleton);
        private void RegisterProgress(IContainerBuilder builder) => builder.Register<IProgressService, ProgressService>(Lifetime.Singleton);
        private void RegisterGameFactory(IContainerBuilder builder) => builder.Register<IGameFactory, GameFactory>(Lifetime.Singleton);
        private void RegisterUiFactory(IContainerBuilder builder) => builder.Register<IUIFactory, UIFactory>(Lifetime.Singleton);
        private void RegisterSaveLoad(IContainerBuilder builder) => builder.Register<ISaveLoadService, SaveLoadService>(Lifetime.Singleton);
    }
}