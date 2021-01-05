using UnityEngine;

public class BackgroundEnemy : MonoBehaviour
{
    private GameObject Enemy;
    public Animator animator;

    private Vector3 destination;
    private Vector3 Path;
    private Vector3 pos;

    private float movespeed;

    // Start is called before the first frame update
    void Start()
    {
        Enemy = gameObject;
        animator = GetComponent<Animator>();

        newDestination();
        forMotion();

        movespeed = Random.Range(5, 10);

        int state = Random.Range(0, 5);
        animator.SetInteger("Color", state);
    }

    // Update is called once per frame
    void Update()
    {
        if (Mathf.Abs(Enemy.transform.position.x - pos.x) > 0.1 || Mathf.Abs(Enemy.transform.position.y - pos.y) > 0.1)
        {
            Enemy.transform.position += Path * movespeed * Time.unscaledDeltaTime;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void newDestination()
    {
        float x;
        float y;

        if (Camera.main.WorldToScreenPoint(new Vector3(Enemy.transform.position.x, 0, 0)).x <= 0)
        {
            x = Screen.width + 500;
            y = Random.Range(0, Screen.height);
        }
        else
        {
            x = -500;
            y = Random.Range(0, Screen.height);
        }

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
