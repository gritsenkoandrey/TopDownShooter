using System;
using CodeBase.ECSCore;
using CodeBase.Game.ComponentsUi;
using CodeBase.Infrastructure.Haptic;
using CodeBase.Utils;
using UniRx;
using VContainer;

namespace CodeBase.Game.SystemsUi
{
    public sealed class SHapticButton : SystemComponent<CHapticButton>
    {
        private IHapticService _hapticService;

        [Inject]
        private void Construct(IHapticService hapticService)
        {
            _hapticService = hapticService;
        }
        
        protected override void OnEnableComponent(CHapticButton component)
        {
            base.OnEnableComponent(component);

            component.Button
                .OnClickAsObservable()
                .ThrottleFirst(TimeSpan.FromSeconds(ButtonSettings.DelayClick))
                .Subscribe(_ => _hapticService.Play(component.HapticType))
                .AddTo(component.LifetimeDisposable);
        }
    }
}