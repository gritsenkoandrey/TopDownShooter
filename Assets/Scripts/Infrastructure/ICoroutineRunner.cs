using System.Collections;
using UnityEngine;

namespace AndreyGritsenko.Infrastructure
{
    public interface ICoroutineRunner
    {
        public Coroutine StartCoroutine(IEnumerator coroutine);
    }
}