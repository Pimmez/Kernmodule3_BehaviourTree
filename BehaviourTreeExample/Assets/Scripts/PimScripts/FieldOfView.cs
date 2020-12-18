using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldOfView : MonoBehaviour
{
    public float viewRadius;
    
    [Range(0, 360)]
    public float viewAngle;

    public LayerMask targetMask;
    public LayerMask obstacleMask;

    [HideInInspector]
    public List<Transform> visibleTargets = new List<Transform>();

    public bool IsHumanoidVisible
    {
        get { return isHumanoidVisible; }
        set { isHumanoidVisible = value; }
    }
    private bool isHumanoidVisible;

    private void Start()
    {
        StartCoroutine("FindTargetsWithDelay", 0.2f); 
    }

    public Vector3 DirFromAngle(float angleInDegrees, bool angleIsGlobal)
    {
        if(!angleIsGlobal)
        {
            angleInDegrees += transform.eulerAngles.y; 
        }
        return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0, Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
    }

    private IEnumerator FindTargetsWithDelay(float _delay)
    {
        while(true)
        {
            yield return new WaitForSeconds(_delay);
            FindVisibleTargets();
        }
    }

    private void FindVisibleTargets()
    {
        visibleTargets.Clear();
        Collider[] targetsInViewRadius = Physics.OverlapSphere(transform.position, viewRadius, targetMask);

        for (int i = 0; i < targetsInViewRadius.Length; i++)
        {
            Transform _target = targetsInViewRadius[i].transform;
            Vector3 _dirToTarget = (_target.position - transform.position).normalized;
            if(Vector3.Angle(transform.forward, _dirToTarget) < viewAngle / 2)
            {
                float _dstToTarget = Vector3.Distance(transform.position, _target.position);
                
                if(!Physics.Raycast(transform.position, _dirToTarget, _dstToTarget, obstacleMask))
                {
                    visibleTargets.Add(_target);
                    isHumanoidVisible = true;
                }
            }
        }
    }
}