using CodeBase.ECSCore;
using CodeBase.Game.Behaviours.Gizmos;
using CodeBase.Game.Enums;
using CodeBase.Game.Interfaces;
using UnityEngine;

namespace CodeBase.Game.Components
{
    public sealed class CLevel : EntityComponent<CLevel>, ILevel
    {
        [SerializeField] private SpawnPoint[] _spawnPoints;
        public SpawnPoint[] SpawnPoints => _spawnPoints;
        public LevelType LevelType { get; private set; }
        public int LevelTime { get; private set; }
        public Entity Entity => this;

        public void SetLevelType(LevelType levelType) => LevelType = levelType;
        public void SetLevelTime(int levelTime) => LevelTime = levelTime;
    }
}