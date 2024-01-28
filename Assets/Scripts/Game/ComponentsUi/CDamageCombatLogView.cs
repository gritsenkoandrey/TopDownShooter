using CodeBase.ECSCore;
using CodeBase.Game.Interfaces;
using TMPro;
using UnityEngine;

namespace CodeBase.Game.ComponentsUi
{
    public sealed class CDamageCombatLogView : EntityComponent<CDamageCombatLogView>
    {
        [SerializeField] private TextMeshProUGUI _text;
        [SerializeField] private CanvasGroup _canvasGroup;
        [SerializeField] private int _points;
        [SerializeField] private float _offset;

        public TextMeshProUGUI Text => _text;
        public CanvasGroup CanvasGroup => _canvasGroup;
        public int Points => _points;
        public float Offset => _offset;

        public readonly CombatLogSettings Settings = new ();
    }

    public sealed class CombatLogSettings
    {
        public ITarget Target;
        public bool IsActive;
        public int Index;
        public Vector3 From;
        public Vector3 To;
        public Vector3 Center;
    }
}