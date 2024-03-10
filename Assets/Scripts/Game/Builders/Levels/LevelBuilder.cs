using CodeBase.Game.Interfaces;
using CodeBase.Infrastructure.StaticData.Data;
using UnityEngine;

namespace CodeBase.Game.Builders.Levels
{
    public sealed class LevelBuilder
    {
        private GameObject _prefab;
        private Level _data;

        public LevelBuilder SetPrefab(GameObject prefab)
        {
            _prefab = prefab;

            return this;
        }
        
        public LevelBuilder SetData(Level data)
        {
            _data = data;

            return this;
        }

        public ILevel Build()
        {
            ILevel level = Object.Instantiate(_prefab).GetComponent<ILevel>();
            
            level.Time.Value = _data.LevelTime;

            return level;
        }
    }
}