using SturdyArrow.Services;
using SturdyArrow.SceneManagement;

namespace SturdyArrow.Infrastructure.StateMachine
{
    public class BootState : FsmState
    {
        public const string BOOTSTRAP_NAME = "BOOTSTRAP";

        private bool isFirstUpdate = true;

        private ISceneService _sceneService;

        public BootState(Fsm fsm, ISceneService sceneService) : base(fsm)
        {
            Name = BOOTSTRAP_NAME;
            _sceneService = sceneService;
        }

        public override void Update()
        {
            if(isFirstUpdate)
            {
                fsm.SetState(GameLoopState.GAMELOOP_NAME);
                isFirstUpdate = false;
            }
        }

        public override void Exit()
        {
            base.Exit();
            _sceneService.Load(Scene.GameLoop);
        }
    }
}
