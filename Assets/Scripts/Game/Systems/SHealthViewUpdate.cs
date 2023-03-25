using CodeBase.ECSCore;
using CodeBase.Game.Components;
using CodeBase.Utils;
using DG.Tweening;
using UniRx;
using UnityEngine;

namespace CodeBase.Game.Systems
{
    public sealed class SHealthViewUpdate : SystemComponent<CHealthView>
    {
        private Camera _camera;
        
        protected override void OnEnableSystem()
        {
            base.OnEnableSystem();
            
            _camera = Camera.main;
        }

        protected override void OnDisableSystem()
        {
            base.OnDisableSystem();
        }

        protected override void OnTick()
        {
            base.OnTick();

            foreach (CHealthView component in Entities)
            {
                LookAtCamera(component);
            }
        }

        protected override void OnEnableComponent(CHealthView component)
        {
            base.OnEnableComponent(component);

            component.Health.Health
                .SkipLatestValueOnSubscribe()
                .Subscribe(health =>
                {
                    if (health > 0)
                    {
                        float x =  Mathematics.Remap(0, component.Health.MaxHealth, 0f, 1f, health);

                        component.Text.text = $"{health}/{component.Health.MaxHealth}";
                        component.Tween?.Kill();
                        component.Tween = component.Fill.DOScale(new Vector3(x, 1f, 1f), 0.1f).SetEase(Ease.Linear);
                    }
                    else
                    {
                        component.Background.SetActive(false);
                    }
                })
                .AddTo(component.LifetimeDisposable);
        }

        protected override void OnDisableComponent(CHealthView component)
        {
            base.OnDisableComponent(component);
        }
        
        private void LookAtCamera(CHealthView component)
        {
            Quaternion rotation = _camera.transform.rotation;

            component.Background.transform.LookAt(component.Background.transform.position + rotation * Vector3.back, rotation * Vector3.up);
        }
    }
}