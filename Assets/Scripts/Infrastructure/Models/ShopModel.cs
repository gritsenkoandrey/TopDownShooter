using CodeBase.Game.Enums;
using CodeBase.Infrastructure.AssetData;
using CodeBase.Infrastructure.Progress;
using CodeBase.Infrastructure.Progress.Data;
using JetBrains.Annotations;

namespace CodeBase.Infrastructure.Models
{
    [UsedImplicitly(ImplicitUseKindFlags.InstantiatedNoFixedConstructorSignature)]
    public sealed class ShopModel
    {
        private readonly IProgressService _progressService;
        private readonly IAssetService _assetService;

        private Shop Shop => _progressService.ShopData.Data.Value;

        public ShopModel(IProgressService  progressService, IAssetService assetService)
        {
            _progressService = progressService;
            _assetService = assetService;
        }

        public void BuyWeapon(WeaponType type)
        {
            if (CanBuyWeapon(type))
            {
                Shop.Add(type);
            }
        }

        public void BuySkin(int skin)
        {
            if (CanBuySkin(skin))
            {
                Shop.Add(skin);
            }
        }

        public bool IsBuyWeapon(WeaponType type) => Shop.Contains(type);

        public bool IsBuySkin(int skin) => Shop.Contains(skin);

        private bool CanBuyWeapon(WeaponType type)
        {
            if (IsBuyWeapon(type))
            {
                return false;
            }

            return true;
        }
        
        private bool CanBuySkin(int skin)
        {
            if (IsBuySkin(skin))
            {
                return false;
            }
            
            return true;
        }
    }
}