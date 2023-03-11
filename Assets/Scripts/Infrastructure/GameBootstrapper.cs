using CodeBase.Infrastructure.States;
using UnityEngine;

namespace CodeBase.Infrastructure
{
    public sealed class GameBootstrapper : MonoBehaviour, ICoroutineRunner
    {
        [SerializeField] private LoadingCurtain _curtain;
        
        private Game _game;
        
        private void Awake()
        {
            _game = new Game(this, Instantiate(_curtain, transform));
            
            _game.StateMachine.Enter<BootstrapState>();
            
            AppSettings();

            DontDestroyOnLoad(this);
        }

        private void AppSettings()
        {
            Application.targetFrameRate = 60;
        }
    }
}