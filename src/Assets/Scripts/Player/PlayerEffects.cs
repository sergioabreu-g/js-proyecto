using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Player))]
[RequireComponent(typeof(Rigidbody2D))]
public class PlayerEffects : MonoBehaviour
{
    public ParticleSystem _particles;
    public float speedToEmissionRatio = 1;

    private Player _player;
    private Rigidbody2D _rb;

    void Start() {
        _player = GetComponent<Player>();
        _rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        ParticleSystem.EmissionModule em = _particles.emission;
        float emissionRatio = _rb.velocity.magnitude * speedToEmissionRatio;
        em.rateOverTime = _player.insideWater() ? emissionRatio : 0;
    }
}
