using Zenject;
using SturdyArrow.SceneManagement;

namespace SturdyArrow.Infrastructure.Installers
{
    public class LoaderCallbackInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.InstantiateComponentOnNewGameObject<LoaderCallback>();
        }
    }
}