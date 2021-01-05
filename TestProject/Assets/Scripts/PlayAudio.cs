using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayAudio : MonoBehaviour
{
    private void Start()
    {
        if (PlayerPrefs.GetString("Volume") == "off")
        {
            for (int i = 0; i < 5; i++)
            {
                gameObject.transform.GetChild(i).GetComponent<AudioSource>().volume = 0f;
            }
        }
    }

    public void Click()
    {
        gameObject.transform.GetChild(0).GetComponent<AudioSource>().Play();
    }

    public void Death()
    {
        gameObject.transform.GetChild(1).GetComponent<AudioSource>().Play();
    }

    public void Coin()
    {
        gameObject.transform.GetChild(2).GetComponent<AudioSource>().Play();
    }
    public void Eat()
    {
        gameObject.transform.GetChild(3).GetComponent<AudioSource>().Play();
    }

    public void Idle()
    {
        gameObject.transform.GetChild(4).GetComponent<AudioSource>().Play();
    }

    public void Starta()
    {
        //gameObject.transform.GetChild(5).GetComponent<AudioSource>().Play();
    }

    public void StopAllAudio()
    {
        for (int i = 0; i < 5; i++)
        {
            gameObject.transform.GetChild(i).GetComponent<AudioSource>().Stop();
        }
    }

    public void StopIdle()
    {
        gameObject.transform.GetChild(4).GetComponent<AudioSource>().Stop();
    }
}
