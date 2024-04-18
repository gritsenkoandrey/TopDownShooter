using CodeBase.ECSCore;
using CodeBase.Game.Components;
using CodeBase.Infrastructure.Models;
using CodeBase.Utils;
using UniRx;
using UnityEngine;
using VContainer;

namespace CodeBase.Game.Systems
{
    public sealed class STargetWeaponUpdate : SystemComponent<CTargetWeapon>
    {
        private LevelModel _levelModel;

        [Inject]
        private void Construct(LevelModel levelModel)
        {
            _levelModel = levelModel;
        }

        protected override void OnUpdate()
        {
            base.OnUpdate();
            
            Entities.Foreach(UpdateTarget);
        }

        protected override void OnEnableComponent(CTargetWeapon component)
        {
            base.OnEnableComponent(component);

            _levelModel.Target
                .Subscribe(component.SetTarget)
                .AddTo(component.LifetimeDisposable);
        }

        private void UpdateTarget(CTargetWeapon component)
        {
            if (component.IsPause)
            {
                return;
            }
            
            if (component.HasTarget)
            {
                component.Transform.position = component.Target.Position;
                component.Transform.localEulerAngles += Vector3.up * Time.deltaTime * 250f;
                component.TargetScale.localScale = Vector3.one * component.Target.Scale;
            }

            float scale = component.Scale;

            if (component.IsValid)
            {
                scale += Time.deltaTime * 5f;
            }
            else
            {
                scale -= Time.deltaTime * 5f;
            }

            scale = Mathf.Clamp(scale, 0f, 1f);
            component.Transform.localScale = Vector3.one * scale;
        }
    }
}