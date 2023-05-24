namespace CodeBase.Game.StateMachine.Zombie
{
    public interface IEnemyState
    {
        public void Enter();
        public void Exit();
        public void Tick();
    }
}