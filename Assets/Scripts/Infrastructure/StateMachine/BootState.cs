using UnityEngine;

namespace SturdyArrow.Infrastructure.StateMachine
{
    public class BootState : FsmState
    {
        public const string BOOTSTRAP_NAME = "BOOTSTRAP";

        private float timer = 0;

        public BootState(Fsm fsm) : base(fsm) => Name = BOOTSTRAP_NAME;

        public override void Enter()
        {
            base.Enter();
            timer = 2;
        }

        public override void Update()
        {
            timer = Mathf.Clamp(timer - Time.deltaTime, 0, 2);
            if(timer == 0) {
                fsm.SetState(MainMenuState.MAINMENU_NAME);
            }
        }
    }
}
