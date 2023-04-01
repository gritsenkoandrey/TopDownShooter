using System;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace CodeBase.Infrastructure.Loader
{
    public sealed class SceneLoader : ISceneLoader
    {
        public void Load(string name, Action onLoaded = null)
        {
            LoadScene(name, onLoaded);
        }
        
        private async void LoadScene(string name, Action onLoaded = null)
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