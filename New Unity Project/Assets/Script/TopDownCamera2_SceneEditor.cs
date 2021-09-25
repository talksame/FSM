using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


namespace FSM.Cameras
{
    //� ī�޶� Ŀ�������� �� �� ������ �־�� �Ѵ�.
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

        //targetCamera�� �޾ƿ� ī�޶� ����ϱ� ���ؼ� �ʿ��� OnsceneGuI �Լ�
        private void OnSceneGUI()
        {
            if (!targetCamera || !targetCamera.target)
            {
                return;
            }

            //ī�޶� �����Ѵٸ�, ī�޶��� ���� ��ġ�� �޾ƿ��� �ڵ�
            Transform cameraTarget = targetCamera.target;
            Vector3 targetPosition = cameraTarget.position;
            targetPosition.y += targetCamera.lookAtHeight;

            // Draw distance cricle // ȸ���ϴ� ���� ǥ���ϴ� ��. ������ Ȯ���� ��
            Handles.color = new Color(1f, 0f, 0f, 0.15f);
            Handles.DrawSolidDisc(targetPosition, Vector3.up, targetCamera.distance);

            Handles.color = new Color(1f, 0f, 0f, 0.75f);
            Handles.DrawWireDisc(targetPosition, Vector3.up, targetCamera.distance);

            //������ ������ �� �ֵ��� �Ѵ�.
            //ScaleSlider�� ���ؼ� ī�޶� ���Ͻ� �� ���̸� ������ �� �ֵ��� �մϴ�.
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

            // ���̺� ����.
            GUIStyle labelStyle = new GUIStyle();
            labelStyle.fontSize = 15;
            labelStyle.normal.textColor = Color.white;
            labelStyle.alignment = TextAnchor.UpperCenter;
            //���ڸ� �׸��� �Լ� ���ͽ� �� ����Ʈ�� ����Ѵ�.
            Handles.Label(targetPosition + (-cameraTarget.forward * targetCamera.distance), "Distance", labelStyle);

           
            labelStyle.alignment = TextAnchor.MiddleRight;
            Handles.Label(targetPosition + (Vector3.up * targetCamera.height), "Height", labelStyle);

            targetCamera.HandleCamera();




        }
    }

}

