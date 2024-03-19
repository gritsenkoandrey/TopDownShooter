using Cysharp.Threading.Tasks;
using DG.Tweening;
using UniRx;

namespace CodeBase.UI.Screens
{
    public sealed class WinScreen : BaseScreen
    {
        private Tween _tween;

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
        
        public override ScreenType GetScreenType() => ScreenType.Win;

        private protected override async UniTask Show()
        {
            await base.Show();

            _tween = BounceButton();
        }

        private protected override async UniTask Hide()
        {
            _tween?.Kill();

            await base.Hide();
            
            CloseScreen.Execute();
        }
    }
}