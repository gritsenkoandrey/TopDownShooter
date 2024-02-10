using CodeBase.Game.Enums;
using CodeBase.Infrastructure.Progress;
using JetBrains.Annotations;
using UniRx;

namespace CodeBase.Infrastructure.Models
{
    [UsedImplicitly(ImplicitUseKindFlags.InstantiatedNoFixedConstructorSignature)]
    public sealed class InventoryModel
    {
        private readonly IProgressService _progressService;

        private WeaponType _selectedWeapon;
        
        public InventoryModel(IProgressService progressService)
        {
            _progressService = progressService;
        }

        public IReactiveProperty<int> ClipCount { get; } = new ReactiveProperty<int>();
        public IReactiveCommand<float> ReloadingWeapon { get; } = new ReactiveCommand<float>();

        public int GetEquipmentIndex() => _progressService.InventoryData.Data.Value.EquipmentIndex;
        public int GetWeaponIndex() => _progressService.InventoryData.Data.Value.WeaponIndex;
        public void SetEquipmentIndex(int index) => _progressService.InventoryData.Data.Value.EquipmentIndex = index;
        public void SetWeaponIndex(int index) => _progressService.InventoryData.Data.Value.WeaponIndex = index;
        public WeaponType GetSelectedWeapon() => _selectedWeapon;
        public void SetSelectedWeapon(WeaponType weaponType) => _selectedWeapon = weaponType;
    }
}