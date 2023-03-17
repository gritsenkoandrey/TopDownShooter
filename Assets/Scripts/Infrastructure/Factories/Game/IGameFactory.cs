using CodeBase.Game.Interfaces;
using CodeBase.Game.LevelData;
using CodeBase.Infrastructure.Services;

namespace CodeBase.Infrastructure.Factories.Game
{
    public interface IGameFactory : IService
    {
        public Level CurrentLevel { get; }
        public ICharacter CurrentCharacter { get; }
        public Level CreateLevel();
        public ICharacter CreateCharacter();
        public void CleanUp();
    }
}