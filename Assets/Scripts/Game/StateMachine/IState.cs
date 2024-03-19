namespace CodeBase.Game.StateMachine
{
    public interface IState
    {
        void Enter();
        void Exit();
        void Tick();
    }
}