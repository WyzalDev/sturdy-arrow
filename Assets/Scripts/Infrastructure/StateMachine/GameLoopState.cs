using UnityEngine;

namespace SturdyArrow.Infrastructure.StateMachine
{
    public class GameLoopState : FsmState
    {
        public const string GAMELOOP_NAME = "GAMELOOP";

        private float timer = 0;

        public GameLoopState(Fsm fsm) : base(fsm) => Name = GAMELOOP_NAME;

        public override void Enter()
        {
            base.Enter();
            //timer = 2;
        }

        //public override void Update()
        //{
        //    timer = Mathf.Clamp(timer - Time.deltaTime, 0, 2);
        //    if(timer == 0)
        //    {
        //        fsm.SetState(EndGameState.ENDGAME_NAME);
        //    }
        //}
    }
}