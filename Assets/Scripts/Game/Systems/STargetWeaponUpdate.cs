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

        private const float ScaleOffset = 2.5f;
        private const float RotateOffset = 250f;

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
            component.transform.rotation = Quaternion.identity;
            
            if (component.IsPause)
            {
                return;
            }
            
            if (component.HasTarget)
            {
                component.Transform.position = component.Target.Position;
                component.TargetScale.localScale = Vector3.one * component.Target.Scale;
            }

            float scale = component.Scale;

            if (component.IsValid)
            {
                component.Transform.localEulerAngles += Vector3.up * Time.deltaTime * RotateOffset;

                scale += Time.deltaTime * ScaleOffset;
            }
            else
            {
                scale -= Time.deltaTime * ScaleOffset;
            }

            component.Transform.localScale = Vector3.one * Mathf.Clamp(scale, 0f, 1f);
        }
    }
}