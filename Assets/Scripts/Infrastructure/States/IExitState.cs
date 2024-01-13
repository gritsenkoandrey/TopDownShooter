using VContainer.Unity;

namespace CodeBase.Infrastructure.States
{
    public interface IExitState : IInitializable
    {
        public void Exit();
    }
}