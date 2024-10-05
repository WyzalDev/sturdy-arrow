using SturdyArrow.Infrastructure.StateMachine;
using SturdyArrow.SceneManagement;
using SturdyArrow.Services;
using Unity.VisualScripting;
using UnityEngine;
using Zenject;

namespace SturdyArrow.Infrastructure.Installers
{
    public class BootstrapInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            BindSceneLoader();
            BindSceneService();

            BindFsm();
            InstantiateLifecycleService();
            CheckAllObjectsInContainer();
        }

        private void BindFsm()
        {
            Fsm fsmInstance = new Fsm();

            Container.Bind<Fsm>()
                .FromInstance(fsmInstance)
                .AsSingle()
                .NonLazy();

            fsmInstance.AddState(Container.Instantiate<BootState>());
            fsmInstance.AddState(Container.Instantiate<MainMenuState>());
            fsmInstance.AddState(Container.Instantiate<GameLoopState>());
            fsmInstance.AddState(Container.Instantiate<EndGameState>());
            fsmInstance.SetState(BootState.BOOTSTRAP_NAME);
        }

        private void InstantiateLifecycleService()
        {
            LifecycleMono lifecycleService = Container
                .InstantiateComponentOnNewGameObject<LifecycleMono>();
        }


        private void BindSceneLoader()
        {
            Container.Bind<SceneLoader>()
                .FromNew()
                .AsSingle()
                .NonLazy();
        }

        private void BindSceneService()
        {
            Container.Bind<ISceneService>()
                .To<DefaultSceneService>()
                .FromNew()
                .AsSingle();
        }

        private void CheckAllObjectsInContainer()
        {
            Debug.Log("START CHECKING ALL OBJECT IN CONTAINER");
            Debug.Log("CONTRACTS :");
            foreach(var contract in Container.AllContracts)
            {
                Debug.Log(contract.ToSafeString());
            }
        }
    }
}