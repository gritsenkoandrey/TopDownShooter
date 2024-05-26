using CodeBase.ECSCore;
using CodeBase.Game.Components;
using CodeBase.Utils;

namespace CodeBase.Game.Systems
{
    public sealed class SExecuteWeaponAmmunition : SystemComponent<CWeapon>
    {
        protected override void OnUpdate()
        {
            base.OnUpdate();
            
            Entities.Foreach(Execute);
        }

        private void Execute(CWeapon component) => component.Weapon.Execute();
    }
}