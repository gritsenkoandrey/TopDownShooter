using CodeBase.Infrastructure.Progress;
using UniRx;
using UnityEngine;

namespace CodeBase.Game.Models
{
    public sealed class Health : IProgressReader
    {
        public int MaxHealth { get; set; }
        public int BaseHealth { get; set; }
        public bool IsAlive => CurrentHealth.Value > 0;
        public ReactiveProperty<int> CurrentHealth { get; } = new();

        void IProgressReader.Read(PlayerProgress progress)
        {
            MaxHealth = BaseHealth * progress.Stats.Health;
            CurrentHealth.SetValueAndForceNotify(MaxHealth);
        }

        public override string ToString() => $"{Mathf.Clamp(CurrentHealth.Value, 0, MaxHealth)}/{MaxHealth}";
    }
}