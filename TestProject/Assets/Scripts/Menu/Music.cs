using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Music : MonoBehaviour
{
    private void Start()
    {
        if (GameObject.FindGameObjectsWithTag("Music").Length == 1)
        {
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        if (PlayerPrefs.GetString("Volume") == "off")
        {
            gameObject.GetComponent<AudioSource>().volume = 0f;
        }
        else
        {
            gameObject.GetComponent<AudioSource>().volume = 0.2f;
        }
    }

    public void ChangeVolume()
    {
        if (PlayerPrefs.GetString("Volume") == "off")
        {
            gameObject.GetComponent<AudioSource>().volume = 0f;
        }
        else
        {
            gameObject.GetComponent<AudioSource>().volume = 0.2f;

        }
    }
}
