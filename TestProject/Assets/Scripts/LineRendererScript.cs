using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class LineRendererScript : MonoBehaviour
{
    public Transform charPoint;
    public float drawSpeed = 6f;

    private LineRenderer lr;
    private Transform origin;
    private Transform destination;
    public float counter = 0;
    public float dist;

    private void Awake()
    {
        lr = GetComponent<LineRenderer>();
        lr.positionCount = 1;
    }

    //Function that is needed to add point
    public void addPoint(GameObject obj)
    {
        counter = 0;
        origin = obj.transform;
        lr.positionCount++;
    }

    //Function to delete second point's transform
    void dellSndPoint()
    {
        lr.positionCount--;
    }

    //Function that is called only once to add origin point(Character)
    public void addChar(Transform tr) 
    {
        charPoint = tr;
    }

    private void Update()
    {
        //drawing a line
        if (lr.positionCount > 1)
        {
            lr.SetPosition(0, origin.position);
            destination = charPoint;
            dist = Vector3.Distance(destination.position, origin.position);
            if (dist > 0.12)
            {
                counter += .1f / drawSpeed;
                float x = Mathf.Lerp(0, dist, counter);

                Vector3 pointA = origin.position;
                Vector3 pointB = destination.position;

                Vector3 pointAlongLine = x * Vector3.Normalize(pointB - pointA) + pointA;

                lr.SetPosition(1, pointAlongLine);
            }
        }
    }
}
