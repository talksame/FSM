using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldOfView : MonoBehaviour
{
    public float viewRaidus = 5f;
    [Range(0, 360)]
    public float viewAngle = 90f;

    public LayerMask targetMask;
    public LayerMask obstalceMask;


    private List<Transform> visibleTargets  = new List<Transform>();

    // ������Ƽ �߰�
    public List<Transform> VisibleTargets => visibleTargets;
    
    private Transform nearestTarget;
    private float distanceToTarget = 0.0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        FindVisibleTargets();
    }

    void FindVisibleTargets()
    {
        distanceToTarget = 0.0f;
        nearestTarget = null;
        visibleTargets.Clear();


        Collider[] targetsInViewRadius = Physics.OverlapSphere(transform.position, viewRaidus, targetMask);

        for (int i = 0; i < targetsInViewRadius.Length ; ++i){
            Transform target = targetsInViewRadius[i].transform;

            Vector3 dirtoTarget = (target.position = transform.position).normalized;
            // �ᱹ�� �þ߰��� �ﰢ������ ����Ѵ�.
            if ( Vector3.Angle(transform.forward, dirtoTarget) < viewAngle / 2)
            {
                float dstToTarget = Vector3.Distance(transform.position, target.position);
                // ���� ��ġ���� �þ߰����� �������� ����� ��, ��ֹ��� ���θ� �ľ��Ѵ�.
                if ( !Physics.Raycast(transform.position, dirtoTarget, dstToTarget, obstalceMask))
                {
                    visibleTargets.Add(target);
                    if( nearestTarget == null || (distanceToTarget > dstToTarget))
                    {
                        nearestTarget = target;
                        distanceToTarget = dstToTarget;

                    }
                }
            }
        }

    }


    public Vector3 DirFromAngle(float angleInDegrees, bool angleIsGlobal)
    {
        if (!angleIsGlobal)
        {
            angleInDegrees += transform.eulerAngles.y;
        }

        return new Vector3(
            Mathf.Sin(angleInDegrees * Mathf.Deg2Rad),
            0,
            Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
    }
}
