using UnityEngine;

namespace CodeBase.Utils
{
    public static class Mathematics
    {
        public static float Remap(float iMin, float iMax, float oMin, float oMax, float value)
        {
            return Mathf.Lerp(oMin, oMax, Mathf.InverseLerp(iMin, iMax, value));
        }
        
        public static Vector3 GenerateRandomPoint(float radius)
        {
            float angle = Random.Range(0f, 1f) * (2f * Mathf.PI) - Mathf.PI;
                    
            float x = Mathf.Sin(angle) * radius;
            float z = Mathf.Cos(angle) * radius;

            return new Vector3(x, 0f, z);
        }
    }
}