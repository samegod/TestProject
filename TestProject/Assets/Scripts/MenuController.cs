using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    public Text text;
    public Text coins;
    public GameObject logo;
    public bool rotate = false;
    public float z;

    private void Start()
    {
        coins.text = PlayerPrefs.GetInt("Coins").ToString();
        text.text = "Best Score: " + PlayerPrefs.GetInt("Score");
    }

    void Update()
    {
        z = logo.transform.rotation.z * 180;

        if (rotate)
        {
            if (z> -4f)
                logo.transform.Rotate(0, 0, -0.05f);
            else
                rotate = false;
        }
        else
        {
            if (z < 4f)
                logo.transform.Rotate(0, 0, 0.05f);
            else
                rotate = true;
        }
    }

    public void Play()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(1);
    }

    public void Inventory()
    {

    }

    public void Exit()
    {
        Debug.Log("Quit");
        Application.Quit();
    }
}
