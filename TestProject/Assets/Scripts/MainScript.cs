using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainScript : MonoBehaviour
{
    public GameObject Food;
    public GameObject Enemy;
    public GameObject PauseButton;

    public float FoodTimeLeft;
    public float EnemyTimeLeft;

    float x1;
    float x2;
    float y1;
    float y2;

    void Start()
    {

        addFoodTime();
        addEnemyTime();

        x1 = PauseButton.transform.position.x - 50;
        x2 = PauseButton.transform.position.x + 50;
        y1 = PauseButton.transform.position.y - 50;
        y2 = PauseButton.transform.position.y + 50;
    }


    void Update()
    {
        FoodTimeLeft -= Time.deltaTime;
        EnemyTimeLeft -= Time.deltaTime;
        if (FoodTimeLeft < 0)
        {
            Instantiate(Food, newPosition(), Quaternion.identity);
            addFoodTime();
        }

        if (EnemyTimeLeft < 0)
        {
            Instantiate(Enemy, newEnemyPosition(), Quaternion.identity);

            addEnemyTime();
        }
    }

    Vector3 newPosition()
    {
        float x;
        float y;

        do
        {
            x = Random.Range(10, Screen.width - 10);
            y = Random.Range(10, Screen.height - 10);
        } while ((x1 < x && x2 > x) && (y1 < y && y2 > y));

        Vector3 vec = new Vector3(x, y, 0);
        vec = Camera.main.ScreenToWorldPoint(vec);
        vec.z = 0;

        return vec;
    }

    Vector3 newEnemyPosition()
    {
        float x = 0;
        float y = 0;
        int choise;
        choise = Random.Range(0, 4);

        if (choise == 0)
        {
            x = Random.Range(-100, -20);
            y = Random.Range(Screen.height + 100, Screen.height - 100);
        } else if (choise == 1)
        {
            x = Random.Range(Screen.width - 100, Screen.width + 100);
            y = Random.Range(Screen.height + 20, Screen.height + 100);
        } else if (choise == 2)
        {
            x = Random.Range(Screen.width + 20, Screen.width + 100);
            y = Random.Range(Screen.height + 100, Screen.height - 100);
        }else if (choise == 3)
        {
            x = Random.Range(Screen.width - 100, Screen.width + 100);
            y = Random.Range(-100, -20);
        }

        Vector3 vec = new Vector3(x, y, 0);
        vec = Camera.main.ScreenToWorldPoint(vec);
        vec.z = 0;

        return vec;
    }

    void addFoodTime()
    {
        FoodTimeLeft = Random.Range(3f, 5f);
    }

    void addEnemyTime()
    {
        EnemyTimeLeft = Random.Range(5f, 15f);
    }
}
