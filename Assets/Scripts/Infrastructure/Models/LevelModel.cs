using CodeBase.Game.Interfaces;
using JetBrains.Annotations;
using UniRx;

namespace CodeBase.Infrastructure.Models
{
    [UsedImplicitly(ImplicitUseKindFlags.InstantiatedNoFixedConstructorSignature)]
    public sealed class LevelModel
    {
        private readonly ReactiveCollection<IEnemy> _enemies = new ();
        
        public ICharacter Character { get; private set; }
        public ILevel Level { get; private set; }
        public IReadOnlyReactiveCollection<IEnemy> Enemies => _enemies;
        
        public void AddEnemy(IEnemy enemy) => _enemies.Add(enemy);
        public void RemoveEnemy(IEnemy enemy) => _enemies.Remove(enemy);
        public void SetCharacter(ICharacter character) => Character = character;
        public void SetLevel(ILevel level) => Level = level;

        public void CleanUp()
        {
            CleanCharacter();
            CleanLevel();
            CleanEnemies();
        }

        private void CleanCharacter() => Character = null;
        private void CleanLevel() => Level = null;
        private void CleanEnemies() => _enemies.Clear();
    }
}