using SturdyArrow.SceneManagement;
using SturdyArrow.Services;
using System;
using UnityEngine;

namespace SturdyArrow.Infrastructure.StateMachine
{
    public class EndGameState : FsmState
    {
        public const string ENDGAME_NAME = "ENDGAME";

        private float timer = 0;

        private int random;

        private ISceneService _sceneService;
        public EndGameState(Fsm fsm, ISceneService sceneService) : base(fsm)
        {
            Name = ENDGAME_NAME;
            _sceneService = sceneService;
        }

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
                random = (int)Math.Round(UnityEngine.Random.value);

                switch(random)
                {
                    case 1:
                        {
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

        public override void Exit()
        {
            base.Exit();
            if(random == 1)
                _sceneService.Load(Scene.MainMenu);
            else
                _sceneService.Load(Scene.GameLoop);
        }
    }
}
