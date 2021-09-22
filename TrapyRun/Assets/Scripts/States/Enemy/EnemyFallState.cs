using UnityEngine;

namespace States.Enemy
{
    public class EnemyFallState : EnemyBaseState
    {
        public EnemyFallState(EnemyController enemyController) : base(enemyController)
        {
            enemyController.PlayFallAnim();
        }

        public override void Update(EnemyController enemyController)
        {
            enemyController.enemyMovement.Fall();
        }

        public override void FixedUpdate(EnemyController enemyController)
        {
            if (!enemyController.CheckIfFloating())
            {
                enemyController.currentState = new EnemyRunState(enemyController);
            }

            if (enemyController.CheckIfFell())
            {
                enemyController.enemyMovement.Respawn();
            }
        }

        public override void OnCollisionEnter(EnemyController enemyController, Collision collision)
        {
        
        }

        public override void OnCollisionExit(EnemyController enemyController, Collision collision)
        {
        
        }

        public override void OnTriggerEnter(EnemyController enemyController, Collider other)
        {
        
        }

        public override void OnTriggerExit(EnemyController enemyController, Collider other)
        {
        
        }
    }
}
