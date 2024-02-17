using System;
using Cysharp.Threading.Tasks;
using JetBrains.Annotations;
using UnityEngine.AddressableAssets;
using UnityEngine.SceneManagement;

namespace CodeBase.Infrastructure.Loader
{
    [UsedImplicitly(ImplicitUseKindFlags.InstantiatedNoFixedConstructorSignature)]
    public sealed class SceneLoaderService : ISceneLoaderService
    {
        void ISceneLoaderService.Load(string name, Action onLoaded)
        {
            LoadScene(name, onLoaded).Forget();
        }
        
        private async UniTaskVoid LoadScene(string name, Action onLoaded)
        {
            if (SceneManager.GetActiveScene().name.Equals(name))
            {
                onLoaded?.Invoke();
                
                return;
            }
            
            await Addressables.LoadSceneAsync(name).ToUniTask();
            
            onLoaded?.Invoke();
        }
    }
}