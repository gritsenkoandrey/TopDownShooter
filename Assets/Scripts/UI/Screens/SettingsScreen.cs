using CodeBase.Infrastructure.GUI;
using CodeBase.Infrastructure.Models;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UniRx;
using VContainer;

namespace CodeBase.UI.Screens
{
    public sealed class SettingsScreen : BaseScreen
    {
        private IGuiService _guiService;
        
        private Tween _tween;

        [Inject]
        private void Construct(IGuiService guiService)
        {
            _guiService = guiService;
        }

        private protected override void OnEnable()
        {
            base.OnEnable();

            _button
                .OnClickAsObservable()
                .First()
                .Subscribe(_ => Hide().Forget())
                .AddTo(LifeTimeDisposable);
            
            Show().Forget();
        }
        
        public override ScreenType GetScreenType() => ScreenType.Settings;

        private protected override async UniTask Show()
        {
            await base.Show();
            
            _tween = BounceButton();
        }

        private protected override async UniTask Hide()
        {
            _tween?.Kill();
            
            await base.Hide();
            
            await FadeCanvas(1f, 0f).AsyncWaitForCompletion().AsUniTask();
            
            _guiService.Pop();
            
            CloseScreen.Execute();
        }
    }
}