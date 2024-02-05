using CodeBase.Game.Components;
using UnityEngine;

namespace CodeBase.Game.Builders.Player
{
    public sealed class UnitBuilder
    {
        private GameObject _prefab;
        private Transform _parent;
        private Vector3 _position;
        
        public UnitBuilder SetPrefab(GameObject prefab)
        {
            _prefab = prefab;
            return this;
        }

        public UnitBuilder SetParent(Transform parent)
        {
            _parent = parent;
            return this;
        }

        public UnitBuilder SetPosition(Vector3 position)
        {
            _position = position;
            return this;
        }

        public CUnit Build()
        {
            return Object.Instantiate(_prefab, _position, Quaternion.identity, _parent).GetComponent<CUnit>();
        }
    }
}