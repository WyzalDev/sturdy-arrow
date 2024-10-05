using SturdyArrow.SceneManagement;

namespace SturdyArrow.Services
{
    public class DefaultSceneService : ISceneService
    {
        private SceneLoader _sceneLoader;

        public DefaultSceneService(SceneLoader sceneLoader) => _sceneLoader = sceneLoader;

        public void Load(Scene scene) => _sceneLoader.Load(scene);
    }
}