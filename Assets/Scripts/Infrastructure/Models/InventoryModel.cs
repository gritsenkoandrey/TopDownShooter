using CodeBase.Game.Weapon;
using UniRx;

namespace CodeBase.Infrastructure.Models
{
    public sealed class InventoryModel
    {
        public readonly ReactiveProperty<WeaponType> SelectedWeapon = new();
        public readonly ReactiveProperty<int> WeaponIndex = new(0);
        public readonly ReactiveProperty<int> EquipmentIndex = new(0);
        public readonly ReactiveProperty<int> ClipCount = new();
        public readonly ReactiveProperty<bool> IsReloading = new();
    }
}