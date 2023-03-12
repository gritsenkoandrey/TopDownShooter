using UnityEngine;

namespace CodeBase.Utils
{
    public static class Animations
    {
        public static int Velocity => Animator.StringToHash(nameof(Velocity));
        public static int Shoot => Animator.StringToHash(nameof(Shoot));
        public static int Hit => Animator.StringToHash(nameof(Hit));
        public static int RunBlend => Animator.StringToHash(nameof(RunBlend));
        public static int IdleBlend => Animator.StringToHash(nameof(IdleBlend));
        public static int ShootBlend => Animator.StringToHash(nameof(ShootBlend));
    }
}