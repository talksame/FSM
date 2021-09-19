using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;

namespace FastCampus.Characters
{
    [RequireComponent(typeof(NavMeshAgent)), RequireComponent(typeof(CharacterController)), RequireComponent(typeof(Animator))]
    public class PlayerCharacter : MonoBehaviour
    {
        #region Variables

        private CharacterController controller;
        [SerializeField]
        private LayerMask groundLayerMask;

        private NavMeshAgent agent;
        private Camera camera;

        [SerializeField]
        private Animator animator;

        readonly int moveHash = Animator.StringToHash("Move");
        readonly int fallingHash = Animator.StringToHash("Falling");
        #endregion

        #region Main Methods
        // Start is called before the first frame update
        void Start()
        {
            controller = GetComponent<CharacterController>();

            agent = GetComponent<NavMeshAgent>();
            agent.updatePosition = false;
            agent.updateRotation = true;


            camera = Camera.main;
        }

        // Update is called once per frame
        void Update()
        {
            // Process mouse left button input
            if (Input.GetMouseButtonDown(0))
            {
                // Make ray from screen to world
                Ray ray = camera.ScreenPointToRay(Input.mousePosition);

                // Check hit from ray
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit, 100, groundLayerMask))
                {
                    Debug.Log("We hit " + hit.collider.name + " " + hit.point);

                    // Move our player to what we hit
                    agent.SetDestination(hit.point);
                }
            }

            if (agent.remainingDistance > agent.stoppingDistance)
            {
                controller.Move(agent.velocity * Time.deltaTime);
                animator.SetBool(moveHash, true);
            }
            else
            {
                controller.Move(Vector3.zero);
                animator.SetBool(moveHash, false);
            }

            if (agent.isOnOffMeshLink)
            {
                animator.SetBool(fallingHash, agent.velocity.y != 0.0f);
            }
            else
            {
                animator.SetBool(fallingHash, false);
            }
        }

        private void OnAnimatorMove()
        {
            Vector3 position = agent.nextPosition;
            animator.rootPosition = agent.nextPosition;
            transform.position = position;
        }
        #endregion Main Methods

        #region Helper Methods
        #endregion Helper Methods
    }
}