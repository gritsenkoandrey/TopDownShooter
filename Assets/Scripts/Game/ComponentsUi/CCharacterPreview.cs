using CodeBase.ECSCore;
using UnityEngine;

namespace CodeBase.Game.ComponentsUi
{
    public sealed class CCharacterPreview : EntityComponent<CCharacterPreview>
    {
        [SerializeField] private CCharacterPreviewModel _characterPreviewModel;
        [SerializeField] private CCharacterPreviewAnimator _characterPreviewAnimator;
        [SerializeField] private Camera _camera;

        public CCharacterPreviewModel CharacterPreviewModel => _characterPreviewModel;
        public CCharacterPreviewAnimator CharacterPreviewAnimator => _characterPreviewAnimator;
        public Camera Camera => _camera;
    }
}