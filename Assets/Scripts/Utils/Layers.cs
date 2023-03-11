using UnityEngine;

namespace CodeBase.Utils
{
    public static class Layers
    {
        private const string LayerGround = "Ground";
        private const string LayerCharacter = "Character";

        public static int Ground { get; }
        public static int Character { get; }

        static Layers()
        {
            Ground = LayerMask.GetMask(LayerGround);
            Character = LayerMask.NameToLayer(LayerCharacter);
        }
    }
}