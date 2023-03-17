using CodeBase.ECSCore;
using CodeBase.Infrastructure.Input;
using UniRx;
using UnityEngine;

namespace CodeBase.Game.Components
{
    public sealed class CInput : EntityComponent<CInput>
    {
        [SerializeField] private Joystick _joystick;

        public IInput Input => _joystick;

        public ReactiveCommand UpdateInput { get; } = new ();
        
        protected override void OnEntityCreate() { }
        protected override void OnEntityEnable() { }
        protected override void OnEntityDisable() { }
    }
}