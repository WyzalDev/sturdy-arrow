using SturdyArrow.Services;
using UnityEngine;

namespace SturdyArrow.Infrastructure.StateMachine
{
    public class MainMenuState : FsmState
    {
        public const string MAINMENU_NAME = "MAINMENU";

        private float timer = 0;

        private IAudioService _audioService;

        public MainMenuState(Fsm fsm, IAudioService audioService) : base(fsm)
        {
            Name = MAINMENU_NAME;
            _audioService = audioService;
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
        }

    }
}