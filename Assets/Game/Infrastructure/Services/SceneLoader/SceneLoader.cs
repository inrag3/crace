using System;
using System.Collections;
using Game.Infrastructure.Services.CoroutinePerformer;
using UnityEngine.SceneManagement;

namespace Game.Infrastructure.Services.SceneLoader
{
    public class SceneLoader : ISceneLoader
    {
        private readonly ICoroutinePerformer _performer;

        public SceneLoader(ICoroutinePerformer performer)
        {
            _performer = performer;
        }

        public void Load(Scene scene, Action onLoaded = null) =>
            _performer.StartPerform(Start(scene, onLoaded));

        private IEnumerator Start(Scene scene, Action onLoaded = null)
        {
            var nextScene = SceneManager.LoadSceneAsync(scene.ToString());
            while (!nextScene.isDone)
                yield return null;
            onLoaded?.Invoke();
        }
    }
}