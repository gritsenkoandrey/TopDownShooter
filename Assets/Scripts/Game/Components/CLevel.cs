using CodeBase.ECSCore;
using CodeBase.Game.Enums;
using UnityEngine;

namespace CodeBase.Game.Components
{
    public sealed class CLevel : EntityComponent<CLevel>
    {
        [SerializeField] private Transform _characterSpawnPoint;

        public Vector3 CharacterSpawnPosition => _characterSpawnPoint.position;
        public LevelType LevelType { get; private set; }
        public int LevelTime { get; private set; }

        public void SetLevelType(LevelType levelType) => LevelType = levelType;
        public void SetLevelTime(int levelTime) => LevelTime = levelTime;
    }
}