using CodeBase.Game.Components;
using UnityEngine;

namespace CodeBase.Game.Builders.Level
{
    public sealed class LevelBuilder
    {
        private GameObject _prefab;
        private int _levelTime;

        public LevelBuilder SetPrefab(GameObject prefab)
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
            CLevel level = Object.Instantiate(_prefab).GetComponent<CLevel>();
            
            level.SetLevelTime(_levelTime);

            return level;
        }
    }
}