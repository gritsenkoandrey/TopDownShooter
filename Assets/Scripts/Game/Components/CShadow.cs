using CodeBase.ECSCore;
using UnityEngine;

namespace CodeBase.Game.Components
{
    public sealed class CShadow : EntityComponent<CShadow>
    {
        [SerializeField] private GameObject _shadow;

        public void SetActive(bool isActive) => _shadow.SetActive(isActive);
    }
}