using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace FSM.Characters
{
    public class AttackState : State<EnemyController>
    {
        private Animator animator;

        private int hashAttack = Animator.StringToHash("Attack");

        public override void OnInitialized()
        {
            animator = context.GetComponent<Animator>();

        }

        public override void OnEnter()
        {
            if (context.IsAvailableAttack)
            {
                animator?.SetTrigger(hashAttack);
            }
            else
            {
                stateMachine.ChangeState<IdleState>();
            }

        }

        public override void Update(float deltaTime)
        {

        }
    }

}

