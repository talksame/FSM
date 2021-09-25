using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace FSM.Characters
{
    public class IdleState : State<EnemyController>
    {
        public bool isPatrol = true;
        // 랜덤한 아이들 상태
        private float minIdleTime = 0.0f;
        private float maxIdlelTime = 3.0f;
        private float idlelTime = 0.0f;


        private Animator animator;
        private CharacterController controller;

        protected int hasMove = Animator.StringToHash("Move");
        protected int hasMoveSpeed = Animator.StringToHash("MoveSpeed");
        // 초기화 함수
        public override void OnInitialized()
        {
            animator = context.GetComponent<Animator>();
            controller = context.GetComponent<CharacterController>();


        }

        public override void OnEnter()
        {
            animator?.SetBool(hasMove, false);
            animator?.SetFloat(hasMoveSpeed, 0);
            controller?.Move(Vector3.zero);

            if (isPatrol)
            {
                idlelTime = Random.Range(minIdleTime, maxIdlelTime);
            }
        }

        public override void Update(float deltaTime)
        {
            Transform enemy = context.SearchEnemy();
            if (enemy)
            {
                if (context.IsAvailableAttack)
                {
                    stateMachine.ChangeState<AttackState>();
                }
                else
                {
                    stateMachine.ChangeState<MoveState>();
                }
            }
            else if ( isPatrol && stateMachine.ElapsedTimeInState > idlelTime)
            {
                stateMachine.ChangeState<PatrolScript>();
            }

        }


        public override void onExit()
        {

        }
    }
}
