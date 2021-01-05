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
    public float targetX = -1;
    public GameObject ButtonsPanel;
    public GameObject ShopPanel;
    public GameObject VoluneOn;
    public GameObject VolumeOff;

    private static GameObject Music;

    private bool openShop = false;
    private bool closeShop = false;

    private void Start()
    {
        CheckCoins();
        text.text = "Рекорд: " + PlayerPrefs.GetInt("Score");

        if (PlayerPrefs.GetString("Volume") == "off")
        {
            VoluneOn.SetActive(false);
            VolumeOff.SetActive(true);
        }
        else
        {
            VoluneOn.SetActive(true);
            VolumeOff.SetActive(false);
        }
        if (GameObject.FindGameObjectsWithTag("Music").Length == 1)
        {
            Music = GameObject.FindGameObjectsWithTag("Music")[0];
        }
        else
        {
            Music = GameObject.FindGameObjectsWithTag("Music")[0];
        }
    }

    public void CheckCoins()
    {
        coins.text = PlayerPrefs.GetInt("Coins").ToString();
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

        Vector3 ShopVec = ShopPanel.transform.position;

        if (openShop && ShopPanel.transform.position.x > 0)
        {
            
            ButtonsPanel.transform.Translate(-5f * Time.deltaTime, 0, 0);
            ShopPanel.transform.Translate(-5f * Time.deltaTime, 0, 0);
        }
        else
        {
            openShop = false;
        }

        if (closeShop && ButtonsPanel.transform.position.x < 0)
        {
            ButtonsPanel.transform.Translate(5f * Time.deltaTime, 0, 0);
            ShopPanel.transform.Translate(5f * Time.deltaTime, 0, 0);
        }
        else
        {
            closeShop = false;
        }
    }

    public void Play()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(1);
    }

    public void Inventory()
    {
        closeShop = false;
        openShop = true;
    }

    public void Exit()
    {
        Debug.Log("Quit");
        Application.Quit();
    }

    public void CloseShop()
    {
        openShop = false;
        closeShop = true;
    }

    public void Volumeon()
    {
        VoluneOn.SetActive(false);
        VolumeOff.SetActive(true);
        PlayerPrefs.SetString("Volume", "off");
        Music.SendMessage("ChangeVolume");
    }

    public void Volumeoff()
    {
        VoluneOn.SetActive(true);
        VolumeOff.SetActive(false);
        PlayerPrefs.SetString("Volume", "on");
        Music.SendMessage("ChangeVolume");
    }
}
