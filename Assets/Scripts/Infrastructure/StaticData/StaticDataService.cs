using System.Collections.Generic;
using System.Linq;
using CodeBase.Game.Enums;
using CodeBase.Infrastructure.StaticData.Data;
using CodeBase.UI;
using UnityEngine;

namespace CodeBase.Infrastructure.StaticData
{
    public sealed class StaticDataService : IStaticDataService
    {
        private const string ZombieDataPath = "StaticData/ZombieData";
        private const string ScreenDataPath = "StaticData/ScreenData";
        private const string CharacterDataPath = "StaticData/CharacterData/CharacterData";

        private Dictionary<ZombieType, ZombieData> _monsters;
        private Dictionary<ScreenType, ScreenData> _screens;
        private CharacterData _character;

        public void Load()
        {
            _monsters = Resources
                .LoadAll<ZombieData>(ZombieDataPath)
                .ToDictionary(data => data.ZombieType, data => data);

            _screens = Resources
                .LoadAll<ScreenData>(ScreenDataPath)
                .ToDictionary(data => data.ScreenType, data => data);

            _character = Resources.Load<CharacterData>(CharacterDataPath);
        }

        public ZombieData ZombieData(ZombieType type) => 
            _monsters.TryGetValue(type, out ZombieData staticData) ? staticData : null;
        
        public ScreenData ScreenData(ScreenType type) => 
            _screens.TryGetValue(type, out ScreenData staticData) ? staticData : null;

        public CharacterData CharacterData() => _character;
    }
}