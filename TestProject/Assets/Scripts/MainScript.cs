using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainScript : MonoBehaviour
{
    public GameObject Food;
    public GameObject Enemy;
    public GameObject Coin;
    public GameObject PauseButton;
    public GameObject canvas;
    public GameObject character;
    public GameObject[] enemyarr;
    public LinkedList<GameObject> Enemylist;
    public Animator[] EnemyAnimator;
    public GameObject[] EnemyArray;

    public float FoodTimeLeft;
    public float CoinTimeLeft;
    public float EnemyTimeLeft;

    private float x1;
    private float x2;
    private float y1;
    private float y2;
    private float ragemodeTime;
    public int difficulty = 1;

    void Start()
    {
        EnemyArray = new GameObject[difficulty];
        EnemyAnimator = new Animator[difficulty];

        Enemylist = new LinkedList<GameObject>();

        AddFoodTime();
        AddCoinTime();

        Vector3 vec1;
        Vector3 vec2;
        vec1 = Camera.main.WorldToScreenPoint(new Vector3(PauseButton.transform.position.x - 0.5f, PauseButton.transform.position.y - 0.5f, 0));
        vec2 = Camera.main.WorldToScreenPoint(new Vector3(PauseButton.transform.position.x + 0.5f, PauseButton.transform.position.y + 0.5f, 0));
        x1 = vec1.x;
        y1 = vec1.y;
        x2 = vec2.x;
        y2 = vec2.y;

        ragemodeTime = 0f;

        character.SendMessage("SetButtonPosition1", vec1);
        character.SendMessage("SetButtonPosition2", vec2);
    }


    void Update()
    {
        FoodTimeLeft -= Time.deltaTime;
        CoinTimeLeft -= Time.deltaTime;
        EnemyTimeLeft -= Time.deltaTime;
        if (FoodTimeLeft < 0)
        {
            Instantiate(Food, newPosition(), Quaternion.identity);
            AddFoodTime();
        }

        if (CoinTimeLeft < 0)
        {
            Instantiate(Coin, newPosition(), Quaternion.identity);
            AddCoinTime();
        }

        CheckDifficulty();

        Ragemode();
    }

    public void AddDifficulty(int x)
    {

        if (x >= 1)
        {
            if (x < 10)
                difficulty = x;
        }
        else
            difficulty = 1;


        System.Array.Resize<GameObject>(ref EnemyArray, difficulty);
        System.Array.Resize<Animator>(ref EnemyAnimator, difficulty);
    }

    private void CheckDifficulty()
    {
        for (int i = 0; i < difficulty; i++)
        {
            if (EnemyArray[i] == null)
            {
                NewEnemy(i);
            }
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
            x = Random.Range(-100, -80);
            y = Random.Range(Screen.height + 100, Screen.height - 100);
        } else if (choise == 1)
        {
            x = Random.Range(Screen.width - 100, Screen.width + 100);
            y = Random.Range(Screen.height + 80, Screen.height + 100);
        } else if (choise == 2)
        {
            x = Random.Range(Screen.width + 80, Screen.width + 100);
            y = Random.Range(Screen.height + 100, Screen.height - 100);
        } else if (choise == 3)
        {
            x = Random.Range(Screen.width - 100, Screen.width + 100);
            y = Random.Range(-100, -80);
        }

        Vector3 vec = new Vector3(x, y, 0);
        vec = Camera.main.ScreenToWorldPoint(vec);
        vec.z = 0;

        return vec;
    }

    void AddFoodTime()
    {
        FoodTimeLeft = Random.Range(3f, 5f);
    }

    void AddCoinTime()
    {
        CoinTimeLeft = Random.Range(5f, 10f);
    }

    public void CharacterRaged()
    {
        ragemodeTime = 3f;
    }

    public void KillThemAll()
    {
        for (int i = 0; i < difficulty; i++)
        {
            EnemyArray[i].SendMessage("Death");
        }
    }

    public void Ragemode()
    {
        for (int i = 0; i < difficulty; i++)
        {
            if (EnemyAnimator[i] != null)
            {
                if (ragemodeTime <= 0)
                {
                    EnemyAnimator[i].SetBool("Scared", false);
                }
                else
                {

                    EnemyAnimator[i].SetBool("Scared", true);
                }
            }
        }
        ragemodeTime -= Time.deltaTime;
    }

    private void NewEnemy(int index)
    {
        EnemyArray[index] = Instantiate(Enemy, newEnemyPosition(), Quaternion.identity);
        EnemyAnimator[index] = EnemyArray[index].GetComponent<Animator>();
    }
}

