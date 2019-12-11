using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class Fish : MonoBehaviour
{
    public Progress.Fish fishType;
    public int sortingOrderPrePhoto;
    public int sortingOrderPostPhoto;

    private SpriteRenderer _spriteRenderer;

    private void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        _spriteRenderer.sortingOrder = Player.GetProgress().getFishPhoto(fishType)? sortingOrderPostPhoto : sortingOrderPrePhoto;
    }

    public Sprite getSprite()
    {
        return _spriteRenderer.sprite;
    }
}
