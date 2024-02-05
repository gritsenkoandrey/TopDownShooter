using System;
using System.Collections.Generic;

namespace CodeBase.Utils
{
    public static class CollectionExtension
    {
        public static void Foreach<T>(this IEnumerable<T> collection, Action<T> action)
        {
            foreach (T element in collection) action.Invoke(element);
        }

        public static bool HasIndex<T>(this IList<T> collection, int index)
        {
            return index >= 0 && collection.Count > index;
        }

        public static int GetRandomIndex<T>(this IList<T> collection)
        {
            return UnityEngine.Random.Range(0, collection.Count);
        }
        
        public static T GetRandomElement<T>(this IList<T> collection)
        {
            return collection[UnityEngine.Random.Range(0, collection.Count)];
        }
    }
}