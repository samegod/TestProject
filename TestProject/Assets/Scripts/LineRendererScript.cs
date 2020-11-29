using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class LineRendererScript : MonoBehaviour
{
    public Transform[] points;
    public Transform[] toDraw;
    public float drawSpeed = 6f;

    private LineRenderer lr;
    private Transform origin;
    private Transform destination;
    private float counter = 0;
    private float dist;

    private void Awake()
    {
        lr = GetComponent<LineRenderer>();
        lr.positionCount = 1;

        toDraw = new Transform[1];
    }

    //Function that is needed to add point to the end of the array
    public void addPoint(GameObject obj)
    {
        Transform[] arr = new Transform[points.Length + 1];
        for (int i = 0; i < points.Length; i++)
        {
            arr[i] = points[i];

        }
        arr[points.Length] = obj.transform;
        points = arr;
        lr.positionCount++;
    }

    //Function to delete second point's transform and remake the array
    void dellSndPoint()
    {
        lr.positionCount--;
        int len = points.Length;
        for (int i = 1; i < len - 1; i++)
        {
            points[i] = points[i + 1];
        }

        Array.Resize(ref points, len - 1);
    }

    //Function that is called only once to add origin point(Character)
    public void addChar(Transform tr) 
    {
        Transform[] arr = { tr };
        points = arr;
        toDraw[0] = points[0];

        origin = points[0];
    }

    private void Update()
    {
        //moving point of line
        //if (points.Length > 1)
        //{
        //    lr.SetPosition(0, points[0].position);
        //    destination = points[1];
        //    dist = Vector3.Distance(origin.position, destination.position);
        //    if (counter < dist)
        //    {
        //        counter += .1f / drawSpeed;
        //        float x = Mathf.Lerp(0, dist, counter);

        //        Vector3 pointA = origin.position;
        //        Vector3 pointB = destination.position;

        //        Vector3 pointAlongLine = x * Vector3.Normalize(pointB - pointA) + pointA;

        //        lr.SetPosition(1, pointAlongLine);
        //    }
        //}


        for (int i = points.Length - 1; i >= 0; i--)
        {
            lr.SetPosition(points.Length - i - 1, points[i].position);
        }

    }
}
