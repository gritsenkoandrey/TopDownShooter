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

        public CCharacterPreviewAnimator CharacterPreviewAnimator => _characterPreviewAnimator;
        public CCharacterPreviewButtons CharacterPreviewButtons => _characterPreviewButtons;
        public CCharacterPreviewModel CharacterPreviewModel => _characterPreviewModel;

        public readonly ReactiveCommand SelectCharacter = new();

        public readonly CompositeDisposable ButtonDisposable = new();
    }
}