using System;
using CodeBase.ECSCore;
using CodeBase.Game.Enums;
using UnityEngine;

namespace CodeBase.Game.Components
{
    public sealed class CBodyMediator : EntityComponent<CBodyMediator>
    {
        [SerializeField] private SkinData[] _skins;
        public SkinData[] Skins => _skins;
    }

    [Serializable]
    public struct SkinData
    {
        public SkinType Type;
        public VisualData Data;
    }

    [Serializable]
    public struct VisualData
    {
        public GameObject[] Visual;
    }
}