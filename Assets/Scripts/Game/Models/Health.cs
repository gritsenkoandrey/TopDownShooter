using CodeBase.Utils;
using UniRx;
using UnityEngine;

namespace CodeBase.Game.Models
{
    public sealed class Health
    {
        public int MaxHealth { get; private set; }
        public int BaseHealth { get; private set; }
        public bool IsAlive => CurrentHealth.Value > 0;
        public ReactiveProperty<int> CurrentHealth { get; } = new();

        public void SetMaxHealth(int maxHealth) => MaxHealth = maxHealth;
        public void SetBaseHealth(int baseHealth) => BaseHealth = baseHealth;
        
        public override string ToString() => string
            .Format(FormatText.Health, Mathf.Clamp(CurrentHealth.Value, 0, MaxHealth).ToString(), MaxHealth.ToString());
    }
}