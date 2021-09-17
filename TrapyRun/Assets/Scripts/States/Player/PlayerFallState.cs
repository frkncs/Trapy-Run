using UnityEngine;

namespace States.Player
{
    public class PlayerFallState : PlayerBaseState
    {
        public PlayerFallState(PlayerController playerController) : base(playerController)
        {
            playerController.PlaFallAnim();
        }

        public override void Update(PlayerController playerController)
        {
            
        }

        public override void FixedUpdate(PlayerController playerController)
        {
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
