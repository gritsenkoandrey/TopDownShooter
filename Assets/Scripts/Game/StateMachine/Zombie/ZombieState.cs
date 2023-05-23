namespace CodeBase.Game.StateMachine.Zombie
{
    public abstract class ZombieState
    {
        protected ZombieStateMachine StateMachine { get; }

        protected ZombieState(ZombieStateMachine stateMachine)
        {
            StateMachine = stateMachine;
        }
        
        public virtual void Enter() {}
        public virtual void Exit() {}
        public virtual void Tick() {}
    }
}