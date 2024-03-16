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
        
        protected override void OnEnableComponent(CUnit unit)
        {
            base.OnEnableComponent(unit);

            unit.Health.CurrentHealth
                .SkipLatestValueOnSubscribe()
                .First(health => health <= 0)
                .Subscribe(_ =>
                {
                    _lootModel.GenerateEnemyLoot(unit);
                    _levelModel.RemoveEnemy(unit);
                    _effectFactory.CreateEffect(EffectType.Death, unit.Position.AddY(unit.Height)).Forget();
                })
                .AddTo(unit.LifetimeDisposable);
        }
    }
}