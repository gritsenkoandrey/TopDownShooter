using CodeBase.ECSCore;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.Game.ComponentsUi
{
    public sealed class CCharacterPreviewButtons : EntityComponent<CCharacterPreviewButtons>
    {
        [SerializeField] private Button _upButton;
        [SerializeField] private Button _downButton;
        [SerializeField] private Button _leftButton;
        [SerializeField] private Button _rightButton;

        public Button UpButton => _upButton;
        public Button DownButton => _downButton;
        public Button LeftButton => _leftButton;
        public Button RightButton => _rightButton;
        
        public ReactiveCommand SelectCharacter { get; } = new();
        public CompositeDisposable ButtonDisposable { get; } = new();
    }
}