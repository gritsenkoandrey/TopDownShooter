using System;
using System.Text;
using System.Threading;
using CodeBase.Utils.CustomDebug;
using Cysharp.Threading.Tasks;
using TMPro;
using UnityEngine;

namespace CodeBase.Infrastructure.Curtain
{
    public sealed class LoadingCurtain : MonoBehaviour, ILoadingCurtainService
    {
        [SerializeField] private CanvasGroup _canvasGroup;
        [SerializeField] private TextMeshProUGUI _loadingText;
        [SerializeField] private float _durationCanvas;
        [SerializeField] private float _durationPrintText;

        private readonly CancellationTokenSource _cancellationTokenSource = new ();
        private readonly StringBuilder _stringBuilder = new ();

        private void OnDestroy()
        {
            _cancellationTokenSource.Cancel();
            _cancellationTokenSource.Dispose();
        }

        void ILoadingCurtainService.Show()
        {
            gameObject.SetActive(true);
            
            _loadingText.text = string.Empty;
            _canvasGroup.alpha = 1f;
        }

        void ILoadingCurtainService.Hide() => ShowAnimation().Forget();

        private async UniTaskVoid ShowAnimation()
        {
            try
            {
                int index = 0;
                float elapsed = 0f;

                while (index < 4)
                {
                    UpdateText(ref index);
                    
                    await UniTask.Delay(TimeSpan.FromSeconds(_durationPrintText), cancellationToken: _cancellationTokenSource.Token);
                }

                while (elapsed < _durationCanvas)
                {
                    elapsed += Time.deltaTime;
                    _canvasGroup.alpha = Mathf.Lerp(1f, 0f, elapsed / _durationCanvas);
                    
                    await UniTask.Yield(PlayerLoopTiming.Update, cancellationToken: _cancellationTokenSource.Token);
                }
                
                gameObject.SetActive(false);
            }
            catch (OperationCanceledException exception)
            {
                CustomDebug.Log(exception.Message);
            }
        }

        private void UpdateText(ref int index)
        {
            _stringBuilder.Clear();
            
            int dotCount = index % 4;
            
            for (int i = 0; i < dotCount; i++)
            {
                _stringBuilder.Append('.');
            }
            
            _loadingText.text = _stringBuilder.ToString();
            
            index++;
        }
    }
}