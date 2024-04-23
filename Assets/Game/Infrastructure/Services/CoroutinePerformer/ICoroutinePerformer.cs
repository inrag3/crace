using System.Collections;
using UnityEngine;

namespace Game.Infrastructure.Services.CoroutinePerformer
{
    public interface ICoroutinePerformer
    {
        public Coroutine StartPerform(IEnumerator coroutine);

        public void StopPerform(Coroutine coroutine);
    }
}