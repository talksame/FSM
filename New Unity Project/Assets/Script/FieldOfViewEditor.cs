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

        //Debug ���� ���� ( ��)
        Handles.color = Color.white;
        Handles.DrawWireArc(fov.transform.position, Vector3.up, Vector3.forward, 360, fov.viewRaidus);

        //�þ߰��� ���� ����� ���� ����
        //���� �𼭸��� ������ ������ ��� �� ����

        Vector3 viewAngleA = fov.DirFromAngle(-fov.viewAngle / 2, false);
        Vector3 viewAngleB = fov.DirFromAngle(fov.viewAngle / 2, false);

        //float x = Mathf.Sin(fov.viewAngle / 2 * Mathf.Deg2Rad) * fov.viewRaidus;
        //float z = Mathf.Cos(fov.viewAngle / 2 * Mathf.Deg2Rad) * fov.viewRaidus;


        Handles.DrawLine(fov.transform.position, fov.transform.position + viewAngleA * fov.viewRaidus);
        Handles.DrawLine(fov.transform.position, fov.transform.position + viewAngleB * fov.viewRaidus);

        // ���� ��ġ Ȯ�� ���ü�
        Handles.color = Color.red;
        foreach ( Transform VisibleTarget in fov.VisibleTargets)
        {
            //���� ��ġ���� ���� ���� ��ġ���� ���� �׸�.
            Handles.DrawLine(fov.transform.position, VisibleTarget.position);
        }


    }



}
