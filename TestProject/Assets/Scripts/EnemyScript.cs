using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    public GameObject PauseButton;

    public float movespeed = 2;

    private GameObject Enemy;

    private Vector3 destination;
    private Vector3 Path;
    private Vector3 pos;

    float x1;
    float x2;
    float y1;
    float y2;

    void Start()
    {
        PauseButton = GameObject.Find("Canvas").transform.GetChild(0).gameObject;
        Enemy = gameObject;
        //PauseButton = PauseButton.transform.GetChild(0).gameObject;

        x1 = PauseButton.transform.position.x - 50;
        x2 = PauseButton.transform.position.x + 50;
        y1 = PauseButton.transform.position.y - 50;
        y2 = PauseButton.transform.position.y + 50;

        newDestination();
        forMotion();
    }

    void Update()
    {
        if (Mathf.Abs(Enemy.transform.position.x - pos.x) > 0.1 || Mathf.Abs(Enemy.transform.position.y - pos.y) > 0.1)
        {
            Enemy.transform.position += Path * movespeed * Time.deltaTime;
        }else
        {
            newDestination();
            forMotion();
        }
    }

    void newDestination()
    {
        float x;
        float y;

        do
        {
            x = Random.Range(10, Screen.width - 10);
            y = Random.Range(10, Screen.height - 10);
        } while ((x1 < x && x2 > x) && (y1 < y && y2 > y));

        destination = new Vector3(x, y, 0);
        destination = Camera.main.ScreenToWorldPoint(destination);
        destination.z = 0;
    }

    void forMotion()
    {
        Path.x = destination.x - Enemy.transform.position.x;
        Path.y = destination.y - Enemy.transform.position.y;

        float len = Mathf.Sqrt(sqr(Path.x) + sqr(Path.y));
        Path.x /= len;
        Path.y /= len;

        pos = destination;
    }

    float sqr(float x1)
    {
        return x1 * x1;
    }
}
