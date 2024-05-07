using Game.Services.SceneLoader;
using UnityEngine;
using Zenject;

namespace Game.Meta
{
    public class Restarter : MonoBehaviour
    {
        private ISceneLoader _sceneLoader;
    
        [Inject]
        private void Construct(ISceneLoader sceneLoader)
        {
            _sceneLoader = sceneLoader;
        }
    
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.F5))
            {
                print("333");
                print(_sceneLoader);
                _sceneLoader.Load(Scene.Core);
            }
        }
    }
}