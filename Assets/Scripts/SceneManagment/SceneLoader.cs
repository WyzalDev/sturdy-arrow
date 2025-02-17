using System;
using UnityEngine.SceneManagement;

namespace SturdyArrow.SceneManagement
{
    public class SceneLoader
    {

        private Action onLoaderCallback;

        public void Load(Scene scene)
        {
            //Set the loader callback action to load the target scene
            onLoaderCallback = () =>
            {
                SceneManager.LoadScene(scene.ToString());
            };

            //Load the loading scene
            SceneManager.LoadScene(Scene.Loading.ToString());
        }

        public void LoaderCallback()
        {
            if(onLoaderCallback != null)
            {
                onLoaderCallback();
                onLoaderCallback = null;
            }
        }
    }

    //name each scene same as scene existing in project
    public enum Scene
    {
        Bootstrap,
        Loading,
        MainMenu,
        GameLoop
    }
}