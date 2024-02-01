using CodeBase.ECSCore;
using CodeBase.Game.ComponentsUi;
using CodeBase.Game.Interfaces;
using CodeBase.Infrastructure.Factories.UI;
using CodeBase.Infrastructure.Models;
using Cysharp.Threading.Tasks;
using VContainer;

namespace CodeBase.Game.SystemsUi
{
    public sealed class SEnemyHealthProvider : SystemComponent<CEnemyHealthProvider>
    {
        private IUIFactory _uiFactory;
        private LevelModel _levelModel;

        [Inject]
        private void Construct(IUIFactory uiFactory, LevelModel levelModel)
        {
            _uiFactory = uiFactory;
            _levelModel = levelModel;
        }

        protected override void OnEnableComponent(CEnemyHealthProvider component)
        {
            base.OnEnableComponent(component);

            CreateEnemyHealths(component).Forget();
        }

        private async UniTaskVoid CreateEnemyHealths(CEnemyHealthProvider component)
        {
            foreach (IEnemy enemy in _levelModel.Enemies)
            {
                CEnemyHealth enemyHealth = await _uiFactory.CreateEnemyHealth(enemy, component.transform);
                enemyHealth.CanvasGroup.alpha = 0f;
            }
        }
    }
}