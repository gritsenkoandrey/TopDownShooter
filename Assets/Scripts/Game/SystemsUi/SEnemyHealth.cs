using CodeBase.ECSCore;
using CodeBase.Game.ComponentsUi;
using CodeBase.Game.Interfaces;
using CodeBase.Utils;
using UniRx;

namespace CodeBase.Game.SystemsUi
{
    public sealed class SEnemyHealth : SystemComponent<CEnemyHealth>
    {
        protected override void OnEnableSystem()
        {
            base.OnEnableSystem();
        }

        protected override void OnDisableSystem()
        {
            base.OnDisableSystem();
        }

        protected override void OnEnableComponent(CEnemyHealth component)
        {
            base.OnEnableComponent(component);

            component.Enemy
                .SkipLatestValueOnSubscribe()
                .First()
                .Subscribe(enemy =>
                {
                    SubscribeOnChangeHealth(component, enemy);
                })
                .AddTo(component.LifetimeDisposable);
        }

        protected override void OnDisableComponent(CEnemyHealth component)
        {
            base.OnDisableComponent(component);
        }

        private void SubscribeOnChangeHealth(CEnemyHealth component, IEnemy enemy)
        {
            enemy.Health.Health
                .Subscribe(health =>
                {
                    if (health > 0)
                    {
                        component.Text.text = $"{health}/{enemy.Health.MaxHealth}";
                        float fillAmount = Mathematics.Remap(0, enemy.Health.MaxHealth, 0, 1, health);
                        component.Fill.fillAmount = fillAmount;
                    }
                    else
                    {
                        component.CanvasGroup.alpha = 0f;
                    }
                })
                .AddTo(component.LifetimeDisposable);
        }
    }
}