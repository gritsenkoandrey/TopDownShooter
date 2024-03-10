using CodeBase.ECSCore;
using CodeBase.Game.ComponentsUi;
using UnityEngine;

namespace CodeBase.Game.Components
{
    public sealed class CBodyMediator : EntityComponent<CBodyMediator>
    {
        [SerializeField] private SkinData[] _skins;
        public SkinData[] Skins => _skins;
    }
}