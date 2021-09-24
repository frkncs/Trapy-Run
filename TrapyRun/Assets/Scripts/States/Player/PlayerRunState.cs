using UnityEngine;

namespace States.Player
{
    public class PlayerRunState : PlayerBaseState
    {
        public PlayerRunState(PlayerController playerController) : base(playerController)
        {
            playerController.PlayRunAnim();
        }

        public override void Update(PlayerController playerController)
        {
            playerController.playerMovement.MoveAndRotate();
        }

        public override void FixedUpdate(PlayerController playerController)
        {
            if (playerController.CheckIsFloating())
            {
                playerController.currentState = new PlayerFallState(playerController);
            }

            if (Input.GetMouseButtonUp(0))
            {
                playerController.playerMovement.ResetLookRotation();
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
