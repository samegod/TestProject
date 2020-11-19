using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DotScript : MonoBehaviour
{
    public GameObject obj;
    private Transform tr;

    private float scale = 0.25f;
    private bool agh = true;

    private void Start()
    {
        obj = GetComponent<GameObject>();
        tr = GetComponent<Transform>();
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

    IEnumerator destroy()
    {
        Debug.Log("Destroyed!");
        yield return new WaitForSeconds(0.5f);
        Destroy(gameObject, .5f);
    }
}
