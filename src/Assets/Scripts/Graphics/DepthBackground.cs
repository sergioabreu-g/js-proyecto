using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class DepthBackground : MonoBehaviour
{
    private SpriteRenderer sprite;

    public Transform player;
    public float higherY, lowestY, maxAlpha, minAlpha;

    // Start is called before the first frame update
    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        float currentAlpha = Mathf.Clamp((((player.position.y - lowestY) / (higherY - lowestY)) * maxAlpha), minAlpha, maxAlpha);
        Color color = sprite.color;
        color.a = currentAlpha;
        sprite.color = color;
    }
}
