using AndreyGritsenko.Infrastructure.Services;
using AndreyGritsenko.Infrastructure.States;

namespace AndreyGritsenko.Infrastructure
{
    public sealed class Game
    {
        public readonly GameStateMachine StateMachine;

        public Game(ICoroutineRunner coroutineRunner, LoadingCurtain curtain)
        {
            StateMachine = new GameStateMachine(new SceneLoader(coroutineRunner), curtain, AllServices.Container);
        }
    }
}