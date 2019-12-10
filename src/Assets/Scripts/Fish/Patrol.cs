using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class Patrol : MonoBehaviour
{
    // Start is called before the first frame update
    private SpriteRenderer _spriteRenderer;
    public Vector3 right, left;
    public float speed = 1, rotOffset = 1, rotSpeed = 1;

    private Vector3 direction;
    private float initialSpeed, rotation, rotTime = 0f;
    void Start()
    {
        initialSpeed = speed;
        left += transform.position;
        right += transform.position;
        direction = left - right;
        direction.Normalize();
        transform.position = right;
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        float time = Time.deltaTime;
        rotTime += time * rotSpeed;
        rotation = Mathf.Sin(rotTime) * rotOffset;
        Vector3 rot = transform.eulerAngles;
        //rot.y = rotation;
        rot.z = rotation / 10;
        transform.eulerAngles = rot;

        transform.Translate(direction * time * speed, Space.World);
        if ((0.3 > Vector3.Distance(left, transform.position) && speed == initialSpeed) || (0.3 > Vector3.Distance(right, transform.position) && speed < initialSpeed))
        {
            speed = -speed;
            _spriteRenderer.flipX = !_spriteRenderer.flipX;
        }
    }
}
