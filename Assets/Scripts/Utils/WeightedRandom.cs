using System;
using System.Collections.Generic;
using System.Linq;

namespace CodeBase.Utils
{
    public sealed class WeightedRandom<T>
    {
        public T GetRandomWeightedItems(List<RandomItemContainer> items)
        {
            float weightSum = items.Sum(container => container.Weight);
            float value = UnityEngine.Random.Range(0f, weightSum);
            float current = 0f;

            for (int i = 0; i < items.Count; i++)
            {
                current += items[i].Weight;

                if (current > value)
                {
                    return items[i].Item;
                }
            }

            throw new ArgumentException("Item not found");
        }
        
        public struct RandomItemContainer
        {
            public readonly T Item;
            public readonly float Weight;

            public RandomItemContainer(T item, float weight)
            {
                Item = item;
                Weight = weight;
            }
        }
    }
}