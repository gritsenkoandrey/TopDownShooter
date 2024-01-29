using UnityEngine;

namespace CodeBase.Utils
{
    public static class Animations
    {
        public static int Velocity => Animator.StringToHash(nameof(Velocity));
        public static int Shoot => Animator.StringToHash(nameof(Shoot));
        public static int Death => Animator.StringToHash(nameof(Death));
        public static int Victory => Animator.StringToHash(nameof(Victory));
        public static int Preview => Animator.StringToHash(nameof(Preview));
        public static int PreviewBlend => Animator.StringToHash(nameof(PreviewBlend));
    }
}