using UnityEngine;

namespace SturdyArrow.Infrastructure.StateMachine
{
    public abstract class FsmState
    {
        public string Name { get; protected set; }

        protected readonly Fsm fsm;

        public FsmState(Fsm fsm)
        {
            this.fsm = fsm;
        }

        public virtual void Enter() {
            Debug.Log($"Enter [{Name}] state");
        }

        public virtual void Exit() {
            Debug.Log($"Exit [{Name}] state, now updating");
        }

        public virtual void Update() { }

    }
}

