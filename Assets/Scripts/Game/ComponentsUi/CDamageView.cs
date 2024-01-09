using CodeBase.ECSCore;
using CodeBase.Game.Interfaces;
using TMPro;
using UnityEngine;

namespace CodeBase.Game.ComponentsUi
{
    public sealed class CDamageView : EntityComponent<CDamageView>
    {
        [SerializeField] private TextMeshProUGUI _text;
        [SerializeField] private CanvasGroup _canvasGroup;
        [SerializeField] private int _points;
        [SerializeField] private float _offset;

        public TextMeshProUGUI Text => _text;
        public CanvasGroup CanvasGroup => _canvasGroup;
        public int Points => _points;
        public float Offset => _offset;

        public readonly DamageViewSettings Settings = new ();
    }

    public sealed class DamageViewSettings
    {
        public IEnemy Target;
        public bool IsActive;
        public int Index;
        public Vector3 From;
        public Vector3 To;
        public Vector3 Center;
    }
}