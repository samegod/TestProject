using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class LineRendererScript : MonoBehaviour
{
    private LineRenderer lr;
    public Transform[] points;
    private int size;

    private void Awake()
    {
        lr = GetComponent<LineRenderer>();
        lr.positionCount = 1;
    }

    public void addPoint(GameObject obj)
    {
        Debug.Log(obj.name);

        Transform[] arr = new Transform[points.Length + 1];
        for (int i = 0; i < points.Length; i++)
        {
            arr[i] = points[i];

        }
        arr[points.Length] = obj.transform;
        points = arr;
        lr.positionCount++;
    }

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

    public void addChar(Transform tr) 
    {
        Transform[] arr = { tr };
        points = arr;
    }

    private void Update()
    {
        for(int i = 0; i < points.Length; i++)
        {
            lr.SetPosition(i, points[i].position);
        }
    }
}
