using UnityEngine;

namespace States.Enemy
{
    public class EnemyRunState : EnemyBaseState
    {
        public EnemyRunState(EnemyController enemyController) : base(enemyController)
        {
            enemyController.PlayRunAnim();
            enemyController.enemyMovement.ActivateNavMesh();
        }

        public override void Update(EnemyController enemyController)
        {
            if (enemyController.enemyMovement.IsNavMeshActivated())
            {
                enemyController.enemyMovement.NavMeshMovement();
            }
            else
            {
                enemyController.enemyMovement.MoveForward();
            }
        }

        public override void FixedUpdate(EnemyController enemyController)
        {
            if (enemyController.CheckIfFloating())
            {
                enemyController.currentState = new EnemyFallState(enemyController);
            }
        }

        public override void OnCollisionEnter(EnemyController enemyController, Collision collision)
        {
            enemyController.CatchPlayer(collision);
            enemyController.enemyMovement.FindPath(collision);
        }

        public override void OnCollisionExit(EnemyController enemyController)
        {
            
        }

        public override void OnTriggerEnter(EnemyController enemyController)
        {
            
        }

        public override void OnTriggerExit(EnemyController enemyController)
        {
            
        }
    }
}
