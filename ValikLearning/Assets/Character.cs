using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public GameObject Char;
    public float force;
    public float speed;

    private Rigidbody2D rig;

    // Start is called before the first frame update
    void Start()
    {
        rig = Char.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Vector2 vec = new Vector2(0, force * 100);
            rig.AddForce(vec);
        }

        float ax = Input.GetAxis("Horizontal");
        Char.transform.position += new Vector3(speed * Time.deltaTime * ax, 0, 0);

        if (Input.GetKey(KeyCode.W))
        {
            Char.transform.Rotate(new Vector3(0, 0, 1));
        }
    }

}
