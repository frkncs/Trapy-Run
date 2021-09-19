using UnityEngine;

namespace States.Player
{
    public class PlayerFallState : PlayerBaseState
    {
        public PlayerFallState(PlayerController playerController) : base(playerController)
        {
            playerController.PlayFallAnim();
        }

        public override void Update(PlayerController playerController)
        {
            playerController.playerMovement.MoveAndRotate();
        }

        public override void FixedUpdate(PlayerController playerController)
        {
            if (!playerController.CheckIsFloating())
            {
                playerController.currentState = new PlayerRunState(playerController);
            }

            if (playerController.CheckIfFell())
            {
                playerController.Die();
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
