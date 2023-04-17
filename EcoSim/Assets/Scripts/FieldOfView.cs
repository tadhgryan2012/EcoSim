using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldOfView : MonoBehaviour {
    public float viewRadius;
    [Range(0, 360)]
    public float viewAngle;

    public LayerMask targetMask;

    [HideInInspector]
    public List<Transform> visibleTargets = new List<Transform>();

    void Start() {
        StartCoroutine(findTargetsWithDelay(0.2f));
    }

    IEnumerator findTargetsWithDelay(float delay) {
        while (true) {
            findVisibleTargets();
            yield return new WaitForSeconds(delay);
        }
    }

    void findVisibleTargets() {
        visibleTargets.Clear();
        Collider[] targetsInViewRadius = Physics.OverlapSphere(transform.position, viewRadius, targetMask);

        for (int i=0; i<targetsInViewRadius.Length; i++) {
            Transform target = targetsInViewRadius[i].transform;
            Vector3 dirToTarget = (target.position - transform.position).normalized;
            if (Vector3.Angle(transform.forward, dirToTarget) < viewAngle / 2) {
                visibleTargets.Add(target);
            }
        }
    }

    public Vector3 DirFromAngle(float angle, bool angleIsGlobal) {
        if (!angleIsGlobal)
            angle += transform.eulerAngles.y;
        
        return new Vector3(Mathf.Sin(angle * Mathf.Deg2Rad), 0, Mathf.Cos(angle * Mathf.Deg2Rad));
    }
}
