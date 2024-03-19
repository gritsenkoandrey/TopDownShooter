using Cysharp.Threading.Tasks;
using DG.Tweening;
using UniRx;

namespace CodeBase.UI.Screens
{
    public sealed class LobbyScreen : BaseScreen
    {
        private protected override void OnEnable()
        {
            base.OnEnable();

            _button
                .OnClickAsObservable()
                .First()
                .Subscribe(_ => Hide().Forget())
                .AddTo(LifeTimeDisposable);
        }

        public override ScreenType GetScreenType() => ScreenType.Lobby;

        private protected override async UniTask Hide()
        {
            await base.Hide();
            
            await FadeCanvas(1f, 0f).AsyncWaitForCompletion().AsUniTask();
            
            CloseScreen.Execute();
        }
    }
}