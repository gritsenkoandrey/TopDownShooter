using UnityEngine;

namespace CodeBase.Utils
{
    public static class VectorExtension
    {
        public static Vector3 AddY(this Vector3 vector, float add) => new Vector3(vector.x, vector.y + add, vector.z);
        public static Vector3 AddX(this Vector3 vector, float add) => new Vector3(vector.x + add, vector.y, vector.z);
        public static Vector3 AddZ(this Vector3 vector, float add) => new Vector3(vector.x, vector.y, vector.z + add);
        public static Vector3 ZeroX(this Vector3 vector) => new Vector3(0f, vector.y, vector.z);
        public static Vector3 ZeroY(this Vector3 vector) => new Vector3(vector.x, 0f, vector.z);
        public static Vector3 ZeroZ(this Vector3 vector) => new Vector3(vector.x, vector.y, 0f);
    }
}