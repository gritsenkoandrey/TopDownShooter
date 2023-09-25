using CodeBase.Game.Components;
using CodeBase.Infrastructure.CameraMain;
using UnityEngine;

namespace CodeBase.Game.Builders
{
    public sealed class CharacterBuilder
    {
        private CCharacter _prefab;
        private ICameraService _cameraService;

        private Transform _parent;
        private Vector3 _position;
        private int _health;
        private int _damage;
        private float _attackDistance;
        private float _attackRecharge;
        private float _speed;

        public CharacterBuilder SetPrefab(CCharacter prefab)
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

        public CharacterBuilder SetHealth(int health)
        {
            _health = health;

            return this;
        }

        public CharacterBuilder SetDamage(int damage)
        {
            _damage = damage;

            return this;
        }
        
        public CharacterBuilder SetAttackDistance(float attackDistance)
        {
            _attackDistance = attackDistance;

            return this;
        }
        
        public CharacterBuilder SetAttackRecharge(float attackRecharge)
        {
            _attackRecharge = attackRecharge;

            return this;
        }

        public CharacterBuilder SetSpeed(float speed)
        {
            _speed = speed;

            return this;
        }

        public CharacterBuilder SetCamera(ICameraService cameraService)
        {
            _cameraService = cameraService;

            return this;
        }

        public CharacterBuilder Reset()
        {
            _prefab = null;
            _cameraService = null;
            _position = default;
            _health = default;
            _damage = default;
            _attackDistance = default;
            _attackRecharge = default;
            _speed = default;

            return this;
        }

        public CCharacter Build()
        {
            CCharacter character = Object.Instantiate(_prefab, _position, Quaternion.identity, _parent);

            character.Health.BaseHealth = _health;
            character.Weapon.BaseDamage = _damage;
            character.Weapon.AttackDistance = _attackDistance;
            character.Weapon.AttackRecharge = _attackRecharge;
            character.Move.BaseSpeed = _speed;
            
            _cameraService.SetTarget(character.transform);

            return character;
        }
    }
}