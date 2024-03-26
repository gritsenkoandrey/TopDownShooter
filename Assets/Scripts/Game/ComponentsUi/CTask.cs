using CodeBase.ECSCore;
using CodeBase.Infrastructure.DailyTasks;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.Game.ComponentsUi
{
    public sealed class CTask : EntityComponent<CTask>
    {
        [SerializeField] private TextMeshProUGUI _questText;
        [SerializeField] private TextMeshProUGUI _progressText;
        [SerializeField] private TextMeshProUGUI _buttonText;
        [SerializeField] private Image _fillProgress;
        [SerializeField] private Button _getButton;
        [SerializeField] private Color _colorDone;

        public TextMeshProUGUI QuestText => _questText;
        public TextMeshProUGUI ProgressText => _progressText;
        public TextMeshProUGUI ButtonText => _buttonText;
        public Image FillProgress => _fillProgress;
        public Button GetButton => _getButton;
        public Color ColorDone => _colorDone;

        public readonly IReactiveProperty<Task> Task = new ReactiveProperty<Task>();
    }
}