using CodeBase.Game.Components;
using CodeBase.Game.Models;

namespace CodeBase.Game.Interfaces
{
    public interface ICharacter : IEntity
    {
        public CAnimator Animator { get; }
        public Health Health { get; }
        public CWeaponMediator WeaponMediator { get; }
        public CMove Move { get; }
        public CStateMachine StateMachine { get; }
    }
}