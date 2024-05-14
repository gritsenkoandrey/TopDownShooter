using CodeBase.ECSCore;
using CodeBase.Game.Components;
using CodeBase.Utils;
using UnityEngine;

namespace CodeBase.Game.Systems
{
    public sealed class SHealthRegeneration : SystemComponent<CHealth>
    {
        protected override void OnUpdate()
        {
            base.OnUpdate();
            
            Entities.Foreach(RegenerationHealth);
        }

        private void RegenerationHealth(CHealth component)
        {
            if (component.RegenerationHealth <= 0 || 
                component.IsAlive == false || 
                component.CurrentHealth.Value == component.MaxHealth)
            {
                return;
            }

            if (component.CurrentRegenerationInterval > 0f)
            {
                component.CurrentRegenerationInterval -= Time.deltaTime;
                
                return;
            }

            component.CurrentRegenerationInterval = component.RegenerationInterval;
            
            if (component.CurrentHealth.Value + component.RegenerationHealth > component.MaxHealth)
            {
                component.CurrentHealth.Value = component.MaxHealth;
            }
            else
            {
                component.CurrentHealth.Value += component.RegenerationHealth;
            }
        }
    }
}