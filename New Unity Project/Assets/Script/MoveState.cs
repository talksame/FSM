using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


namespace FastCampus.Characters
{
    public class MoveState : State<EnemyController>
    {
        private Animator animator;
        private CharacterController controller;
        private NavMeshAgent agent;


        private int hashMove = Animator.StringToHash("Move");
        private int hashMoveSpeed = Animator.StringToHash("MoveSpeed");

        public override void OnInitialized()
        {
            animator = context.GetComponent<Animator>();
            controller = context.GetComponent<CharacterController>();
            agent = context.GetComponent<NavMeshAgent>();
        }
        // target 위치를 Enterㅇ[서 입력
        public override void OnEnter()
        {
            agent?.SetDestination(context.Target.position);
            animator?.SetBool(hashMove, true);
        }

        public override void Update(float deltaTime)
        {
            Transform enemy = context.SearchEnemy();
            if (enemy)
            {
                agent.SetDestination(context.Target.position);

                if (agent.remainingDistance > agent.stoppingDistance)
                {
                    controller.Move(agent.velocity * deltaTime);
                    animator.SetFloat(hashMoveSpeed, agent.velocity.magnitude / agent.speed, 1f, deltaTime);
                    return;
                }

            }
            if (!enemy && agent.remainingDistance <= agent.stoppingDistance)
            {
                stateMachine.ChangeState<IdleState>();
            }
        }

        public override void onExit()
        {
            animator?.SetBool(hashMove, false);
            animator?.SetFloat(hashMoveSpeed, 0f);
            agent.ResetPath();
        }
    }

}

