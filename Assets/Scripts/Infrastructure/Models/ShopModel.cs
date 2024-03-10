using CodeBase.Game.Enums;
using CodeBase.Infrastructure.Progress;
using CodeBase.Infrastructure.Progress.Data;
using CodeBase.Infrastructure.StaticData;
using CodeBase.Infrastructure.StaticData.Data;
using JetBrains.Annotations;

namespace CodeBase.Infrastructure.Models
{
    [UsedImplicitly(ImplicitUseKindFlags.InstantiatedNoFixedConstructorSignature)]
    public sealed class ShopModel
    {
        private readonly IProgressService _progressService;
        private readonly IStaticDataService _staticDataService;

        private Shop Shop => _progressService.ShopData.Data.Value;

        public ShopModel(IProgressService  progressService, IStaticDataService staticDataService)
        {
            _progressService = progressService;
            _staticDataService = staticDataService;
        }

        public void Buy(WeaponType type)
        {
            if (CanBuy(type))
            {
                _progressService.MoneyData.Data.Value -= GetCost(type);

                Shop.Add(type);
            }
        }
        public void Buy(SkinType type)
        {
            if (CanBuy(type))
            {
                _progressService.MoneyData.Data.Value -= GetCost(type);
                
                Shop.Add(type);
            }
        }

        public bool IsBuy(WeaponType type) => Shop.Contains(type);
        public bool IsBuy(SkinType type) => Shop.Contains(type);

        public bool CanBuy(WeaponType type)
        {
            if (IsBuy(type))
            {
                return false;
            }

            int cost = GetCost(type);
            
            if (_progressService.MoneyData.Data.Value >= cost)
            {
                return true;
            }
            
            return false;
        }
        public bool CanBuy(SkinType type)
        {
            if (IsBuy(type))
            {
                return false;
            }

            int cost = GetCost(type);
            
            if (_progressService.MoneyData.Data.Value >= cost)
            {
                return true;
            }
            
            return false;
        }

        public int GetCost(WeaponType type)
        {
            int cost = 0;
            
            foreach (WeaponShopData data in _staticDataService.ShopData().WeaponsShopData)
            {
                if (data.WeaponType == type)
                {
                    cost = data.Cost;
                    
                    break;
                }
            }

            return cost;
        }
        public int GetCost(SkinType type)
        {
            int cost = 0;

            foreach (SkinShopData data in _staticDataService.ShopData().SkinsShopData)
            {
                if (data.SkinType == type)
                {
                    cost = data.Cost;
                    
                    break;
                }
            }

            return cost;
        }
    }
}