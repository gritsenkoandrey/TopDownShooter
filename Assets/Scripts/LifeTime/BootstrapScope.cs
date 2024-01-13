using CodeBase.App;
using CodeBase.Game.Weapon.Factories;
using CodeBase.Infrastructure.AssetData;
using CodeBase.Infrastructure.CameraMain;
using CodeBase.Infrastructure.Curtain;
using CodeBase.Infrastructure.Factories.Effects;
using CodeBase.Infrastructure.Factories.Game;
using CodeBase.Infrastructure.Factories.TextureArray;
using CodeBase.Infrastructure.Factories.UI;
using CodeBase.Infrastructure.GUI;
using CodeBase.Infrastructure.Input;
using CodeBase.Infrastructure.Loader;
using CodeBase.Infrastructure.Models;
using CodeBase.Infrastructure.Pool;
using CodeBase.Infrastructure.Progress;
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

            builder.Register<InventoryModel>(Lifetime.Singleton).AsSelf();
            builder.Register<LevelModel>(Lifetime.Singleton).AsSelf();
            
            builder.Register<ISceneLoaderService, SceneLoaderService>(Lifetime.Singleton);
            builder.Register<IProgressService, ProgressService>(Lifetime.Singleton);
            builder.Register<IAssetService, AssetService>(Lifetime.Singleton);
            
            builder.Register<IStaticDataService, StaticDataService>(Lifetime.Singleton);
            builder.Register<ITextureArrayFactory, TextureArrayFactory>(Lifetime.Singleton);
            builder.Register<IGameFactory, GameFactory>(Lifetime.Singleton);
            builder.Register<IUIFactory, UIFactory>(Lifetime.Singleton);
            builder.Register<IWeaponFactory, WeaponFactory>(Lifetime.Singleton);
            builder.Register<IEffectFactory, EffectFactory>(Lifetime.Singleton);
            builder.Register<IObjectPoolService, ObjectPoolService>(Lifetime.Singleton).WithParameter(transform);
            
            builder.Register<IGameStateService, GameStateService>(Lifetime.Singleton);
            builder.Register<StateBootstrap>(Lifetime.Singleton).As<IInitializable>();
            builder.Register<StateFail>(Lifetime.Singleton).As<IInitializable>();
            builder.Register<StateGame>(Lifetime.Singleton).As<IInitializable>();
            builder.Register<StateLoadLevel>(Lifetime.Singleton).As<IInitializable>();
            builder.Register<StateLoadProgress>(Lifetime.Singleton).As<IInitializable>();
            builder.Register<StateLobby>(Lifetime.Singleton).As<IInitializable>();
            builder.Register<StatePreview>(Lifetime.Singleton).As<IInitializable>();
            builder.Register<StateWin>(Lifetime.Singleton).As<IInitializable>();

            builder.RegisterEntryPoint<SystemEntryPoint>().AsSelf();
            builder.RegisterEntryPoint<BootstrapEntryPoint>().AsSelf();
        }
    }
}