using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.Advertisements;

public class PauseMenu : MonoBehaviour
{
    private bool secondTry = false;

    public bool GameIsPaused = false;

    public GameObject pauseMenuUI;
    public GameObject endMenuUI;
    public GameObject Character;
    public GameObject PauseButton;
    public GameObject SecondChanceButton;

    private void Start()
    {
        if (Advertisement.isSupported)
        {
            Advertisement.Initialize("3928945", false);
        }
    }
    public void SetChar(GameObject chara)
    {

        Character = chara;
    }

    public void Update()
    {
        if (Advertisement.IsReady() && !secondTry)
        {
            SecondChanceButton.SetActive(true);
        }
        else
        {
            SecondChanceButton.SetActive(false);
        }
    }

    public void EndGame()
    {
        Time.timeScale = 0f;
        endMenuUI.SetActive(true);
        PauseButton.SetActive(false);
    }

    public void Reload()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(1);
    }

    public void SecondChance()
    {
        secondTry = true;

        if (Advertisement.IsReady())
        {
            Advertisement.Show("rewardedVideo");
            Character.SendMessage("BackOnStage");
            endMenuUI.SetActive(false);
            PauseButton.SetActive(true);
        }
    }

    public void Menu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }

    public void SetPause()
    {
        if (GameIsPaused)
        {
            Resume();
        }else
        {
            Pause();
        }
    }

    void Resume()
    {
        pauseMenuUI.SetActive(false);
        PauseButton.SetActive(true);
        Time.timeScale = 1f;
        GameIsPaused = false;
        Character.SendMessage("PauseGame");
    }

    void Pause()
    {
        pauseMenuUI.SetActive(true);
        PauseButton.SetActive(false);
        Time.timeScale = 0f;
        GameIsPaused = true;
        Character.SendMessage("PauseGame");
    }

}
