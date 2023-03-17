using CodeBase.Infrastructure.Services;
using UnityEngine;

namespace CodeBase.UI.Factories
{
    public interface IUIFactory : IService
    {
        public GameObject CreateCanvas();
        public BaseScreen CreateScreen(ScreenType type);
        public void CleanUp();
    }
}