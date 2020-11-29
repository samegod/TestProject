using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DotScript : MonoBehaviour
{
    private GameObject lr;
    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
        lr = GameObject.Find("LineRenderer");
        lr.SendMessage("addPoint", gameObject);
    }

    //Call all needed functions to destroy the dot
    void destroy()
    {
        animator.SetBool("destroying", true);
        lr = GameObject.Find("LineRenderer");
        lr.SendMessage("dellSndPoint");
        Destroy(gameObject, 0.35f);
    }
}
