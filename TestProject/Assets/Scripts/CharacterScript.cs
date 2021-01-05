using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class CharacterScript : MonoBehaviour
{
    public GameObject point;
    public GameObject Character;
    public GameObject lr;
    public GameObject text;
    public GameObject main;
    public GameObject canvas;
    public GameObject NewBest;
    public GameObject PlusCoin;
    public GameObject Audio;
    public LinkedList<GameObject> dots = new LinkedList<GameObject>();

    public float movespeed = 2;
    public float rotateZ;

    private Vector3 vec;
    private Vector3 Path;
    private Vector3 pos;

    private Animator animator;

    float x1;
    float x2;
    float y1;
    float y2;

    private int score = 0;

    private bool Pause = false;
    private bool start = false;

    private float ragemodeTime;

    void Start()
    {
        animator = GetComponent<Animator>();
        
        lr.SendMessage("AddChar", gameObject.transform);

        vec = Character.transform.position;
        pos = new Vector3(1, 1, 0);

        ragemodeTime = 0f;

        StartCoroutine(SkipAnim());
    }

    public void Instant(GameObject[] prefs)
    {
        lr = prefs[0];
        text = prefs[1];
        main = prefs[2];
        canvas = prefs[3];
        NewBest = prefs[4];
        PlusCoin = prefs[5];
        Audio = prefs[6];
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Food(Clone)")
        {
            Audio.SendMessage("Eat");

            Destroy(collision.gameObject);

            score += 50;
            text.SendMessage("addScore", score);

            main.SendMessage("AddDifficulty", score / 300);

            if (PlayerPrefs.GetInt("Score") < score)
                NewBest.SetActive(true);

            animator.SetBool("Ragemode", true);
            main.SendMessage("CharacterRaged");
            ragemodeTime = 3f;
        }

        if (collision.gameObject.name == "Enemy(Clone)")
        {
            if (animator.GetBool("Ragemode"))
            {
                Audio.SendMessage("Eat");

                collision.gameObject.SendMessage("Death");

                score += 100;
                text.SendMessage("addScore", score);

                main.SendMessage("AddDifficulty", score / 300);

                if (PlayerPrefs.GetInt("Score") < score)
                    NewBest.SetActive(true);
            }
            else
            {
                Audio.SendMessage("StopIdle");
                if (dots.First != null)
                {
                    dots.First.Value.SendMessage("destroy");
                    dots.RemoveFirst();
                }
                StartCoroutine(DeathAnim());

                if (PlayerPrefs.GetInt("Score") < score)
                    PlayerPrefs.SetInt("Score", score);
            }
        }

        if (collision.gameObject.name == "Coin(Clone)")
        {
            Audio.SendMessage("Coin");

            int val = PlayerPrefs.GetInt("Coins");
            if (score < 5000)
            {
                val += (score / 1000) + 1;
                NewBest.SendMessage("PlusChange", (score / 1000) + 1);
            }
            else
            {
                val += 5;
                NewBest.SendMessage("PlusChange", 5);
            }
            PlayerPrefs.SetInt("Coins", val);

            PlusCoin.SetActive(true);

            Destroy(collision.gameObject);
        }
    }

    void Update()
    {
        //rotate character 
        rotateZ = Mathf.Atan2(Path.y, Path.x) * Mathf.Rad2Deg;
        Character.transform.rotation = Quaternion.Euler(0f, 0f, rotateZ);

        if (Input.GetMouseButtonDown(0) && !Pause && start)
        {
            vec = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            vec.z = 0;

            double d = Math.Sqrt(Math.Pow(Character.transform.position.x - vec.x, 2) + Math.Pow(Character.transform.position.y - vec.y, 2));

            if (d > 0.5)
            {
                float x = Input.mousePosition.x;
                float y = Input.mousePosition.y;

                //create new point
                if (!((x1 < x && x2 > x) && (y1 < y && y2 > y)))
                {
                    dots.AddLast(Instantiate(point, vec, Quaternion.identity));
                    //delete previous point
                    if (dots.Last != dots.First)
                    {

                        dots.Last.Previous.Value.SendMessage("destroy");
                        dots.Remove(dots.Last.Previous);
                    }

                    forMotion();
                }
            }
        }

        //moving character and destroying point if it reached the destination
        if (Mathf.Abs(Character.transform.position.x - pos.x) > 0.1 || Mathf.Abs(Character.transform.position.y - pos.y) > 0.1)
        {
            Character.transform.position += Path * movespeed * Time.deltaTime;
        }
        else if (dots.First != null)
        {
            dots.Last.Value.SendMessage("destroy");
            dots.RemoveLast();
        }

        Ragemode();
    }

    //counting the position of destination
    void forMotion()
    {
        Path.x = dots.Last.Value.transform.position.x - Character.transform.position.x;
        Path.y = dots.Last.Value.transform.position.y - Character.transform.position.y;

        float len = Mathf.Sqrt(sqr(Path.x) + sqr(Path.y));
        Path.x /= len;
        Path.y /= len;

        pos = dots.Last.Value.transform.position;
    }

    //Function for pause
    public void PauseGame()
    {
        if (Pause)
        {
            Audio.SendMessage("Idle");
            Pause = false;
        }
        else
        {
            Audio.SendMessage("StopAllAudio");
            Pause = true;
        }
    }

    public void SetButtonPosition1(Vector3 vec)
    {
        x1 = vec.x;
        y1 = vec.y;
    }
    public void SetButtonPosition2(Vector3 vec)
    {
        x2 = vec.x;
        y2 = vec.y;
    }

    float sqr(float x1)
    {
        return x1 * x1;
    }

    void Ragemode()
    {
        if (ragemodeTime <= 0)
            animator.SetBool("Ragemode", false);
        else
            ragemodeTime -= Time.deltaTime;
    }

    public void Initialise()
    {
        Audio.SendMessage("Idle");
        Character.transform.position = new Vector3(0, 0, 0);
        score = 0;
        text.SendMessage("addScore", score);
        main.SendMessage("KillThemAll");
    }

    public void BackOnStage()
    {
        Time.timeScale = 1f;
        main.SendMessage("KillThemAll");
        animator.SetBool("Death", false);

        StartCoroutine(SkipAnim());
    }

    IEnumerator SkipAnim()
    {
        Audio.SendMessage("Starta");
        yield return new WaitForSeconds(1.3f);
        start = true;
        Audio.SendMessage("Idle");
    }

    IEnumerator DeathAnim()
    {
        Audio.SendMessage("Death");
        start = false;
        animator.updateMode = AnimatorUpdateMode.UnscaledTime;
        Time.timeScale = 0f;
        animator.SetBool("Death", true);
        yield return new WaitForSecondsRealtime(1f);
        animator.updateMode = AnimatorUpdateMode.Normal;
        canvas.SendMessage("EndGame");
    }
}
