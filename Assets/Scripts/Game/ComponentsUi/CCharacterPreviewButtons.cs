using CodeBase.ECSCore;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.Game.ComponentsUi
{
    public sealed class CCharacterPreviewButtons : EntityComponent<CCharacterPreviewButtons>
    {
        [SerializeField] private Button _leftButton;
        [SerializeField] private Button _rightButton;

        public Button LeftButton => _leftButton;
        public Button RightButton => _rightButton;
    }
}