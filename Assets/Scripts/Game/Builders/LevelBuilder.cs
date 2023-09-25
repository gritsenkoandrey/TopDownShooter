using CodeBase.Game.Components;
using CodeBase.Game.Enums;
using UnityEngine;

namespace CodeBase.Game.Builders
{
    public sealed class LevelBuilder
    {
        private CLevel _prefab;
        private LevelType _levelType;
        private int _levelTime;

        public LevelBuilder SetPrefab(CLevel prefab)
        {
            _prefab = prefab;

            return this;
        }

        public LevelBuilder SetLevelType(LevelType levelType)
        {
            _levelType = levelType;

            return this;
        }

        public LevelBuilder SetLevelTime(int levelTime)
        {
            _levelTime = levelTime;

            return this;
        }

        public LevelBuilder Reset()
        {
            _prefab = null;
            _levelType = default;
            _levelTime = default;

            return this;
        }

        public CLevel Build()
        {
            CLevel level = Object.Instantiate(_prefab);
            
            level.SetLevelType(_levelType);
            level.SetLevelTime(_levelTime);

            return level;
        }
    }
}