using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Script : MonoBehaviour
{
    public GameObject point;
    public GameObject Character;
    public float x = 200, y = 200;

    public LinkedList<GameObject> dots = new LinkedList<GameObject>();

    private Vector3 vec;
    private Vector3 Path;
    private Vector3 pos;

    private bool Pause;
    void Start()
    {
        vec = Character.transform.position;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (Pause)
            {
                PauseGame();
                Pause = false;
            }
            else
            {
                ResumeGame();
                Pause = true;
            }
        }
        if (Input.GetMouseButtonDown(0))
        {
            vec = new Vector3((Input.mousePosition.x - Screen.width / 2) / 53, (Input.mousePosition.y - Screen.height / 2) / 53, 0);
            dots.AddLast(Instantiate(point, vec, Quaternion.identity));

        }

        if (Mathf.Abs(Character.transform.position.x - pos.x) > 0.1 || Mathf.Abs(Character.transform.position.y - pos.y) > 0.1)
        {
            Character.transform.Translate(Path * Time.deltaTime);
        }
        else if (dots.First != null)
        {
            Debug.Log("Prepare to destroying!");

            Path.x = dots.First.Value.transform.position.x - Character.transform.position.x;
            Path.y = dots.First.Value.transform.position.y - Character.transform.position.y;

            pos = dots.First.Value.transform.position;

            dots.First.Value.SendMessage("destroy");
            dots.RemoveFirst();
        }
    }

    void PauseGame()
    {
        Time.timeScale = 0;
    }

    void ResumeGame()
    {
        Time.timeScale = 1;
    }
}
