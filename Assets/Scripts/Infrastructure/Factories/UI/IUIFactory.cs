using System.Collections.Generic;
using CodeBase.Game.ComponentsUi;
using CodeBase.Game.Enums;
using CodeBase.Game.Interfaces;
using CodeBase.Infrastructure.Progress;
using CodeBase.Infrastructure.Services;
using CodeBase.UI.Screens;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace CodeBase.Infrastructure.Factories.UI
{
    public interface IUIFactory : IService
    {
        public IList<IProgressReader> ProgressReaders { get; }
        public IList<IProgressWriter> ProgressWriters { get; }
        public UniTask<BaseScreen> CreateScreen(ScreenType type);
        public UniTask<CUpgradeButton> CreateUpgradeButton(UpgradeButtonType type, Transform parent);
        public UniTask<CEnemyHealth> CreateEnemyHealth(IEnemy enemy, Transform parent);
        public void CleanUp();
    }
}