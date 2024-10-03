using SturdyArrow.Infrastructure.StateMachine;
using UnityEngine;
using Zenject;

namespace SturdyArrow.Services
{
    public class LifecycleService : MonoBehaviour
    {
        private Fsm Fsm { get; set; }

        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
        }

        [Inject]
        private void Construct(Fsm fsm) => Fsm = fsm;

        //TODO delete
        public void SetState(string stateName)
        {
            if(Fsm.ContainsState(stateName))
            {
                Fsm.SetState(stateName);
            }
        }

        void Update() => Fsm.Update();
    }
}