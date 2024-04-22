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

        public IReactiveProperty<WeaponType> SelectedWeapon { get; } = new ReactiveProperty<WeaponType>();
        public IReactiveProperty<SkinType> SelectedSkin { get; } = new ReactiveProperty<SkinType>();
        public IReactiveProperty<int> IndexWeapon { get; } = new ReactiveProperty<int>();
        public IReactiveProperty<int> IndexSkin { get; } = new ReactiveProperty<int>();
        public IReactiveProperty<int> ClipCount { get; } = new ReactiveProperty<int>();
        public IReactiveCommand<float> ReloadingWeapon { get; } = new ReactiveCommand<float>();

        public InventoryModel(IProgressService progressService)
        {
            _progressService = progressService;
        }

        public int GetSkinIndex() => _progressService.InventoryData.Data.Value.EquipmentIndex;
        public int GetWeaponIndex() => _progressService.InventoryData.Data.Value.WeaponIndex;
        public void SetSkinIndex(int index) => _progressService.InventoryData.Data.Value.EquipmentIndex = index;
        public void SetWeaponIndex(int index) => _progressService.InventoryData.Data.Value.WeaponIndex = index;
    }
}