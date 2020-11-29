using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class CharacterScript : MonoBehaviour
{
    public GameObject point;
    public GameObject Character;
    public GameObject lr;
    public GameObject PauseButton;
    public GameObject text;
    public LinkedList<GameObject> dots = new LinkedList<GameObject>();

    public float movespeed = 2;
    public float rotateZ;

    private Vector3 vec;
    private Vector3 Path;
    private Vector3 pos;

    private BoxCollider2D collider;

    float x1;
    float x2;
    float y1;
    float y2;

    private int score = 0;

    private bool Pause = false;
    void Start()
    {
        collider = GetComponent<BoxCollider2D>();
        lr.SendMessage("addChar", gameObject.transform);
        vec = Character.transform.position;
        pos = new Vector3(1, 1, 0);

        x1 = PauseButton.transform.position.x - 50;
        x2 = PauseButton.transform.position.x + 50;
        y1 = PauseButton.transform.position.y - 50;
        y2 = PauseButton.transform.position.y + 50;
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Food(Clone)")
        {
            Destroy(collision.gameObject);
            score += 100;
            text.SendMessage("addScore", score);
        }

        if (collision.gameObject.name == "Enemy(Clone)")
        {
            Time.timeScale = 0;
            score = 0;
            text.SendMessage("addScore", score);
        }
    }

    void Update()
    {
        //rotate character 
        rotateZ = Mathf.Atan2(Path.y, Path.x) * Mathf.Rad2Deg;
        Character.transform.rotation = Quaternion.Euler(0f, 0f, rotateZ);

        //pause the game
        if (Input.GetKeyDown(KeyCode.Space))
        {
            PauseGame();
        }

        if (Input.GetMouseButtonDown(0))
        {
            vec = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            vec.z = 0;

            double d = Math.Sqrt(Math.Pow(Character.transform.position.x - vec.x, 2) + Math.Pow(Character.transform.position.y - vec.y, 2));

            if (d > 0.5)
            {
                float x = Input.mousePosition.x;
                float y = Input.mousePosition.y;

                //create new point
                if (!((x1 < x && x2 > x) && (y1 < y && y2 > y)) && !Pause)
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
    void PauseGame()
    {
        if (Pause)
        {
            Time.timeScale = 1;
            Pause = false;
        }
        else
        {
            Time.timeScale = 0;
            Pause = true;
        }
    }

    float sqr(float x1)
    {
        return x1 * x1;
    }
}
