using SturdyArrow.SceneManagement;
using SturdyArrow.Services;
using UnityEngine;

namespace SturdyArrow.Infrastructure.StateMachine
{
    public class MainMenuState : FsmState
    {
        public const string MAINMENU_NAME = "MAINMENU";

        private float timer = 0;

        private IAudioService _audioService;

        private ISceneService _sceneService;

        public MainMenuState(Fsm fsm, IAudioService audioService, ISceneService sceneService) : base(fsm)
        {
            Name = MAINMENU_NAME;
            _audioService = audioService;
            _sceneService = sceneService;
        }

        public override void Enter()
        {
            base.Enter();
            timer = 4;
            _audioService.PlayMusic("SomeMusic1", true);
        }

        public override void Update()
        {
            timer = Mathf.Clamp(timer - Time.deltaTime, 0, 4);
            if(timer == 0)
            {
                fsm.SetState(GameLoopState.GAMELOOP_NAME);
            }
        }

        public override void Exit()
        {
            base.Exit();
            _audioService.PlayMusic("SomeMusic2", true);
            _sceneService.Load(Scene.GameLoop);
        }

    }
}