using UnityEngine;

namespace SturdyArrow.Infrastructure.StateMachine
{
    public class MainMenuState : FsmState
    {
        public const string MAINMENU_NAME = "MAINMENU";

        private float timer = 0;

        public MainMenuState(Fsm fsm) : base(fsm) => Name = MAINMENU_NAME;

        public override void Enter()
        {
            base.Enter();
            timer = 2;
        }

        public override void Update()
        {
            timer = Mathf.Clamp(timer - Time.deltaTime, 0, 2);
            if(timer == 0)
            {
                fsm.SetState(GameLoopState.GAMELOOP_NAME);
            }
        }

    }
}