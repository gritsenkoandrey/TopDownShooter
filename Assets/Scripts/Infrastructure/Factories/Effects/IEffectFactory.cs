using Cysharp.Threading.Tasks;
using UnityEngine;

namespace CodeBase.Infrastructure.Factories.Effects
{
    public interface IEffectFactory
    {
        public UniTask<GameObject> CreateHitFx(Vector3 position);
        public UniTask<GameObject> CreateDeathFx(Vector3 position);
    }
}