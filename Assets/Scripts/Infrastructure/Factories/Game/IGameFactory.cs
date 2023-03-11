using CodeBase.Infrastructure.Services;
using UnityEngine;

namespace CodeBase.Infrastructure.Factories.Game
{
    public interface IGameFactory : IService
    {
        public GameObject CurrentLevel { get; }
        public GameObject CreateLevel();
        public GameObject CreateCanvas();
    }
}