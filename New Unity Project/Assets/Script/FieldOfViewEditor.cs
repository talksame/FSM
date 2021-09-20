using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(FieldOfView))]

public class FieldOfViewEditor : Editor
{
    private void OnSceneGUI()
    {
        FieldOfView fov = (FieldOfView)target;

        //Debug 위젯 구현 ( 원)
        Handles.color = Color.white;
        Handles.DrawWireArc(fov.transform.position, Vector3.up, Vector3.forward, 360, fov.viewRaidus);

        //시야각에 대한 디버깅 위젯 구현
        //왼쪽 모서리와 오른쪽 꼭지점 잡기 의 구현

        Vector3 viewAngleA = fov.DirFromAngle(-fov.viewAngle / 2, false);
        Vector3 viewAngleB = fov.DirFromAngle(fov.viewAngle / 2, false);

        //float x = Mathf.Sin(fov.viewAngle / 2 * Mathf.Deg2Rad) * fov.viewRaidus;
        //float z = Mathf.Cos(fov.viewAngle / 2 * Mathf.Deg2Rad) * fov.viewRaidus;


        Handles.DrawLine(fov.transform.position, fov.transform.position + viewAngleA * fov.viewRaidus);
        Handles.DrawLine(fov.transform.position, fov.transform.position + viewAngleB * fov.viewRaidus);

        // 적의 위치 확인 가시선
        Handles.color = Color.red;
        foreach ( Transform VisibleTarget in fov.VisibleTargets)
        {
            //현재 위치에서 부터 적의 위치까지 선을 그림.
            Handles.DrawLine(fov.transform.position, VisibleTarget.position);
        }


    }



}
