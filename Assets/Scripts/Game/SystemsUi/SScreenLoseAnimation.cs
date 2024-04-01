using CodeBase.ECSCore;
using CodeBase.Game.ComponentsUi;
using DG.Tweening;
using UnityEngine;

namespace CodeBase.Game.SystemsUi
{
    public class SScreenLoseAnimation : SystemComponent<CPrintResultText>
    {
        private const float Offset = 0.1f;

        protected override void OnEnableComponent(CPrintResultText component)
        {
            base.OnEnableComponent(component);

            StartAnimation(component);
        }

        private void StartAnimation(CPrintResultText component)
        {
            float delay = 0f;
            
            InAnimation(component, ref delay);
        }

        private void InAnimation(CPrintResultText component, ref float delay)
        {
            foreach (Transform letter in component.Letters)
            {
                delay += Offset;

                DOTween.Sequence()
                    .AppendInterval(delay)
                    .Append(letter.DOScale(Vector3.one, 0.1f).From(Vector3.zero))
                    .Append(letter.DOPunchScale(Vector3.one * 0.25f, 0.25f, 1, 0.5f).SetEase(Ease.InSine))
                    .SetLink(component.gameObject);
            }
        }
    }
}