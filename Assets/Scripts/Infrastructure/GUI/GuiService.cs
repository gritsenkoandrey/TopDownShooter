using System.Collections.Generic;
using CodeBase.UI;
using CodeBase.UI.Screens;
using UnityEngine;

namespace CodeBase.Infrastructure.GUI
{
    public sealed class GuiService : MonoBehaviour, IGuiService
    {
        [SerializeField] private StaticCanvas _staticCanvas;

        private readonly Stack<BaseScreen> _screens = new ();
        
        StaticCanvas IGuiService.StaticCanvas => _staticCanvas;
        
        float IGuiService.ScaleFactor => _staticCanvas.Canvas.scaleFactor;

        void IGuiService.Push(BaseScreen screen)
        {
            if (_screens.TryPeek(out BaseScreen value))
            {
                value.SetActive(false);
            }
            
            _screens.Push(screen);
        }

        void IGuiService.Pop()
        {
            if (_screens.TryPop(out BaseScreen value))
            {
                Destroy(value.gameObject);
            }
            
            if (_screens.TryPeek(out BaseScreen screen))
            {
                screen.SetActive(true);
            }
        }

        void IGuiService.CleanUp()
        {
            foreach (BaseScreen screen in _screens)
            {
                Destroy(screen.gameObject);
            }
            
            _screens.Clear();
        }
    }
}