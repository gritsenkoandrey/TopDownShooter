using CodeBase.ECSCore;
using UniRx;
using UnityEngine;

namespace CodeBase.Game.Components
{
    public sealed class CAnimator : EntityComponent<CAnimator>
    {
        [SerializeField] private Animator _animator;
        public Animator Animator => _animator;

        public readonly ReactiveCommand<float> OnRun = new();
        public readonly ReactiveCommand OnIdle = new();
        public readonly ReactiveCommand OnAttack = new();
        public readonly ReactiveCommand OnDeath = new();
        public readonly ReactiveCommand OnVictory = new();
    }
}