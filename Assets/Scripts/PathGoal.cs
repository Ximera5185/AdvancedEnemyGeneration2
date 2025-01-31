using PathCreation;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathGoal : MonoBehaviour
{
    [SerializeField] GameObject Plane;

    Vector3[] goalPositions = new Vector3 [2];
    
   
    void Start()
    {
        goalPositions [0] = gameObject.transform.position;
        goalPositions [1] = Plane.transform.position;
    }

    void Update()
    {
        
    }

    VertexPath GeneratePath(Vector3 [] points, bool closedPath)
    {
        // Create a closed, 2D bezier path from the supplied points array
        // These points are treated as anchors, which the path will pass through
        // The control points for the path will be generated automatically
        BezierPath bezierPath = new BezierPath(points, closedPath, PathSpace.xy);
        // Then create a vertex path from the bezier path, to be used for movement etc
        return new VertexPath(bezierPath);
    }


}
