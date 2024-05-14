using CodeBase.Game.Components;
using CodeBase.Game.ComponentsUi;
using CodeBase.Game.Enums;
using CodeBase.Game.Interfaces;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace CodeBase.Infrastructure.Factories.Game
{
    public interface IGameFactory
    {
        UniTask<ILevel> CreateLevel();
        UniTask<CCharacter> CreateCharacter(SkinType skinType, Vector3 position, Transform parent);
        UniTask<CUnit> CreateUnit(Vector3 position, Transform parent);
        UniTask<CTurret> CreateTurret(TurretType turretType, Vector3 position, Transform parent);
        UniTask<CCharacterPreview> CreateCharacterPreview();
    }
}