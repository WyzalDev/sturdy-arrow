using UnityEngine;
using Zenject;

namespace SturdyArrow.Infrastructure.StateMachine
{
    public class LifecycleMono : MonoBehaviour
    {
        private Fsm Fsm { get; set; }

        [Inject]
        private void Construct(Fsm fsm) => Fsm = fsm;

        void Update() => Fsm.Update();
    }
}