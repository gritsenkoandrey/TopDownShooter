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
        [Range(2.5f, 10f)] public float AttackDistance;
        [Range(0.1f, 2f)] public float AttackRecharge;
        
        public CCharacter Prefab;
    }
}