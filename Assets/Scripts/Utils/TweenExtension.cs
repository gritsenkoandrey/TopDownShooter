using DG.Tweening;
using UnityEngine;

namespace CodeBase.Utils
{
    public static class TweenExtension
    {
        public static Tween PunchTransform(this Transform transform)
        {
            return transform
                .DOPunchScale(Vector3.one * 0.25f, 0.25f, 1, 0.5f)
                .SetEase(Ease.InSine)
                .SetRelative()
                .OnComplete(() => transform.localScale = Vector3.one);
        }
    }
}