using CodeBase.ECSCore;
using UniRx;
using UnityEngine;

namespace CodeBase.Game.Components
{
    public sealed class CAnimator : EntityComponent<CAnimator>
    {
        [SerializeField] private Animator _animator;
        public Animator Animator => _animator;
        public ReactiveCommand<float> UpdateAnimator { get; } = new();
    }
}