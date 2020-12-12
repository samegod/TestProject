using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NewBest : MonoBehaviour
{
    public GameObject NewBestText;
    public GameObject PlusCoin;
    public Text plusText;
    public bool rotate = false;
    public bool rotate1 = false;
    public float z;
    public float z1;

    private bool fst = true;
    private bool sh = true;


    void Update()
    {
        z = NewBestText.transform.rotation.z * 180;

        if (rotate)
        {
            if (z > -4f)
                NewBestText.transform.Rotate(0, 0, -0.15f);
            else
                rotate = false;
        }
        else
        {
            if (z < 4f)
                NewBestText.transform.Rotate(0, 0, 0.15f);
            else
                rotate = true;
        }

        z1 = PlusCoin.transform.rotation.z * 180;

        if (rotate1)
        {
            if (z > -4f)
                PlusCoin.transform.Rotate(0, 0, -0.15f);
            else
                rotate1 = false;
        }
        else
        {
            if (z < 4f)
                PlusCoin.transform.Rotate(0, 0, 0.15f);
            else
                rotate1 = true;
        }


        if (NewBestText.activeSelf && fst)
        {
            StartCoroutine(HideBest());
            fst = false;
        }

        if (PlusCoin.activeSelf && sh)
        {
            StartCoroutine(HidePlusCoin());
            sh = false;
        }
    }

    public void PlusChange(int x)
    {
        plusText.text = "+" + x;
    }

    IEnumerator HideBest()
    {
        yield return new WaitForSeconds(5f);
        NewBestText.SetActive(false);
    }
    IEnumerator HidePlusCoin()
    {
        yield return new WaitForSeconds(1f);
        PlusCoin.SetActive(false);
        sh = true;
    }
}
