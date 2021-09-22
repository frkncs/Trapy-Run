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
                enemyController.Run();
            }
        }

        public override void FixedUpdate(EnemyController enemyController)
        {
            
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
