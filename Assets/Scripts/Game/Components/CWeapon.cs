using System;
using CodeBase.ECSCore;
using CodeBase.Game.Behaviours.AnimationStateBehaviour;
using CodeBase.Game.Enums;
using CodeBase.Game.Interfaces;
using CodeBase.Utils;
using UnityEngine;

namespace CodeBase.Game.Components
{
    public sealed class CWeapon : EntityComponent<CWeapon>, IAnimationStateReader
    {
        [SerializeField] private ProjectileType _projectileType;
        [SerializeField] private Transform[] _spawnPoints;
        [SerializeField] private RuntimeAnimatorController _runtimeAnimatorController;

        public ProjectileType ProjectileType => _projectileType;
        public Transform[] SpawnPoints => _spawnPoints;
        public RuntimeAnimatorController RuntimeAnimatorController => _runtimeAnimatorController;
        public event Action OnHit;
        public IWeapon Weapon { get; private set; }
        
        public void SetWeapon(IWeapon weapon)
        {
            Weapon?.Dispose();
            Weapon = weapon;
        }

        protected override void OnEntityDisable()
        {
            base.OnEntityDisable();
            
            Weapon?.Dispose();
        }

        void IAnimationStateReader.EnteredState(int stateHash) { }
        void IAnimationStateReader.ExitedState(int stateHash) { }
        void IAnimationStateReader.UpdateState(int stateHash)
        {
            if (Animations.Shoot == stateHash)
            {
                OnHit?.Invoke();
            }
        }
    }
}