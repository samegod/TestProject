using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject[] Chars;
    public GameObject[] objects;
    public GameObject mainScript;
    public GameObject pauseScript;

    // Start is called before the first frame update
    void Start()
    {
        int id = PlayerPrefs.GetInt("Char");
        GameObject obj = Instantiate<GameObject>(Chars[id]);
        obj.SendMessage("Instant", objects);

        mainScript.SendMessage("SetChar", obj);
        pauseScript.SendMessage("SetChar", obj);
    }

}
