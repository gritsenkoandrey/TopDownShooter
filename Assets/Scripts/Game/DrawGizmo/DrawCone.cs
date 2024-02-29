using System.Collections.Generic;
using UnityEngine;

namespace CodeBase.Game.DrawGizmo
{
    public sealed class DrawCone : MonoBehaviour
    {
        [Range(10, 180)] public int Angle = 15;
        [Range(1, 50)] public int Step = 5;
        [Range(0.5f, 10f)] public float Lenght = 5f;
        
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
        
            Vector3 rot = transform.eulerAngles;
            Vector3 pos = transform.position;

            List<Vector3> points = new List<Vector3>();

            for (int angle = -Angle; angle <= Angle; angle += Step)
            {
                float rad = Mathf.Repeat(rot.y + angle, 360f) * Mathf.Deg2Rad;
                float x = pos.x + Lenght * Mathf.Sin(rad);
                float z = pos.z + Lenght * Mathf.Cos(rad);
                Vector3 point = new Vector3(x, 0f, z);
                
                points.Add(point);
            }

            for (int i = 0; i < points.Count; i++)
            {
                Gizmos.DrawLine(pos, points[i]);
                
                if (i == 0)
                {
                    continue;
                }
                
                Gizmos.DrawLine(points[i], points[i - 1]);
            }
        }
    }
}