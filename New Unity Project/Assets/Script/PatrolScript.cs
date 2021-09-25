using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace FSM.Characters
{
    public class PatrolScript : State<EnemyController>
    {
        private Animator animator;
        private CharacterController controller;
        private NavMeshAgent agent;

        protected int hasMove = Animator.StringToHash("Move");
        protected int hasMoveSpeed = Animator.StringToHash("MoveSpeed");
        // 초기화 함수
        public override void OnInitialized()
        {
            animator = context.GetComponent<Animator>();
            controller = context.GetComponent<CharacterController>();
            agent = context.GetComponent<NavMeshAgent>();
        }

        public override void OnEnter()
        {
            //최적화 기법
            if ( context.targetWaypoint == null)
            {
                Transform destination = context.FindNextWayPoint();
            }


            if (context.targetWaypoint)
            {
                //AI에 목표지점을 넣어주고, 이동상태를 true로 변환한다.
                agent?.SetDestination(context.targetWaypoint.position);
                animator?.SetBool(hasMove, true);
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
            else
            {
                //pathPending => AI 의 경로가 존재하는가?
                //이동한 거리가 남은 거리보다 많다면
                if ( !agent.pathPending && ( agent.remainingDistance <= agent.stoppingDistance))
                {
                    Transform nextDest = context.FindNextWayPoint();
                    if ( nextDest)
                    {
                        agent?.SetDestination(nextDest.position);
                    }
                    stateMachine.ChangeState<IdleState>();
                }
                else
                {
                    controller.Move(agent.velocity * deltaTime);
                    animator.SetFloat(hasMoveSpeed, agent.velocity.magnitude / agent.speed, 0.1f, deltaTime);
                }

            }

        }


        public override void onExit()
        {
            animator?.SetBool(hasMove, false);
            agent?.ResetPath();
        }
    }
}

