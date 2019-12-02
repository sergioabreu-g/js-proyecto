using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]
public class Fish : MonoBehaviour
{
    public Progress.Fish fishType;
    private ParticleSystem _trail;

    private void Start()
    {
        _trail = GetComponent<ParticleSystem>();
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
}
