using CodeBase.ECSCore;
using CodeBase.Game.Components;
using CodeBase.Utils;
using UnityEngine;

namespace CodeBase.Game.Systems
{
    public sealed class SBulletMove : SystemComponent<CBullet>
    {
        protected override void OnUpdate()
        {
            base.OnUpdate();
            
            Entities.Foreach(Move);
        }

        private void Move(CBullet bullet) => bullet.transform.position += bullet.Direction * Time.deltaTime;
    }
}