using CodeBase.Utils;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.UI.Screens
{
    public sealed class LobbyScreen : BaseScreen
    {
        [SerializeField] private Button _button;
        [SerializeField] private TextMeshProUGUI _text;

        private protected override void OnEnable()
        {
            base.OnEnable();

            _button
                .OnClickAsObservable()
                .First()
                .Subscribe(_ => StartGame().Forget())
                .AddTo(this);
        }

        private async UniTaskVoid StartGame()
        {
            _text.transform.PunchTransform();
            
            await FadeCanvas(1f, 0f, 0.25f).AsyncWaitForCompletion().AsUniTask();

            ChangeState.Execute();
        }
    }
}