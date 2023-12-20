using CodeBase.ECSCore;
using UnityEngine;

namespace CodeBase.Game.Components
{
    public sealed class CBodyMediator : EntityComponent<CBodyMediator>
    {
        [SerializeField] private GameObject[] _heads;
        [SerializeField] private GameObject[] _bodies;

        public GameObject[] Heads => _heads;
        public GameObject[] Bodies => _bodies;
    }
}