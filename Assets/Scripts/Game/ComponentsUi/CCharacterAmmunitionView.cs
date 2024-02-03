using CodeBase.ECSCore;
using TMPro;
using UnityEngine;

namespace CodeBase.Game.ComponentsUi
{
    public sealed class CCharacterAmmunitionView : EntityComponent<CCharacterAmmunitionView>
    {
        [SerializeField] private TextMeshProUGUI _ammunitionCount;
        
        public TextMeshProUGUI AmmunitionCount => _ammunitionCount;
    }
}