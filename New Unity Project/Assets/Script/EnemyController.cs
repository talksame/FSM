using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace FSM.Characters
{ 

     public class EnemyController : MonoBehaviour
    {
        #region Variables
        protected StateMachine<EnemyController> stateMachine;
        //�þ߰Ÿ� > ���ݰŸ� �� �⺻���� ����

        public StateMachine<EnemyController> StateMachine => stateMachine;


        private FieldOfView fov;
        //public LayerMask targetMask;
        //public float viewRadius;
        public float attackRange;


        public Transform Target => fov.NeartestTarget;


        // Patrol
        public Transform[] waypoints;
        [HideInInspector]
        public Transform targetWaypoint = null;
        private int waypointIndex = 0;

        #endregion Variables

        #region Unity Methods
        private void Start()
        {
            stateMachine = new StateMachine<EnemyController>(this, new PatrolScript());
            IdleState idlestate = new IdleState();
            idlestate.isPatrol = true;
            stateMachine.AddState(new IdleState());
            stateMachine.AddState(new MoveState());
            stateMachine.AddState(new AttackState());


            fov = GetComponent<FieldOfView>();
        }

        private void Update()
        {
            stateMachine.Update(Time.deltaTime);
        }
        #endregion Unity Methods
        public bool IsAvailableAttack
        {
            get
            {
                if (!Target)
                {
                    return false;
                }
                float distance = Vector3.Distance(transform.position, Target.position);
                return (distance <= attackRange);
            }
        }

        #region Other Methods
        public Transform SearchEnemy()
        {
            return Target;
        }


        public Transform FindNextWayPoint()
        {
            targetWaypoint = null;
            //0���� Ŭ ���, ��ΰ� ���� ��쿡�� Ÿ���� �����ϱ�.
            if ( waypoints.Length > 0)
            {
                targetWaypoint = waypoints[waypointIndex];
            }

            waypointIndex = (waypointIndex + 1) % waypoints.Length;

            return targetWaypoint;


        }
        #endregion Other Methods



    } 


}






/*\
         #region Variables
        protected StateMachine<EnemyController_New> stateMachine;


        //�þ߰Ÿ� > ���ݰŸ� �� �⺻���� ����
        public LayerMask targetMask;
        public float viewRadius;
        public float attackRange;
        public Transform target;





        #endregion Variables

        #region Unity Methods
        private void Start()
        {
            stateMachine = new StateMachine<EnemyController_New>(this, new IdleState());
            stateMachine.addState(new MoveState());
            stateMachine.addState(new AttackState());
        }

        private void Update()
        {
            stateMachine.Update(Time.deltaTime);
        }
        #endregion Unity Methods
        public bool IsAvailableATtack
        {
            get
            {
                if ( ! target)
                {
                    return false;
                }
                float distance = Vector3.Distance(transform.position, target.position);
                return (distance <= attackRange);
            }
        }

        #region Other Methods
        public Transform SearchEnemy()
        {
            target = null;
            // �������� ��ü���¿��� ���� �ִ°�?
            Collider[] targetInViewRadius = Physics.OverlapSphere(transform.position, viewRadius, targetMask);


            if ( targetInViewRadius.Length > 0)
            {
                target = targetInViewRadius[0].transform;
            }

            return target;
        }
        #endregion Other Methods
    }
 */