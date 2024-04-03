using System.Collections.Generic;
using CodeBase.Game.Interfaces;
using JetBrains.Annotations;
using UniRx;

namespace CodeBase.Infrastructure.Models
{
    [UsedImplicitly(ImplicitUseKindFlags.InstantiatedNoFixedConstructorSignature)]
    public sealed class LevelModel
    {
        private readonly ReactiveCollection<IEnemy> _enemies = new ();
        private readonly List<IObstacle> _obstacles = new ();

        public ICharacter Character { get; private set; }
        public ILevel Level { get; private set; }
        public IReadOnlyReactiveCollection<IEnemy> Enemies => _enemies;
        public IReactiveCommand<int> CompleteLevel { get; } = new ReactiveCommand<int>();
        public IReactiveProperty<ITarget> Target { get; } = new ReactiveProperty<ITarget>();
        public IReadOnlyList<IObstacle> Obstacles => _obstacles;

        public void AddEnemy(IEnemy enemy) => _enemies.Add(enemy);
        public void RemoveEnemy(IEnemy enemy) => _enemies.Remove(enemy);
        public void SetCharacter(ICharacter character) => Character = character;
        public void SetLevel(ILevel level) => Level = level;
        public void AddObstacle(IObstacle obstacle) => _obstacles.Add(obstacle);
        
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
            Character = null;
            Level = null;
            _enemies.Clear();
            _obstacles.Clear();
        }
        
        private bool CharacterHaseFullHealth() => Character.Health.CurrentHealth.Value == Character.Health.MaxHealth;
        private bool CharacterHasHalfHealth() => Character.Health.CurrentHealth.Value >= Character.Health.MaxHealth / 2;
        private bool LevelCompletedHalfTime() => Level.Time >= Level.MaxTime / 2;
    }
}