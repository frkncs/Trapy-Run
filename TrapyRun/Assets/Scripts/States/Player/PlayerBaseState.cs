using UnityEngine;

namespace States.Player
{
    public abstract class PlayerBaseState
    {
        protected PlayerBaseState(PlayerController playerController)
        {
            
        }

        public abstract void Update(PlayerController playerController);
        
        public abstract void FixedUpdate(PlayerController playerController);
        
        public abstract void OnCollisionEnter(PlayerController playerController);
        
        public abstract void OnCollisionExit(PlayerController playerController);
        
        public abstract void OnTriggerEnter(PlayerController playerController);
        
        public abstract void OnTriggerExit(PlayerController playerController);
    }
}
