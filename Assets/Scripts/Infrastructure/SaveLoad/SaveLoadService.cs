using CodeBase.Infrastructure.Factories.Game;
using CodeBase.Infrastructure.Factories.UI;
using CodeBase.Infrastructure.Progress;
using UnityEngine;

namespace CodeBase.Infrastructure.SaveLoad
{
    public sealed class SaveLoadService : ISaveLoadService
    {
        private readonly IProgressService _progressService;
        private readonly IUIFactory _uiFactory;
        private readonly IGameFactory _gameFactory;

        private const string Key = nameof(PlayerProgress);

        public SaveLoadService(IProgressService progressService, IUIFactory uiFactory, IGameFactory gameFactory)
        {
            _progressService = progressService;
            _uiFactory = uiFactory;
            _gameFactory = gameFactory;
        }
        
        public void SaveProgress()
        {
            foreach (IProgressWriter writer in _uiFactory.ProgressWriters)
            {
                writer.Write(_progressService.PlayerProgress);
            }
            
            foreach (IProgressWriter writer in _gameFactory.ProgressWriters)
            {
                writer.Write(_progressService.PlayerProgress);
            }
            
            PlayerPrefs.SetString(Key, _progressService.PlayerProgress.ToSerialize());
        }

        public PlayerProgress LoadProgress()
        {
            return PlayerPrefs.GetString(Key)?.ToDeserialize<PlayerProgress>();
        }
    }
}