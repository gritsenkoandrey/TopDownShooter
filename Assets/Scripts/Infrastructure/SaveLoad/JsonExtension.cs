using UnityEngine;

namespace CodeBase.Infrastructure.SaveLoad
{
    public static class JsonExtension
    {
        public static T ToDeserialize<T>(this string json) => JsonUtility.FromJson<T>(json);
        public static string ToSerialize(this object obj) => JsonUtility.ToJson(obj);
    }
}