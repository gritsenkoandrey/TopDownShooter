using CodeBase.ECSCore;
using CodeBase.Game.Components;
using CodeBase.Game.Enums;
using CodeBase.Game.StateMachine;
using CodeBase.Infrastructure.Factories.UI;
using CodeBase.UI.Screens;
using UniRx;

namespace CodeBase.Game.Systems
{
    public sealed class SCharacterDeath : SystemComponent<CCharacter>
    {
        private readonly IUIFactory _uiFactory;

        public SCharacterDeath(IUIFactory uiFactory)
        {
            _uiFactory = uiFactory;
        }
        
        protected override void OnEnableSystem()
        {
            base.OnEnableSystem();
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
                .ObserveOnMainThread()
                .Subscribe(health =>
                {
                    if (health <= 0)
                    {
                        _uiFactory.CreateScreen(ScreenType.Lose);
                        
                        component.LifetimeDisposable.Clear();

                        foreach (CZombie enemy in component.Enemies)
                        {
                            enemy.Agent.ResetPath();
                            enemy.Radar.Clear.Execute();
                            enemy.State = ZombieState.None;
                        }
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