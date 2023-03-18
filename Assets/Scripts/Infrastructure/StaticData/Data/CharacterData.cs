using CodeBase.Game.Components;
using UnityEngine;

namespace CodeBase.Infrastructure.StaticData.Data
{
    [CreateAssetMenu(fileName = "CharacterData", menuName = "Data/CharacterData")]
    public sealed class CharacterData : ScriptableObject
    {
        [Range(1, 20)] public int Health;
        [Range(1, 5)] public int Damage;
        [Range(1f, 5f)] public float Speed;
        
        public CCharacter Prefab;
    }
}