using UnityEngine;

namespace CodeBase.Utils
{
    public static class Layers
    {
        private const string LayerGround = "Ground";
        private const string LayerCharacter = "Character";
        private const string LayerEnemy = "Enemy";
        private const string LayerBullet = "Bullet";

        public static int Ground { get; }
        public static int Character { get; }
        public static int Enemy { get; }
        public static int Bullet { get; }

        static Layers()
        {
            Ground = LayerMask.GetMask(LayerGround);
            
            Character = LayerMask.NameToLayer(LayerCharacter);
            Enemy = LayerMask.NameToLayer(LayerEnemy);
            Bullet = LayerMask.NameToLayer(LayerBullet);
        }
    }
}