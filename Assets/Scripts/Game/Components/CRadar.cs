using CodeBase.ECSCore;
using UniRx;
using UnityEngine;

namespace CodeBase.Game.Components
{
    public sealed class CRadar : EntityComponent<CRadar>
    {
        [SerializeField] private LineRenderer _lineRenderer;
        [SerializeField] private float _scale = 0.02f;
        [SerializeField] private float _width = 0.2f;

        public LineRenderer LineRenderer => _lineRenderer;
        public float Scale => _scale;
        public float Width => _width;
        public float Radius { get; set; }
        
        public ReactiveCommand Draw { get; } = new();
        public ReactiveCommand Clear { get; } = new();

        protected override void OnEntityCreate() { }
        protected override void OnEntityEnable() { }
        protected override void OnEntityDisable() { }
    }
}