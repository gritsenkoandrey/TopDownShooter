using CodeBase.ECSCore;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.Game.ComponentsUi
{
    public sealed class CCharacterPreviewChanger : EntityComponent<CCharacterPreviewChanger>
    {
        [SerializeField] private CCharacterPreviewModel _characterPreviewModel;
        [SerializeField] private Button _upButton;
        [SerializeField] private Button _downButton;
        [SerializeField] private Button _leftButton;
        [SerializeField] private Button _rightButton;

        public CCharacterPreviewModel CCharacterPreviewModel => _characterPreviewModel;
        public Button UpButton => _upButton;
        public Button DownButton => _downButton;
        public Button LeftButton => _leftButton;
        public Button RightButton => _rightButton;
    }
}