using CodeBase.Game.Components;
using CodeBase.Game.ComponentsUi;
using CodeBase.Game.Interfaces;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace CodeBase.Infrastructure.Factories.Game
{
    public interface IGameFactory
    {
        public UniTask<ILevel> CreateLevel();
        public UniTask<CCharacter> CreateCharacter(Vector3 position, Transform parent);
        public UniTask<CUnit> CreateUnit(Vector3 position, Transform parent);
        public UniTask<CCharacterPreview> CreateCharacterPreview();
        public void CleanUp();
    }
}