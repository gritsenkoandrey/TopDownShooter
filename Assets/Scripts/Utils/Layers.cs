using UnityEngine;

namespace CodeBase.Utils
{
    public static class Layers
    {
        public static int Ground => LayerMask.GetMask(nameof(Ground));
        public static int Wall => LayerMask.GetMask(nameof(Wall));
    }
}