using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;

namespace FastCampus.Characters
{
    [RequireComponent(typeof(NavMeshAgent)), RequireComponent(typeof(CharacterController))]
    public class AgentControllerCharacter : MonoBehaviour
    {
        #region Variables
        private CharacterController characterController;

        [SerializeField]
        private LayerMask groundLayerMask;

        private NavMeshAgent agent;
        //Click & move에서는 카메라 변수가 필요함.
        private Camera camera;

        #endregion

        #region Unity Methods
        // Start is called before the first frame update
        void Start()
        {
            characterController = GetComponent<CharacterController>();

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
                Debug.Log("PUISH");
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
                characterController.Move(agent.velocity * Time.deltaTime);
            }
            else
            {
                characterController.Move(Vector3.zero);
            }
        }

        private void LateUpdate()
        {
            transform.position = agent.nextPosition;
        }

        #endregion Main Methods

        #region Helper Methods

        #endregion Helper Methods
    }
}