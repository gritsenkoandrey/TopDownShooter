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
        
        public static T GetRandomElementAndRemove<T>(this IList<T> collection)
        {
            int index = UnityEngine.Random.Range(0, collection.Count);
            
            T element = collection[index];

            collection.RemoveAt(index);
            
            return element;
        }

        public static T GetFirst<T>(this IList<T> collection)
        {
            return collection[0];
        }

        public static T GetLast<T>(this IList<T> collection)
        {
            return collection[^1];
        }
    }
}