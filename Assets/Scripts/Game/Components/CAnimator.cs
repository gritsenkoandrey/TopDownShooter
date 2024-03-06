using CodeBase.ECSCore;
using CodeBase.Game.Interfaces;
using UniRx;
using UnityEngine;

namespace CodeBase.Game.Components
{
    public sealed class CAnimator : EntityComponent<CAnimator>, IPause
    {
        [SerializeField] private Animator _animator;
        
        public Animator Animator => _animator;

        public readonly ReactiveCommand<float> OnRun = new();
        public readonly ReactiveCommand OnAttack = new();
        public readonly ReactiveCommand OnDeath = new();
        public readonly ReactiveCommand OnVictory = new();
        
        void IPause.Pause(bool isPause) => _animator.speed = isPause ? 0f : 1f;
    }
}