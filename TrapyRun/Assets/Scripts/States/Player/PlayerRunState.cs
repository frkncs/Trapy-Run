using UnityEngine;

namespace States.Player
{
    public class PlayerRunState : PlayerBaseState
    {
        public PlayerRunState(PlayerController playerController) : base(playerController)
        {
            playerController.PlaRunAnim();
        }

        public override void Update(PlayerController playerController)
        {
            playerController.MoveHorizontal();
        }

        public override void FixedUpdate(PlayerController playerController)
        {
            if (playerController.CheckIsFloating())
            {
                playerController.currentState = new PlayerFallState(playerController);
            }
        }

        public override void OnCollisionEnter(PlayerController playerController)
        {
            
        }

        public override void OnCollisionExit(PlayerController playerController)
        {
            
        }

        public override void OnTriggerEnter(PlayerController playerController)
        {
            
        }

        public override void OnTriggerExit(PlayerController playerController)
        {
            
        }
    }
}
