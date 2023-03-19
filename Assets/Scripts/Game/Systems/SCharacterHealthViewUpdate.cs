using CodeBase.ECSCore;
using CodeBase.Game.Components;
using CodeBase.Utils;
using UniRx;
using UnityEngine;

namespace CodeBase.Game.Systems
{
    public sealed class SCharacterHealthViewUpdate : SystemComponent<CCharacter>
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

            foreach (CCharacter character in Entities)
            {
                LookAtCamera(character.HealthView);
            }
        }

        protected override void OnEnableComponent(CCharacter component)
        {
            base.OnEnableComponent(component);

            component.Health.Health
                .SkipLatestValueOnSubscribe()
                .Subscribe(health =>
                {
                    if (health > 0)
                    {
                        float x =  Mathematics.Remap(0, component.Health.MaxHealth, 0f, 1f, health);

                        component.HealthView.Text.text = $"{health}/{component.Health.MaxHealth}";
                        component.HealthView.Fill.localScale = new Vector3(x, 1f, 1f);
                    }
                    else
                    {
                        component.HealthView.Background.SetActive(false);
                    }
                })
                .AddTo(component.LifetimeDisposable);
        }

        protected override void OnDisableComponent(CCharacter component)
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