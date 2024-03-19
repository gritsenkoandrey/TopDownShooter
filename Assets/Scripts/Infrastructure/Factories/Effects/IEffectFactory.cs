using CodeBase.Game.Enums;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace CodeBase.Infrastructure.Factories.Effects
{
    public interface IEffectFactory
    {
        UniTask<GameObject> CreateEffect(EffectType type, Vector3 position);
    }
}