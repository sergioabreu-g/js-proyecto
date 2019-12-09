using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Rigidbody2D))]
public class PlayerAnim : MonoBehaviour
{
    private Animator _animator;
    private Rigidbody2D _rb;

    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponent<Animator>();
        _rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        _animator.SetFloat("speed", _rb.velocity.magnitude);

        Vector3 scale = transform.localScale;
        scale.x = transform.rotation.z < 0? 1 : -1;
        transform.localScale = scale;
    }
}
