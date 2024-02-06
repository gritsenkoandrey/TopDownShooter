using CodeBase.Game.Components;
using CodeBase.Infrastructure.StaticData.Data;
using UnityEngine;

namespace CodeBase.Game.Builders.Player
{
    public sealed class CharacterBuilder
    {
        private GameObject _prefab;
        private CharacterData _data;
        private Transform _parent;
        private Vector3 _position;

        public CharacterBuilder SetPrefab(GameObject prefab)
        {
            _prefab = prefab;
            return this;
        }

        public CharacterBuilder SetParent(Transform parent)
        {
            _parent = parent;
            return this;
        }

        public CharacterBuilder SetPosition(Vector3 position)
        {
            _position = position;
            return this;
        }

        public CharacterBuilder SetData(CharacterData data)
        {
            _data = data;
            return this;
        }

        public CCharacter Build()
        {
            CCharacter character = Object
                .Instantiate(_prefab, _position, Quaternion.identity, _parent)
                .GetComponent<CCharacter>();

            character.Health.SetBaseHealth(_data.Health);
            character.Move.SetBaseSpeed(_data.Speed);

            return character;
        }
    }
}