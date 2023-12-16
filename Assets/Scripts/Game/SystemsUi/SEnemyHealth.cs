using CodeBase.ECSCore;
using CodeBase.Game.ComponentsUi;
using CodeBase.Game.Interfaces;
using CodeBase.Utils;
using UniRx;

namespace CodeBase.Game.SystemsUi
{
    public sealed class SEnemyHealth : SystemComponent<CEnemyHealth>
    {
        protected override void OnEnableComponent(CEnemyHealth component)
        {
            base.OnEnableComponent(component);

            component.Enemy
                .SkipLatestValueOnSubscribe()
                .First()
                .Subscribe(enemy => SubscribeOnChangeHealth(component, enemy))
                .AddTo(component.LifetimeDisposable);
        }

        private void SubscribeOnChangeHealth(CEnemyHealth component, IEnemy enemy)
        {
            enemy.Health.CurrentHealth
                .Subscribe(health =>
                {
                    float fillAmount = Mathematics.Remap(0, enemy.Health.MaxHealth, 0, 1, health);
                    
                    component.Text.text = enemy.Health.ToString();
                    component.Fill.fillAmount = fillAmount;

                    if (health <= 0)
                    {
                        component.CanvasGroup.alpha = 0f;
                    }
                })
                .AddTo(component.LifetimeDisposable);
        }
    }
}