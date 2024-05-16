using System.Collections.Generic;
using CodeBase.ECSCore;
using CodeBase.Game.ComponentsUi;
using CodeBase.Game.Interfaces;
using CodeBase.Infrastructure.CameraMain;
using CodeBase.Infrastructure.Factories.UI;
using CodeBase.Infrastructure.Models;
using CodeBase.Utils;
using Cysharp.Threading.Tasks;
using UniRx;
using UnityEngine;
using VContainer;

namespace CodeBase.Game.SystemsUi
{
    public sealed class SRegenerationHealthProvider : SystemComponent<CRegenerationHealthProvider>
    {
        private IUIFactory _uiFactory;
        private ICameraService _cameraService;
        private LevelModel _levelModel;

        [Inject]
        private void Construct(IUIFactory uiFactory, ICameraService cameraService, LevelModel levelModel)
        {
            _uiFactory = uiFactory;
            _cameraService = cameraService;
            _levelModel = levelModel;
        }
        
        protected override void OnEnableComponent(CRegenerationHealthProvider component)
        {
            base.OnEnableComponent(component);

            _levelModel.Character
                .Health.CurrentHealth
                .Pairwise()
                .Where(pair => pair.Current > pair.Previous)
                .Subscribe(pair =>
                {
                    int health = pair.Current - pair.Previous;

                    Create(component, _levelModel.Character, health).Forget();
                })
                .AddTo(component.LifetimeDisposable);

            foreach (IEnemy enemy in _levelModel.Enemies)
            {
                enemy.Health.CurrentHealth
                    .Pairwise()
                    .Where(pair => pair.Current > pair.Previous)
                    .Subscribe(pair =>
                    {
                        int health = pair.Current - pair.Previous;

                        Create(component, enemy, health).Forget();
                    })
                    .AddTo(component.LifetimeDisposable);
            }
        }

        private async UniTaskVoid Create(CRegenerationHealthProvider component, ITarget target, int health)
        {
            if (component.Elements.IsReadOnly)
            {
                component.Elements = new List<CRegenerationHealth>();
            }
            else
            {
                for (int i = 0; i < component.Elements.Count; i++)
                {
                    if (component.Elements[i].IsActive)
                    {
                        continue;
                    }
                        
                    Activate(component.Elements[i], target, health);
                        
                    return;
                }
            }
                
            CRegenerationHealth element = await _uiFactory.CreateRegenerationHealth(component.transform);
            component.Elements.Add(element);
            Activate(element, target, health);
        }

        private void Activate(CRegenerationHealth element, ITarget target, int health)
        {
            element.SetTarget(target);
            element.Text.text = string.Format(FormatText.AddHealth, health.Trim());
            element.CanvasGroup.alpha = 1f;
            Vector3 worldPos = target.Position.AddY(target.Height / 2f);
            Vector3 screenPos = _cameraService.Camera.WorldToScreenPoint(worldPos);
            element.transform.position = screenPos;
            element.SetActive(true);
        }
    }
}