using CodeBase.App;
using CodeBase.Infrastructure.AssetData;
using CodeBase.Infrastructure.CameraMain;
using CodeBase.Infrastructure.Curtain;
using CodeBase.Infrastructure.Factories.Game;
using CodeBase.Infrastructure.Factories.TextureArray;
using CodeBase.Infrastructure.Factories.UI;
using CodeBase.Infrastructure.GUI;
using CodeBase.Infrastructure.Input;
using CodeBase.Infrastructure.Loader;
using CodeBase.Infrastructure.Pool;
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

            AppSettings app = new AppSettings();
            
            app.SetSettings();
            
            DontDestroyOnLoad(this);
        }

        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterComponentInHierarchy<LoadingCurtain>().As<ILoadingCurtainService>();
            builder.RegisterComponentInHierarchy<CameraService>().As<ICameraService>();
            builder.RegisterComponentInHierarchy<GuiService>().As<IGuiService>();
            builder.RegisterComponentInHierarchy<JoystickService>().As<IJoystickService>();
            
            builder.Register<ISceneLoaderService, SceneLoaderService>(Lifetime.Singleton);
            builder.Register<IProgressService, ProgressService>(Lifetime.Singleton);
            builder.Register<IAssetService, AssetService>(Lifetime.Singleton);
            builder.Register<IStaticDataService, StaticDataService>(Lifetime.Singleton);
            builder.Register<ITextureArrayFactory, TextureArrayFactory>(Lifetime.Singleton);
            builder.Register<IGameFactory, GameFactory>(Lifetime.Singleton);
            builder.Register<IUIFactory, UIFactory>(Lifetime.Singleton);
            builder.Register<ISaveLoadService, SaveLoadService>(Lifetime.Singleton);
            builder.Register<IObjectPoolService, ObjectPoolService>(Lifetime.Singleton).WithParameter(transform);
            
            builder.Register<IGameStateService, GameStateService>(Lifetime.Singleton);
            
            builder.RegisterEntryPoint<BootstrapEntryPoint>().Build();
        }
    }
}