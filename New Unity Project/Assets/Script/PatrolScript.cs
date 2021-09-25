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
        // �ʱ�ȭ �Լ�
        public override void OnInitialized()
        {
            animator = context.GetComponent<Animator>();
            controller = context.GetComponent<CharacterController>();
            agent = context.GetComponent<NavMeshAgent>();
        }

        public override void OnEnter()
        {
            //����ȭ ���
            if ( context.targetWaypoint == null)
            {
                Transform destination = context.FindNextWayPoint();
            }


            if (context.targetWaypoint)
            {
                //AI�� ��ǥ������ �־��ְ�, �̵����¸� true�� ��ȯ�Ѵ�.
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
                //pathPending => AI �� ��ΰ� �����ϴ°�?
                //�̵��� �Ÿ��� ���� �Ÿ����� ���ٸ�
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

