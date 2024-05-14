using CodeBase.ECSCore;
using CodeBase.Utils;
using UniRx;
using UnityEngine;

namespace CodeBase.Game.Components
{
    public sealed class CHealth : EntityComponent<CHealth>
    {
        public int MaxHealth { get; private set; }
        public int BaseHealth { get; private set; }
        public int RegenerationHealth { get; private set; }
        public float RegenerationInterval { get; private set; }
        public float CurrentRegenerationInterval { get; set; }
        public bool IsAlive => CurrentHealth.Value > 0;
        public ReactiveProperty<int> CurrentHealth { get; } = new();

        public void SetMaxHealth(int maxHealth) => MaxHealth = maxHealth;
        public void SetBaseHealth(int baseHealth) => BaseHealth = baseHealth;
        public void SetRegenerationHealth(int regenerationHealth, float regenerationInterval)
        {
            RegenerationHealth = regenerationHealth;
            RegenerationInterval = regenerationInterval;
            CurrentRegenerationInterval = regenerationInterval;
        }
        
        public override string ToString() => string
            .Format(FormatText.Health, Mathf.Clamp(CurrentHealth.Value, 0, MaxHealth).ToString(), MaxHealth.ToString());
    }
}