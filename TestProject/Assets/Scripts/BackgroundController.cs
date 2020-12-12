using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundController : MonoBehaviour
{
    public GameObject EnemyBack;
    public float timeLeft;

    // Start is called before the first frame update
    void Start()
    {
        timeLeft = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (timeLeft <= 0)
        {
            Instantiate<GameObject>(EnemyBack, NewPosition(), Quaternion.identity);
            NewTime();
        }
        else
            timeLeft -= Time.unscaledDeltaTime;
    }

    Vector3 NewPosition()
    {
        float x;
        float y;
        Vector3 newPos;
        int choise;
        choise = Random.Range(0, 2);
        if (choise == 0)
        {
            x = Screen.width + 500;
            y = Random.Range(0, Screen.height);
        }
        else
        {
            x = -500;
            y = Random.Range(0, Screen.height);
        }

        newPos = new Vector3(x, y, 0);
        newPos = Camera.main.ScreenToWorldPoint(newPos);
        newPos.z = 3;

        return newPos;
    }

    private void NewTime()
    {
        timeLeft = Random.Range(0.5f, 0.9f);
    }
}
