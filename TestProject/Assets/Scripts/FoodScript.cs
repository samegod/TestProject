using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    }

}
