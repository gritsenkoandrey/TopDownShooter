using CodeBase.ECSCore;
using CodeBase.Game.Components;
using CodeBase.Game.Enums;
using CodeBase.Infrastructure.Factories.Effects;
using CodeBase.Utils;
using Cysharp.Threading.Tasks;
using UniRx;
using VContainer;

namespace CodeBase.Game.Systems
{
    public sealed class SCharacterDeath : SystemComponent<CCharacter>
    {
        private IEffectFactory _effectFactory;

        [Inject]
        private void Construct(IEffectFactory effectFactory)
        {
            _effectFactory = effectFactory;
        }

        protected override void OnEnableComponent(CCharacter component)
        {
            base.OnEnableComponent(component);

            component.Health.CurrentHealth
                .SkipLatestValueOnSubscribe()
                .First(health => health <= 0)
                .Subscribe(_ =>
                {
                    _effectFactory.CreateEffect(EffectType.Death, component.Position.AddY(component.Height)).Forget();
                    _effectFactory.CreateEffect(EffectType.Blood, component.Position).Forget();

                    component.Shadow.SetActive(false);
                })
                .AddTo(component.LifetimeDisposable);
        }
    }
}