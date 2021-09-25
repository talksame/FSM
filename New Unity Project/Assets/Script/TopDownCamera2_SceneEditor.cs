using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


namespace FSM.Cameras
{
    //어떤 카메라를 커스텀으로 할 지 지정해 주어야 한다.
    [CustomEditor(typeof(TopDownCamera2))]
    public class TopDownCamera2_SceneEditor : Editor
    {
        #region Variabls
        private TopDownCamera2 targetCamera;
        #endregion Variables
        // Start is called before the first frame update

        public override void OnInspectorGUI()
        {
            targetCamera = (TopDownCamera2)target;
            base.OnInspectorGUI();
        }

        //targetCamera에 받아온 카메라를 사용하기 위해서 필요한 OnsceneGuI 함수
        private void OnSceneGUI()
        {
            if (!targetCamera || !targetCamera.target)
            {
                return;
            }

            //카메라가 존재한다면, 카메라의 현재 위치를 받아오는 코드
            Transform cameraTarget = targetCamera.target;
            Vector3 targetPosition = cameraTarget.position;
            targetPosition.y += targetCamera.lookAtHeight;

            // Draw distance cricle // 회전하는 양을 표현하는 값. 눈으로 확인할 때
            Handles.color = new Color(1f, 0f, 0f, 0.15f);
            Handles.DrawSolidDisc(targetPosition, Vector3.up, targetCamera.distance);

            Handles.color = new Color(1f, 0f, 0f, 0.75f);
            Handles.DrawWireDisc(targetPosition, Vector3.up, targetCamera.distance);

            //씬에서 수정할 수 있도록 한다.
            //ScaleSlider를 통해서 카메라 디스턴스 및 높이를 수정할 수 있도록 합니다.
            Handles.color = new Color(1f, 0f, 0f, 0.75f);
            targetCamera.distance = Handles.ScaleSlider(targetCamera.distance, 
                targetPosition, 
                -cameraTarget.forward, 
                Quaternion.identity, 
                targetCamera.distance, 0.1f);
            targetCamera.distance = Mathf.Clamp(targetCamera.distance, 2f, float.MaxValue);

            Handles.color = new Color(0f, 0f, 1f, 0.5f);
            targetCamera.height = Handles.ScaleSlider(targetCamera.height, 
                targetPosition, 
                Vector3.up, 
                Quaternion.identity, 
                targetCamera.height, 0.1f);
            targetCamera.height = Mathf.Clamp(targetCamera.height, 2f, float.MaxValue);

            // 레이블 설정.
            GUIStyle labelStyle = new GUIStyle();
            labelStyle.fontSize = 15;
            labelStyle.normal.textColor = Color.white;
            labelStyle.alignment = TextAnchor.UpperCenter;
            //글자를 그리는 함수 디스터스 와 하이트를 출력한다.
            Handles.Label(targetPosition + (-cameraTarget.forward * targetCamera.distance), "Distance", labelStyle);

           
            labelStyle.alignment = TextAnchor.MiddleRight;
            Handles.Label(targetPosition + (Vector3.up * targetCamera.height), "Height", labelStyle);

            targetCamera.HandleCamera();




        }
    }

}

