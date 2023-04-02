using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.Infrastructure.Curtain
{
    public sealed class LoadingCurtain : MonoBehaviour, ILoadingCurtainService
    {
        [SerializeField] private CanvasGroup _canvasGroup;
        [SerializeField] private Image _loading;

        void ILoadingCurtainService.Show()
        {
            gameObject.SetActive(true);
            
            _canvasGroup.alpha = 1f;
        }

        async UniTask ILoadingCurtainService.Hide()
        {
            await _loading
                .DOFillAmount(1f, 1f)
                .From(0f)
                .SetEase(Ease.Linear)
                .AsyncWaitForCompletion();

            await _canvasGroup
                .DOFade(0f, 0.1f)
                .From(1f)
                .SetEase(Ease.Linear)
                .AsyncWaitForCompletion();
            
            gameObject.SetActive(false);
        }
    }
}