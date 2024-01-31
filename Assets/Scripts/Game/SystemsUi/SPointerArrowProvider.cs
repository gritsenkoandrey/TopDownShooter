using CodeBase.ECSCore;
using CodeBase.Game.ComponentsUi;
using CodeBase.Game.Interfaces;
using CodeBase.Infrastructure.Factories.UI;
using CodeBase.Infrastructure.Models;
using Cysharp.Threading.Tasks;
using VContainer;

namespace CodeBase.Game.SystemsUi
{
    public sealed class SPointerArrowProvider : SystemComponent<CPointerArrowProvider>
    {
        private IUIFactory _uiFactory;
        private LevelModel _levelModel;

        [Inject]
        public void Construct(IUIFactory uiFactory, LevelModel levelModel)
        {
            _uiFactory = uiFactory;
            _levelModel = levelModel;
        }

        protected override void OnEnableComponent(CPointerArrowProvider component)
        {
            base.OnEnableComponent(component);
            
            CreatePointers(component).Forget();
        }

        private async UniTaskVoid CreatePointers(CPointerArrowProvider component)
        {
            foreach (IEnemy enemy in _levelModel.Enemies)
            {
                CPointerArrow pointerArrow = await _uiFactory.CreatePointerArrow(component.transform);

                pointerArrow.SetTarget(enemy);
                pointerArrow.SetRectProvider(component.Rect);
                pointerArrow.SetOffset(component.Offset);
                pointerArrow.CanvasGroup.alpha = 0f;
            }
        }
    }
}