using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(ParticleSystem))]
public class Fish : MonoBehaviour
{
    public Progress.Fish fishType;
    public string fishName = "ATÚN";

    private ParticleSystem _trail;
    private SpriteRenderer _spriteRenderer;

    private void Start()
    {
        _trail = GetComponent<ParticleSystem>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        // TESTING
        Vector3 pos = transform.position;
        pos.x += Time.deltaTime;
        transform.position = pos;
    }

    public void deactivateTrail()
    {
        ParticleSystem.EmissionModule em = _trail.emission;
        em.rateOverTime = 0;
    }

    public Sprite getSprite()
    {
        return _spriteRenderer.sprite;
    }
}
