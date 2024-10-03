using SturdyArrow.Infrastructure.StateMachine;
using SturdyArrow.Services;
using Zenject;

public class BootstrapInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        BindFsm();
        BindLifecycleService();
    }

    private void BindLifecycleService()
    {
        LifecycleService lifecycleService = Container
            .InstantiateComponentOnNewGameObject<LifecycleService>();
        Container.Bind<LifecycleService>().FromInstance(lifecycleService).AsSingle();
    }

    private void BindFsm() {
        Fsm fsmInstance = new Fsm();
        
        fsmInstance.AddState(new BootState(fsmInstance));
        fsmInstance.AddState(new MainMenuState(fsmInstance));
        fsmInstance.AddState(new GameLoopState(fsmInstance));
        fsmInstance.AddState(new EndGameState(fsmInstance));

        fsmInstance.SetState(BootState.BOOTSTRAP_NAME);

        Container.Bind<Fsm>()
            .FromInstance(fsmInstance)
            .AsSingle()
            .NonLazy();
    }
}
