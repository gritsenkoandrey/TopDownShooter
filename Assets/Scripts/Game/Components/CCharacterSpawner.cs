using CodeBase.ECSCore;
using UnityEngine;

namespace CodeBase.Game.Components
{
    public sealed class CCharacterSpawner : EntityComponent<CCharacterSpawner>
    {
        public Vector3 Position => transform.position;
    }
}