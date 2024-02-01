using CodeBase.Game.Weapon;
using JetBrains.Annotations;
using UniRx;

namespace CodeBase.Infrastructure.Models
{
    [UsedImplicitly(ImplicitUseKindFlags.InstantiatedNoFixedConstructorSignature)]
    public sealed class InventoryModel
    {
        public readonly ReactiveProperty<WeaponType> SelectedWeapon = new();
        public readonly ReactiveProperty<int> WeaponIndex = new(0);
        public readonly ReactiveProperty<int> EquipmentIndex = new(0);
        public readonly ReactiveProperty<int> ClipCount = new();
        public readonly ReactiveCommand<float> Reloading = new();
    }
}