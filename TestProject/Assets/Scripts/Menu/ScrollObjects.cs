using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollObjects : MonoBehaviour
{
    public GameObject pacs;
    public GameObject panel;
    public int Skins;

    private Vector3 screenpoint, offset;
    private float _lockedYpos;
    private bool active = false;
    private bool setActive = false;
    private float distance;

    private void Start()
    {
        distance = Skins * -3f;
    }

    // Update is called once per frame
    void Update()
    {
        if (setActive)
            if (pacs.transform.position.x < 0.1f)
            {
                active = true;
                setActive = false;
            }

        if (!active)
        {
            pacs.transform.position = Vector3.MoveTowards(pacs.transform.position, new Vector3(panel.transform.position.x, pacs.transform.position.y, pacs.transform.position.z), Time.deltaTime * Skins * 5f);
        }

        if (pacs.transform.position.x > 0 && active)
        {
            pacs.transform.position = Vector3.MoveTowards(pacs.transform.position, new Vector3(0, pacs.transform.position.y, pacs.transform.position.z), Time.deltaTime * 10f);
        }
        else if (pacs.transform.position.x < distance && active)
            pacs.transform.position = Vector3.MoveTowards(pacs.transform.position, new Vector3(distance, pacs.transform.position.y, pacs.transform.position.z), Time.deltaTime * 10f);

    }

    private void OnMouseDown()
    {
        _lockedYpos = screenpoint.x;
        offset = pacs.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenpoint.z));


        Cursor.visible = false;
    }

    private void OnMouseDrag()
    {
        Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenpoint.z);
        Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint) + offset;
        curPosition.y = _lockedYpos;
        pacs.transform.position = curPosition;
    }

    private void OnMouseUp()
    {
        Cursor.visible = true;
    }

    public void SetActive()
    {
        setActive = true;
    }

    public void SetDisabled()
    {
        active = false;
    }
}
