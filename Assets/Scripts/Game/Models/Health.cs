using UniRx;
using UnityEngine;

namespace CodeBase.Game.Models
{
    public sealed class Health
    {
        public int MaxHealth { get; set; }
        public int BaseHealth { get; set; }
        public bool IsAlive => CurrentHealth.Value > 0;
        public ReactiveProperty<int> CurrentHealth { get; } = new();

        public override string ToString() => $"{Mathf.Clamp(CurrentHealth.Value, 0, MaxHealth)}/{MaxHealth}";
    }
}