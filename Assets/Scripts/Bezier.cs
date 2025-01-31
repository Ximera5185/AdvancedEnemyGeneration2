using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

public static class Bezier 
{
    /* public static Vector3 GetPoint(Vector3 point0,Vector3 point1,Vector3 point2,Vector3 point3,float t) 
     {
         t= Mathf.Clamp01(t);

         float oneMinusT = 1f - t;

         return
         oneMinusT*oneMinusT*oneMinusT *point0 +
         3f*oneMinusT*oneMinusT*t*point1+
         3f*oneMinusT*t*t*point2+t*t*t*point3;
     }*/

    public static Vector3 GetPoint(Vector3 point0, Vector3 point1, Vector3 point2, Vector3 point3, Vector3 point4, float t)
    {
        t = Mathf.Clamp01(t);
        float oneMinusT = 1f - t;

        return
            oneMinusT * oneMinusT * oneMinusT * oneMinusT * point0 +
            4f * oneMinusT * oneMinusT * oneMinusT * t * point1 +
            6f * oneMinusT * oneMinusT * t * t * point2 +
            4f * oneMinusT * t * t * t * point3 +
            t * t * t * t * point4;
    }
    /*public static Vector3 GetFirstDerivative(Vector3 point0, Vector3 point1, Vector3 point2, Vector3 point3, float t) 
    {
        t = Mathf.Clamp01(t);

        float oneMinusT = 1f - t;

        return 
        3f * oneMinusT*oneMinusT*(point1 - point0)+6f*oneMinusT*t*(point2 - point1)+3f*t*t*(point3 - point2);
    }*/

    public static Vector3 GetFirstDerivative(Vector3 point0, Vector3 point1, Vector3 point2, Vector3 point3, Vector3 point4, float t)
    {
        t = Mathf.Clamp01(t);
        float oneMinusT = 1f - t;

        return
            4f * oneMinusT * oneMinusT * oneMinusT * (point1 - point0) +
            12f * oneMinusT * oneMinusT * t * (point2 - point1) +
            12f * oneMinusT * t * t * (point3 - point2) +
            4f * t * t * t * (point4 - point3);
    }
}
