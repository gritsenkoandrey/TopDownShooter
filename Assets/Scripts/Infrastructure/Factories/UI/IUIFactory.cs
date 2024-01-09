using CodeBase.Game.ComponentsUi;
using CodeBase.Game.Enums;
using CodeBase.Game.Interfaces;
using CodeBase.Infrastructure.Services;
using CodeBase.UI.Screens;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace CodeBase.Infrastructure.Factories.UI
{
    public interface IUIFactory : IService
    {
        public UniTask<BaseScreen> CreateScreen(ScreenType type);
        public UniTask<CUpgradeButton> CreateUpgradeButton(UpgradeButtonType type, Transform parent);
        public UniTask<CEnemyHealth> CreateEnemyHealth(IEnemy enemy, Transform parent);
        public UniTask<CPointerArrow> CreatePointerArrow(Transform parent);
        public UniTask<CDamageView> CreateDamageView(Transform parent);
        public void CleanUp();
    }
}