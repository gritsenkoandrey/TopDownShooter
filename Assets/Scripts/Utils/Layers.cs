using UnityEngine;

namespace CodeBase.Utils
{
    public static class Layers
    {
        public static int Ground => LayerMask.GetMask(nameof(Ground));
        public static int Character => LayerMask.NameToLayer(nameof(Character));
        public static int Enemy => LayerMask.NameToLayer(nameof(Enemy));
        public static int Bullet => LayerMask.NameToLayer(nameof(Bullet));
    }
}