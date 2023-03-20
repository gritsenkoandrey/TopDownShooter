using CodeBase.ECSCore;
using CodeBase.Game.Components;
using CodeBase.Infrastructure.Factories.UI;
using CodeBase.Infrastructure.Services;
using CodeBase.UI;
using UniRx;

namespace CodeBase.Game.Systems
{
    public sealed class SCharacterDeath : SystemComponent<CCharacter>
    {
        private IUIFactory _uiFactory;
        
        protected override void OnEnableSystem()
        {
            base.OnEnableSystem();

            _uiFactory = AllServices.Container.Single<IUIFactory>();
        }

        protected override void OnDisableSystem()
        {
            base.OnDisableSystem();
        }

        protected override void OnTick()
        {
            base.OnTick();
        }

        protected override void OnEnableComponent(CCharacter component)
        {
            base.OnEnableComponent(component);

            component.Health.Health
                .SkipLatestValueOnSubscribe()
                .Subscribe(health =>
                {
                    if (health <= 0)
                    {
                        _uiFactory.CreateScreen(ScreenType.Result);

                        foreach (CEnemy enemy in component.Enemies)
                        {
                            enemy.LifetimeDisposable.Clear();
                        }
                        
                        component.LifetimeDisposable.Clear();
                    }
                })
                .AddTo(component.LifetimeDisposable);
        }

        protected override void OnDisableComponent(CCharacter component)
        {
            base.OnDisableComponent(component);
        }
    }
}