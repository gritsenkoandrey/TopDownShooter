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

        private int _index;

        void ILoadingCurtainService.Show()
        {
            gameObject.SetActive(true);
            
            _loadingText.text = string.Empty;
            _canvasGroup.alpha = 1f;
        }

        async UniTask ILoadingCurtainService.Hide()
        {
            await ShowLoadingText().AsyncWaitForCompletion().AsUniTask();
            await FadeCanvas().AsyncWaitForCompletion().AsUniTask();
            
            gameObject.SetActive(false);
        }

        private Tween ShowLoadingText()
        {
            _index = 1;
            _loadingText.text = string.Empty;

            return DOVirtual.DelayedCall(0.3f, UpdateText).SetLoops(3).SetLink(gameObject);
        }

        private Tween FadeCanvas()
        {
            return _canvasGroup.DOFade(0f, 0.25f).From(1f).SetDelay(0.5f).SetEase(Ease.Linear).SetLink(gameObject);
        }

        private void UpdateText()
        {
            _loadingText.text = string.Empty;
            _loadingText.text += (_index % 3) switch { 1 => ".", 2 => "..", _ => "..." };
                    
            _index++;
        }
    }
}