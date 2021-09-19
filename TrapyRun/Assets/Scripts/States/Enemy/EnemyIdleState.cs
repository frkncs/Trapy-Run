using UnityEngine;

namespace States.Enemy
{
    public class EnemyIdleState : EnemyBaseState
    {
        public EnemyIdleState(EnemyController enemyController) : base(enemyController)
        {
            enemyController.PlayIdleAnim();
        }

        public override void Update(EnemyController enemyController)
        {
            if (EnemyController.canRun)
            {
                enemyController.currentState = new EnemyRunState(enemyController);
            }
        }

        public override void FixedUpdate(EnemyController enemyController)
        {
            
        }

        public override void OnCollisionEnter(EnemyController enemyController, Collision collision)
        {
            
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
