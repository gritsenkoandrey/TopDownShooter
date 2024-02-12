using CodeBase.ECSCore;
using CodeBase.Game.ComponentsUi;
using DG.Tweening;
using UnityEngine;

namespace CodeBase.Game.SystemsUi
{
    public class SPrintResultText : SystemComponent<CPrintResultText>
    {
        private const float Offset = 0.1f;

        protected override void OnEnableComponent(CPrintResultText component)
        {
            base.OnEnableComponent(component);

            StartAnimation(component);
        }

        protected override void OnDisableComponent(CPrintResultText component)
        {
            base.OnDisableComponent(component);

            KillTwins(component);
        }

        private void StartAnimation(CPrintResultText component)
        {
            float delay = 0f;
            
            InAnimation(component, ref delay);
        }

        private void InAnimation(CPrintResultText component, ref float delay)
        {
            foreach (Letter letter in component.Letters)
            {
                delay += Offset;

                letter.Sequence = DOTween.Sequence()
                    .AppendInterval(delay)
                    .Append(letter.Transform.DOScale(Vector3.one, 0.1f).From(Vector3.zero))
                    .Append(letter.Transform
                        .DOPunchScale(Vector3.one * 0.25f, 0.25f, 1, 0.5f)
                        .SetEase(Ease.InSine));
            }
        }

        private void OutAnimation(CPrintResultText component, ref float delay)
        {
            foreach (Letter letter in component.Letters)
            {
                delay -= Offset;
                
                letter.Sequence = DOTween.Sequence()
                    .AppendInterval(delay)
                    .Append(letter.Transform
                        .DOPunchScale(Vector3.one * 0.25f, 0.25f, 1, 0.5f)
                        .SetEase(Ease.InSine))
                    .Join(letter.CanvasGroup.DOFade(0f, 0.5f));
            }
        }

        private bool AllTwinsCompleted(CPrintResultText component)
        {
            for (int i = 0; i < component.Letters.Length; i++)
            {
                if (component.Letters[i].Sequence is { active: true })
                {
                    return false;
                }
            }

            return true;
        }

        private void KillTwins(CPrintResultText component)
        {
            for (int i = 0; i < component.Letters.Length; i++)
            {
                component.Letters[i].Sequence?.Kill();
            }
        }
    }
}