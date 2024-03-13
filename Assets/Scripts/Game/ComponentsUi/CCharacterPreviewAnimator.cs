using CodeBase.ECSCore;
using UnityEngine;

namespace CodeBase.Game.ComponentsUi
{
    [RequireComponent(typeof(Animator))]
    public sealed class CCharacterPreviewAnimator : EntityComponent<CCharacterPreviewAnimator>
    {
        [SerializeField] private Animator _animator;
        
        public Animator Animator => _animator;
    }
}