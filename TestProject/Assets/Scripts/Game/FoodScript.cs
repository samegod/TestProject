using UnityEngine;
using System.Collections;

public class FoodScript : MonoBehaviour
{
    public Sprite[] sprites = new Sprite[9];

    private SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        int id = Random.Range(0, 8);
        spriteRenderer.sprite = sprites[id];

        sprites = null;

        StartCoroutine(Anim());
    }

    IEnumerator Anim()
    {
        yield return new WaitForSeconds(9f);

        for (int i = 0; i < 6; ++i)
        {
            yield return new WaitForSeconds(0.2f);
            spriteRenderer.enabled = !spriteRenderer.enabled;
        }

        Destroy(gameObject);
    }
    
}
