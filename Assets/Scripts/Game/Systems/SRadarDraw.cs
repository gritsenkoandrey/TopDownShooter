using CodeBase.ECSCore;
using CodeBase.Game.Components;
using CodeBase.Utils;
using UniRx;
using UnityEngine;

namespace CodeBase.Game.Systems
{
    public sealed class SRadarDraw : SystemComponent<CRadar>
    {
        protected override void OnLateUpdate()
        {
            base.OnLateUpdate();
            
            Entities.Foreach(SetQuaternionIdentity);
        }

        protected override void OnEnableComponent(CRadar component)
        {
            base.OnEnableComponent(component);

            component.Draw
                .Subscribe(_ => DrawCircle(component))
                .AddTo(component.LifetimeDisposable);

            component.Clear
                .Subscribe(_ => Clear(component))
                .AddTo(component.LifetimeDisposable);
        }

        private void DrawCircle(CRadar component)
        {
            float offset = 0f;
            int size = Mathf.RoundToInt(1f / component.Scale + 1f);
                    
            component.LineRenderer.positionCount = size;
            component.LineRenderer.widthMultiplier = component.Width;
                    
            for (int i = 0; i < size; i++)
            {
                offset += 2f * Mathf.PI * component.Scale;
                        
                float x = component.Radius * Mathf.Sin(offset);
                float z = component.Radius * Mathf.Cos(offset);
                        
                component.LineRenderer.SetPosition(i, new Vector3(x, 0f, z));
            }
        }

        private void DrawStar(CRadar component)
        {
            int numPoints = 7;
            float outerRadius = 2f;
            float innerRadius = 1f;
            
            component.LineRenderer.positionCount = numPoints * 2;
            component.LineRenderer.widthMultiplier = component.Width;

            float angleIncrement = 2f * Mathf.PI / numPoints;

            for (int i = 0; i < numPoints * 2; i++)
            {
                int radius = i % 2 == 0 ? Mathf.RoundToInt(outerRadius) : Mathf.RoundToInt(innerRadius);
                float angle = i * angleIncrement;

                float x = radius * Mathf.Cos(angle);
                float z = radius * Mathf.Sin(angle);

                Vector3 point = new Vector3(x, 0f, z);
                component.LineRenderer.SetPosition(i, point);
            }
        }

        private void Clear(CRadar component) => component.LineRenderer.positionCount = 0;

        private void SetQuaternionIdentity(CRadar radar) => radar.transform.rotation = Quaternion.identity;
    }
}