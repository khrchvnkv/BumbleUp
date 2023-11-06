using System;
using System.Collections;
using UnityEngine;

namespace Common.Infrastructure.Services.Coroutines
{
    public sealed class CoroutineRunner : MonoBehaviour, ICoroutineRunner
    {
        public void StopCoroutineSafe(Coroutine coroutine)
        {
            if (coroutine != null) StopCoroutine(coroutine);
        }
        public void ExecuteInNextFrame(Action action)
        {
            StartCoroutine(ExecuteInTheNextFrameCoroutine());
            
            IEnumerator ExecuteInTheNextFrameCoroutine()
            {
                yield return new WaitForEndOfFrame();
                action?.Invoke();
            }
        }
    }
}