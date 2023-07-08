using Cysharp.Threading.Tasks;
using DG.Tweening;
using TMPro;
using UnityEngine;

namespace CodeBase.Infrastructure.Curtain
{
    public sealed class LoadingCurtain : MonoBehaviour, ILoadingCurtainService
    {
        [SerializeField] private CanvasGroup _canvasGroup;
        [SerializeField] private TextMeshProUGUI _loadingText;

        void ILoadingCurtainService.Show()
        {
            gameObject.SetActive(true);
            
            _canvasGroup.alpha = 1f;
        }

        async UniTask ILoadingCurtainService.Hide()
        {
            await ShowLoadingText().AsyncWaitForCompletion();
            await FadeScreen().AsyncWaitForCompletion();
            
            gameObject.SetActive(false);
        }

        private Tween ShowLoadingText()
        {
            int index = 1;

            _loadingText.text = "";

            return DOVirtual.DelayedCall(0.5f, () => UpdateText(ref index)).SetLoops(3);
        }

        private void UpdateText(ref int index)
        {
            _loadingText.text = "";
            _loadingText.text += (index % 3) switch { 1 => ".", 2 => "..", _ => "..." };
                    
            index++;
        }

        private Tween FadeScreen()
        {
            return _canvasGroup.DOFade(0f, 0.1f).From(1f).SetDelay(0.5f).SetEase(Ease.Linear);
        }
    }
}