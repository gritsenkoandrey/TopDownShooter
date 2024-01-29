using CodeBase.Game.Components;
using UnityEngine;

namespace CodeBase.Game.Builders
{
    public sealed class LevelBuilder
    {
        private CLevel _prefab;
        private int _levelTime;

        public LevelBuilder SetPrefab(CLevel prefab)
        {
            _prefab = prefab;

            return this;
        }
        
        public LevelBuilder SetLevelTime(int levelTime)
        {
            _levelTime = levelTime;

            return this;
        }

        public CLevel Build()
        {
            CLevel level = Object.Instantiate(_prefab);
            
            level.SetLevelTime(_levelTime);

            return level;
        }
    }
}