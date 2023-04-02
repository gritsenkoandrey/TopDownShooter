using System;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace CodeBase.Infrastructure.Loader
{
    public sealed class SceneLoaderService : ISceneLoaderService
    {
        void ISceneLoaderService.Load(string name, Action onLoaded)
        {
            LoadScene(name, onLoaded);
        }
        
        private async void LoadScene(string name, Action onLoaded)
        {
            if (SceneManager.GetActiveScene().name == name)
            {
                onLoaded?.Invoke();
                
                return;
            }
            
            AsyncOperation waitNextScene = SceneManager.LoadSceneAsync(name);

            while (!waitNextScene.isDone)
            {
                await UniTask.Yield();
            }
            
            onLoaded?.Invoke();
        }
    }
}