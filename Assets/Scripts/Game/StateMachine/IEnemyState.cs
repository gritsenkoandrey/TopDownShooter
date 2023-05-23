namespace CodeBase.Game.StateMachine
{
    public interface IEnemyState
    {
        public void Enter();
        public void Exit();
        public void Tick();
    }
}