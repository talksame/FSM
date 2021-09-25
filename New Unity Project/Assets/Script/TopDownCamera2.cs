using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FSM.Cameras
{
    public class TopDownCamera2 : MonoBehaviour
    {

        #region Variables

        public float height = 5f;
        public float distance = 10f;
        public float angle = 45f;


        public float lookAtHeight = 2f;
        public float smoothSpeed = 0.5f;

        private Vector3 refVelocity;

        public Transform target;
        #endregion Variables

        private void LateUpdate()
        {
            HandleCamera();
        }

        public void HandleCamera()
        {
            if (!target)
            {
                return;
            }

            //카메라의 현재 위치를 계산함.

            Vector3 worldPosition = (Vector3.forward * -distance) + (Vector3.up * height);
            //Debug.DrawLine(target.position, worldPosition, Color.red);//

            //카메라의 회전을 정의함, Y 축의 UP Vector 기준으로 회전값을 계산한다.
            Vector3 rotatedVector = Quaternion.AngleAxis(angle, Vector3.up) * worldPosition;


            //이후에 카메라의 포지션에 맞춰서 움직이도록 한다.
            Vector3 flatTargetPosition = target.position;
            flatTargetPosition.y += lookAtHeight;

            Vector3 finalPosition = flatTargetPosition + rotatedVector;

            transform.position = Vector3.SmoothDamp(transform.position, finalPosition, ref refVelocity, smoothSpeed);
            //Vector3 smoothedPosition = Vector3.Lerp(transform.position, finalPosition, m_SmoothSpeed);
            //transform.position = smoothedPosition;

            transform.LookAt(target.position);
        }


        private void OnDrawGizmos()
        {
            Gizmos.color = new Color(1f, 0f, 0f, 0.5f);
            if (target)
            {
                Vector3 lookAtPosition = target.position;
                lookAtPosition.y += lookAtHeight;
                Gizmos.DrawLine(transform.position, lookAtPosition);
                Gizmos.DrawSphere(lookAtPosition, 0.25f);
            }

            Gizmos.DrawSphere(transform.position, 0.25f);
        }
    }
}

