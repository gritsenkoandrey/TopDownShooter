using System;

namespace AndreyGritsenko.ECSCore
{
    public static class EntityContainer<T> where T : Entity
    {
        public static Action<T> OnRegistered { get; set; }
        public static Action<T> OnUnregistered { get; set; }
    }
}