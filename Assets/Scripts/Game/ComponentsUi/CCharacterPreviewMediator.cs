using CodeBase.ECSCore;
using UniRx;
using UnityEngine;

namespace CodeBase.Game.ComponentsUi
{
    public sealed class CCharacterPreviewMediator : EntityComponent<CCharacterPreviewMediator>
    {
        [SerializeField] private CCharacterPreviewAnimator _characterPreviewAnimator;
        [SerializeField] private CCharacterPreviewButtons _characterPreviewButtons;
        [SerializeField] private CCharacterPreviewModel _characterPreviewModel;
        [SerializeField] private Camera _previewCamera;

        public CCharacterPreviewAnimator CharacterPreviewAnimator => _characterPreviewAnimator;
        public CCharacterPreviewButtons CharacterPreviewButtons => _characterPreviewButtons;
        public CCharacterPreviewModel CharacterPreviewModel => _characterPreviewModel;
        public Camera PreviewCamera => _previewCamera;

        public readonly ReactiveCommand SelectCharacter = new();

        public readonly CompositeDisposable ButtonDisposable = new();
    }
}