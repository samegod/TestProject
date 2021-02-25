using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroyOnLoad : MonoBehaviour
{
    public void Click()
    {
        if (PlayerPrefs.GetString("Volume") != "off")
            gameObject.GetComponent<AudioSource>().Play();
    }
}
