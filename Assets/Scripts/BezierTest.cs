using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BezierTest : MonoBehaviour
{
    public Transform point0;
    public Transform point1;
    public Transform point2;
    public Transform point3;
    public Transform point4;

    [Range(0, 1)] public float t;

    void Update()
    {
        transform.position = Bezier.GetPoint(point0.position,point1.position,point2.position,point3.position, point4.position, t);

        transform.rotation = Quaternion.LookRotation(Bezier.GetFirstDerivative(point0.position, point1.position, point2.position, point3.position, point4.position, t));
    }

    private void OnDrawGizmos()
    {

        int sigmentsNumber = 20;
        Vector3 preveousePoint = point0.position;

        for (int i = 0; i < sigmentsNumber + 1; i++)
        {
            float paremeter = (float) i / sigmentsNumber;
            Vector3 point = Bezier.GetPoint(point0.position, point1.position, point2.position, point3.position, point4.position, paremeter);
            Gizmos.DrawLine(preveousePoint, point);
            preveousePoint = point;
        }

    }
}
