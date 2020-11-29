using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class Script : MonoBehaviour
{
    public GameObject point;
    public GameObject Character;
    public GameObject lr;
    public GameObject PauseButton;
    public LinkedList<GameObject> dots = new LinkedList<GameObject>();

    public float movespeed = 2;
    public float rotateZ;

    private Vector3 vec;
    private Vector3 Path;
    private Vector3 pos;

    float x1;
    float x2;
    float y1;
    float y2;

    private bool Pause = false;
    void Start()
    {
        lr.SendMessage("addChar", gameObject.transform);
        vec = Character.transform.position;
        pos = new Vector3(1, 1, 0);

        x1 = PauseButton.transform.position.x - 50;
        x2 = PauseButton.transform.position.x + 50;
        y1 = PauseButton.transform.position.y - 50;
        y2 = PauseButton.transform.position.y + 50;
    }


    void Update()
    {
        rotateZ = Mathf.Atan2(Path.y, Path.x) * Mathf.Rad2Deg;
        Character.transform.rotation = Quaternion.Euler(0f, 0f, rotateZ);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            PauseGame();
        }

        if (Input.GetMouseButtonDown(0))
        {
            vec = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            vec.z = 0;

            float x = Input.mousePosition.x;
            float y = Input.mousePosition.y;

            if (!((x1 < x && x2 > x) && (y1 < y && y2 > y)))
            {
                dots.AddLast(Instantiate(point, vec, Quaternion.identity));

                forMotion();
            }
        }
        

        if (Mathf.Abs(Character.transform.position.x - pos.x) > 0.1 || Mathf.Abs(Character.transform.position.y - pos.y) > 0.1)
        {
            Character.transform.position += Path * movespeed * Time.deltaTime;
        }
        else if (dots.First != null)
        {
            if (dots.First == dots.Last)
            {
                forMotion();

                dots.First.Value.SendMessage("destroy");
                dots.RemoveFirst();
            }
            else
            {
                dots.First.Value.SendMessage("destroy");
                dots.RemoveFirst();

                forMotion();
            }
        }
    }

    void forMotion()
    {
        Path.x = dots.First.Value.transform.position.x - Character.transform.position.x;
        Path.y = dots.First.Value.transform.position.y - Character.transform.position.y;

        float len = Mathf.Sqrt(sqr(Path.x) + sqr(Path.y));
        Path.x /= len;
        Path.y /= len;

        pos = dots.First.Value.transform.position;
    }

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
