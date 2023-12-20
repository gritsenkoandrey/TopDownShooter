using UnityEngine;

namespace CodeBase.Utils
{
    public static class Animations
    {
        public static int Velocity => Animator.StringToHash(nameof(Velocity));
        public static int Shoot => Animator.StringToHash(nameof(Shoot));
        public static int Hit => Animator.StringToHash(nameof(Hit));
        public static int Death => Animator.StringToHash(nameof(Death));
        public static int Victory => Animator.StringToHash(nameof(Victory));
        public static int Preview => Animator.StringToHash(nameof(Preview));
        public static int RunBlend => Animator.StringToHash(nameof(RunBlend));
        public static int IdleBlend => Animator.StringToHash(nameof(IdleBlend));
        public static int ShootBlend => Animator.StringToHash(nameof(ShootBlend));
        public static int DeathBlend => Animator.StringToHash(nameof(DeathBlend));
        public static int PreviewBlend => Animator.StringToHash(nameof(PreviewBlend));
    }
}