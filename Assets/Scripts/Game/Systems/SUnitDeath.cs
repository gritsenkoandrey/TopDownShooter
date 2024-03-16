using CodeBase.ECSCore;
using CodeBase.Game.Components;
using CodeBase.Game.Enums;
using CodeBase.Infrastructure.Factories.Effects;
using CodeBase.Infrastructure.Models;
using CodeBase.Utils;
using Cysharp.Threading.Tasks;
using UniRx;
using VContainer;

namespace CodeBase.Game.Systems
{
    public sealed class SUnitDeath : SystemComponent<CUnit>
    {
        private IEffectFactory _effectFactory;
        private LootModel _lootModel;
        private LevelModel _levelModel;

        [Inject]
        private void Construct(IEffectFactory effectFactory, LootModel lootModel, LevelModel levelModel)
        {
            _effectFactory = effectFactory;
            _lootModel = lootModel;
            _levelModel = levelModel;
        }
        
        protected override void OnEnableComponent(CUnit component)
        {
            base.OnEnableComponent(component);

            component.Health.CurrentHealth
                .SkipLatestValueOnSubscribe()
                .First(health => health <= 0)
                .Subscribe(_ =>
                {
                    _lootModel.GenerateEnemyLoot(component);
                    _levelModel.RemoveEnemy(component);
                    _effectFactory.CreateEffect(EffectType.Death, component.Position.AddY(component.Height)).Forget();
                    
                    component.DeathEffect.PlayEffect.Execute(Unit.Default);
                })
                .AddTo(component.LifetimeDisposable);
        }
    }
}