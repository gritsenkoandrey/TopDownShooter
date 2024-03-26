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
        UniTask<BaseScreen> CreateScreen(ScreenType type);
        UniTask<BaseScreen> CreatePopUp(ScreenType type);
        UniTask<CUpgradeButton> CreateUpgradeButton(UpgradeButtonType type, Transform parent);
        UniTask<CEnemyHealth> CreateEnemyHealth(IEnemy enemy, Transform parent);
        UniTask<CPointerArrow> CreatePointerArrow(Transform parent);
        UniTask<CDamageCombatLogView> CreateDamageView(Transform parent);
        UniTask<CMoneyLoot> CreateMoneyLoot(Transform parent);
        UniTask<CTask> CreateDailyTask(Transform parent);
    }
}