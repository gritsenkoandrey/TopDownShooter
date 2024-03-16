using CodeBase.Game.ComponentsUi;
using CodeBase.Game.Enums;
using CodeBase.Game.Interfaces;
using CodeBase.UI.Screens;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace CodeBase.Infrastructure.Factories.UI
{
    public interface IUIFactory
    {
        public UniTask<BaseScreen> CreateScreen(ScreenType type);
        public UniTask<BaseScreen> CreatePopUp(ScreenType type);
        public UniTask<CUpgradeButton> CreateUpgradeButton(UpgradeButtonType type, Transform parent);
        public UniTask<CEnemyHealth> CreateEnemyHealth(IEnemy enemy, Transform parent);
        public UniTask<CPointerArrow> CreatePointerArrow(Transform parent);
        public UniTask<CDamageCombatLogView> CreateDamageView(Transform parent);
        public UniTask<CMoneyLoot> CreateMoneyLoot(Transform parent);
    }
}