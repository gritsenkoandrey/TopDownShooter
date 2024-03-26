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
        public IReactiveCommand<int> CompleteLevel { get; } = new ReactiveCommand<int>();

        public void AddEnemy(IEnemy enemy) => _enemies.Add(enemy);
        public void RemoveEnemy(IEnemy enemy) => _enemies.Remove(enemy);
        public void SetCharacter(ICharacter character) => Character = character;
        public void SetLevel(ILevel level) => Level = level;
        
        public int CalculateLevelStar()
        {
            int index = 0;
            
            if (CharacterHaseFullHealth())
            {
                index++;
            }

            if (CharacterHasHalfHealth())
            {
                index++;
            }

            if (LevelCompletedHalfTime())
            {
                index++;
            }

            CompleteLevel.Execute(index);

            return index;
        }

        public void CleanUp()
        {
            CleanCharacter();
            CleanLevel();
            CleanEnemies();
        }

        private void CleanCharacter() => Character = null;
        private void CleanLevel() => Level = null;
        private void CleanEnemies() => _enemies.Clear();
        
        private bool CharacterHaseFullHealth() => Character.Health.CurrentHealth.Value == Character.Health.MaxHealth;
        private bool CharacterHasHalfHealth() => Character.Health.CurrentHealth.Value >= Character.Health.MaxHealth / 2;
        private bool LevelCompletedHalfTime() => Level.Time >= Level.MaxTime / 2;
    }
}