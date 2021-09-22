using UnityEngine;

namespace States.Enemy
{
    public abstract class EnemyBaseState
    {
        protected EnemyBaseState(EnemyController enemyController)
        {
            
        }

        public abstract void Update(EnemyController enemyController);
        
        public abstract void FixedUpdate(EnemyController enemyController);
        
        public abstract void OnCollisionEnter(EnemyController enemyController, Collision collision);
        
        public abstract void OnCollisionExit(EnemyController enemyController, Collision collision);
        
        public abstract void OnTriggerEnter(EnemyController enemyController, Collider other);
        
        public abstract void OnTriggerExit(EnemyController enemyController, Collider other);
    }
}
