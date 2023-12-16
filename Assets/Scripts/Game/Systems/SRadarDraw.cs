using CodeBase.ECSCore;
using CodeBase.Game.Components;
using UniRx;
using UnityEngine;

namespace CodeBase.Game.Systems
{
    public sealed class SRadarDraw : SystemComponent<CRadar>
    {
        protected override void OnLateUpdate()
        {
            base.OnLateUpdate();

            foreach (CRadar radar in Entities)
            {
                radar.transform.rotation = Quaternion.identity;
            }
        }

        protected override void OnEnableComponent(CRadar component)
        {
            base.OnEnableComponent(component);

            component.Draw
                .Subscribe(_ =>
                {
                    float offset = 0f;
                    int size = (int)(1f / component.Scale + 1f);
                    
                    component.LineRenderer.positionCount = size;
                    component.LineRenderer.widthMultiplier = component.Width;
                    
                    for (int i = 0; i < size; i++)
                    {
                        offset += 2f * Mathf.PI * component.Scale;
                        
                        float x = component.Radius * Mathf.Sin(offset);
                        float z = component.Radius * Mathf.Cos(offset);
                        
                        component.LineRenderer.SetPosition(i, new Vector3(x, 0f, z));
                    }
                })
                .AddTo(component.LifetimeDisposable);

            component.Clear
                .Subscribe(_ => component.LineRenderer.positionCount = 0)
                .AddTo(component.LifetimeDisposable);
        }
    }
}