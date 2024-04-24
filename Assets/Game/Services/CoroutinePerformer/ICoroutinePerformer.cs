using System.Collections;
using UnityEngine;

namespace Game.Services.CoroutinePerformer
{
    public interface ICoroutinePerformer
    {
        public Coroutine StartPerform(IEnumerator coroutine);

        public void StopPerform(Coroutine coroutine);
    }
}