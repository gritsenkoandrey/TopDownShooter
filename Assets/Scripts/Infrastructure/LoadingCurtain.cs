using System.Collections;
using TMPro;
using UnityEngine;

namespace CodeBase.Infrastructure
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
            StartCoroutine(PrintDots());
        }

        private IEnumerator PrintDots()
        {
            _loadingText.text = "Loading";
            
            int i = 0;
            
            while (_canvasGroup.alpha > 0.9f)
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

            _loadingText.text = "";
            _dotsText.text = "";
        }

        private IEnumerator FadeIn()
        {
            yield return new WaitForSeconds(1.5f);
            
            while (_canvasGroup.alpha > 0f)
            {
                _canvasGroup.alpha -= 0.02f;
                
                yield return new WaitForSeconds(0.02f);
            }
            
            gameObject.SetActive(false);
        }
    }
}