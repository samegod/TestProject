using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DotScript : MonoBehaviour
{
    public GameObject lr;
    private Transform tr;

    private float scale = 0.25f;
    private bool agh = true;

    private void Start()
    {
        lr = GameObject.Find("LineRenderer");
        tr = GetComponent<Transform>();
        lr.SendMessage("addPoint", gameObject);
    }

    void Update()
    {

        if (agh)
        {
            scale += 0.005f;
            if (scale > 0.5f)
                agh = false;
        }
        else
        {
            scale -= 0.005f;
            if (scale < 0.25f)
                agh = true;
        }

        tr.localScale = new Vector3(scale, scale, 0.1f);

    }

    void destroy()
    {
        lr = GameObject.Find("LineRenderer");
        lr.SendMessage("dellSndPoint");
        Destroy(gameObject);
    }
}
