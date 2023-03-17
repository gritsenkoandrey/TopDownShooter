using UnityEngine;

namespace CodeBase.Game.LevelData
{
    public sealed class Level : MonoBehaviour
    {
        [SerializeField] private Transform _characterSpawnPoint;

        public Vector3 CharacterSpawnPosition => _characterSpawnPoint.position;
    }
}