using CodeBase.ECSCore;
using CodeBase.Game.ComponentsUi;
using CodeBase.Game.Interfaces;
using CodeBase.Infrastructure.Factories.UI;
using CodeBase.Infrastructure.Models;
using Cysharp.Threading.Tasks;

namespace CodeBase.Game.SystemsUi
{
    public sealed class SEnemyHealthProvider : SystemComponent<CEnemyHealthProvider>
    {
        private readonly IUIFactory _uiFactory;
        private readonly LevelModel _levelModel;

        public SEnemyHealthProvider(IUIFactory uiFactory, LevelModel levelModel)
        {
            _uiFactory = uiFactory;
            _levelModel = levelModel;
        }

        protected override void OnEnableComponent(CEnemyHealthProvider component)
        {
            base.OnEnableComponent(component);

            CreateEnemyHealths(component);
        }

        private void CreateEnemyHealths(CEnemyHealthProvider component)
        {
            foreach (IEnemy enemy in _levelModel.Enemies)
            {
                Initialize(component, enemy).Forget();
            }
        }

        private async UniTaskVoid Initialize(CEnemyHealthProvider component, IEnemy enemy)
        {
            CEnemyHealth enemyHealth = await _uiFactory.CreateEnemyHealth(enemy, component.transform);
            enemyHealth.CanvasGroup.alpha = 0f;
        }
    }
}