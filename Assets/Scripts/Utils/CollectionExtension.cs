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

        public static void Foreach<T>(this IReadOnlyList<T> collection, Action<T> action)
        {
            for (int i = collection.Count - 1; i >= 0; i--) action.Invoke(collection[i]);
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
        
        public static T GetElementOrLast<T>(this IList<T> collection, int index)
        {
            return index >= collection.Count ? collection[^1] : collection[index];
        }
        
        public static T GetElementOrFirst<T>(this IList<T> collection, int index)
        {
            return collection[index % collection.Count];
        }

        public static T GetFirst<T>(this IList<T> collection)
        {
            return collection[0];
        }

        public static T GetFirstAndRemove<T>(this IList<T> collection)
        {
            T element = collection[0];
            collection.RemoveAt(0);
            return element;
        }

        public static T GetLast<T>(this IList<T> collection)
        {
            return collection[^1];
        }

        public static T GetLastAndRemove<T>(this IList<T> collection)
        {
            T element = collection[^1];
            collection.RemoveAt(collection.Count - 1);
            return element;
        }
    }
}