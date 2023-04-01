using System.Collections;
using TMPro;
using UnityEngine;

namespace CodeBase.Infrastructure.Curtain
{
    public sealed class LoadingCurtain : MonoBehaviour
    {
        [SerializeField] private CanvasGroup _canvasGroup;
        [SerializeField] private TextMeshProUGUI _loadingText;
        [SerializeField] private TextMeshProUGUI _dotsText;

        public void Show()
        {
            gameObject.SetActive(true);

            _canvasGroup.alpha = 1f;
        }

        public void Hide()
        {
            StartCoroutine(FadeIn());
        }

        private IEnumerator FadeIn()
        {
            _loadingText.text = "Loading";
            
            int i = 0;
            
            while (i < 6)
            {
                string dots = ".";
                
                if (i % 3 == 1)
                {
                    dots += ".";
                }
                else if (i % 3 == 2)
                {
                    dots += "..";
                }
            
                _dotsText.text = dots;
                
                i++;
                
                yield return new WaitForSeconds(0.2f);
            }

            _canvasGroup.alpha = 0f;
            _loadingText.text = "";
            _dotsText.text = "";
            
            gameObject.SetActive(false);
        }
    }
}