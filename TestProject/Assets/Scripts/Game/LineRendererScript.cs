using UnityEngine;  

public class LineRendererScript : MonoBehaviour
{
    public float drawSpeed = 6f;
    public Texture[] textures = new Texture[9]; 

    private LineRenderer lr;
    private Transform origin;
    private Transform destination;

    private int toDraw = 0;
    private int textureCount = 0;
    public float time;

    public float realtime;

    private void Awake()
    {
        lr = GetComponent<LineRenderer>();
        toDraw = 1;

        time = Time.deltaTime;
    }

    //Function that is needed to add point
    public void AddPoint(GameObject obj)
    {
        origin = obj.transform;
        Vector3 vector3 = obj.transform.position;
        vector3.z = -1;
        origin.position = vector3;
        toDraw++;
        time = Time.time;
        textureCount = 0;
    }

    //Function to delete second point's transform
    public void DellSndPoint()
    {
        toDraw--;
    }

    //Function that is called only once to add origin point(Character)
    public void AddChar(Transform tr) 
    {
        destination = tr;
    }

    //Line animation
    private void SpriteAnimation()
    {
        if (textureCount < 8)
        {
            lr.material.SetTexture("_MainTex", textures[textureCount]);
            textureCount++;
        }
        else if (textureCount == 8)
        {
            lr.material.SetTexture("_MainTex", textures[7]);
            textureCount++;
        }
        else if (textureCount == 9)
        {
            lr.material.SetTexture("_MainTex", textures[6]);
            textureCount++;
        }
    }

    private void FixedUpdate()
    {
        SpriteAnimation();
        lr.positionCount = toDraw;

        //drawing a line
        if (toDraw > 1)
        {
            lr.SetPosition(0, origin.position);
            lr.SetPosition(1, destination.position);
        }

        realtime = Time.time;
    }
}
