using System.Collections;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    public GameObject PauseButton;

    public float movespeed = 2;

    private GameObject Enemy;
    public Animator animator;

    private Vector3 destination;
    private Vector3 Path;
    private Vector3 pos;

    private float x1;
    private float x2;
    private float y1;
    private float y2;

    private bool moving = false;

    void Start()
    {
        animator = GetComponent<Animator>();
        Enemy = gameObject;

        newDestination();
        forMotion();

        int state = Random.Range(0, 5);
        animator.SetInteger("Color", state);

        StartCoroutine(Wait());
    }

    void Update()
    {
        if (moving)
        {
            if (Mathf.Abs(Enemy.transform.position.x - pos.x) > 0.1 || Mathf.Abs(Enemy.transform.position.y - pos.y) > 0.1)
            {
                Enemy.transform.position += Path * movespeed * Time.deltaTime;
            }
            else
            {
                newDestination();
                forMotion();
            }
        }
    }

    void newDestination()
    {
        float x;
        float y;
        x = Random.Range(10, Screen.width - 10);
        y = Random.Range(10, Screen.height - 10);

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

    public void Death()
    {
        Destroy(gameObject);
    }

    private IEnumerator Wait()
    {
        float time = Random.Range(0, 100) / 10;
        yield return new WaitForSeconds(time);
        moving = true;
    }
}
