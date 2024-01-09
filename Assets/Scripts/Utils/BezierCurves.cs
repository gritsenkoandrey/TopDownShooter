using UnityEngine;

namespace CodeBase.Utils
{
    public static class BezierCurves
    {
        public static Vector3 Quadratic(Vector3 from, Vector3 center, Vector3 to, float t)
        {
            return (1f - t) * (1f - t) * from + 2f * (1f - t) * t * center + t * t * to;
        }

        public static Vector3 Cubic(Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3, float t)
        {
            float tt = 1f - t;

            return (tt * tt * tt) * p0 + 3f * (tt * tt * t) * p1 + 3f * (tt * t * t) * p2 + (t * t * t) * p3;
        }
        
        public static Vector3 Noise(Vector3 startPosition, float noiseAmplitude, float noiseFreq, float time, float elapsedTime)
        {
            Vector3 noise;

            float envelope = 1f - (1f - 2f * time) * (1f - 2f * time);

            noise.x = noiseAmplitude * envelope * Mathf.PerlinNoise(startPosition.x, noiseFreq * elapsedTime);
            noise.y = noiseAmplitude * envelope * Mathf.PerlinNoise(startPosition.y, noiseFreq * elapsedTime);
            noise.z = 0f;

            return noise;
        }
    }
}