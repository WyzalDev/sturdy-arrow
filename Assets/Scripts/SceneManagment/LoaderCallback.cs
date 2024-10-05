using UnityEngine;
using Zenject;

namespace SturdyArrow.SceneManagement
{
    public class LoaderCallback : MonoBehaviour
    {
        private SceneLoader _sceneLoader;

        [Inject]
        private void Construct(SceneLoader sceneLoader) => _sceneLoader = sceneLoader;

        private bool isFirstUpdate = true;

        private void Update()
        {
            if(isFirstUpdate)
            {
                isFirstUpdate = false;
                _sceneLoader.LoaderCallback();
            }
        }
    }
}