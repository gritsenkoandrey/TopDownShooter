using CodeBase.ECSCore;
using CodeBase.Utils;
using UniRx;
using UnityEngine;

namespace CodeBase.Game.Components
{
    public class CDeathEffect : EntityComponent<CDeathEffect>
    {
        [SerializeField] private GameObject[] _effects;
        [SerializeField] private GameObject _shadow;

        public IReactiveCommand<Unit> PlayEffect { get; } = new ReactiveCommand();

        public Transform GetEffect()
        {
            _shadow.SetActive(false);

            GameObject effect = _effects.GetRandomElement();
            
            effect.SetActive(true);

            return effect.transform;
        }
    }
}