using System;
using UnityEngine;

namespace SturdyArrow.Infrastructure.StateMachine
{
    public class EndGameState : FsmState
    {
        public const string ENDGAME_NAME = "ENDGAME";
        private float timer = 0;

        public EndGameState(Fsm fsm) : base(fsm) => Name = ENDGAME_NAME;

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
                int r = (int) Math.Round(UnityEngine.Random.value);

                switch(r) {
                    case 1: {
                            fsm.SetState(MainMenuState.MAINMENU_NAME);
                            break;
                        }
                    default:
                        {
                            fsm.SetState(GameLoopState.GAMELOOP_NAME);
                            break;
                        }
                }
            }
        }
    }
}
