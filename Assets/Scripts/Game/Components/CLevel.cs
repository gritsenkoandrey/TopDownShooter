using CodeBase.ECSCore;
using UnityEngine;

namespace CodeBase.Game.Components
{
    public sealed class CLevel : EntityComponent<CLevel>
    {
        [SerializeField] private Transform _characterSpawnPoint;

        public Vector3 CharacterSpawnPosition => _characterSpawnPoint.position;
        
        protected override void OnEntityCreate() { }
        protected override void OnEntityEnable() { }
        protected override void OnEntityDisable() { }
    }
}