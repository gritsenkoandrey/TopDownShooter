using CodeBase.ECSCore;
using CodeBase.Game.Components;
using CodeBase.Game.Interfaces;
using CodeBase.Utils;

namespace CodeBase.Game.Systems
{
    public sealed class SBulletProvider : SystemComponent<CBullet>
    {
        protected override void OnUpdate()
        {
            base.OnUpdate();
            
            Entities.Foreach(Move);
        }

        private void Move(IBullet bullet) => bullet.Object.transform.position += bullet.Direction;
    }
}