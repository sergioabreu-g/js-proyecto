using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour {
    private Rigidbody2D _rb;
    public float acceleration;
    public float maxSpeed;

	// Use this for initialization
	void Start () {
        _rb = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
        Vector2 currentVel = _rb.velocity;

        float hMov = Input.GetAxis("Horizontal");
        if (hMov != 0 && currentVel.magnitude < maxSpeed) _rb.AddForce(Vector2.right * hMov * Time.deltaTime * acceleration);

        float vMov = Input.GetAxis("Vertical");
        if (vMov != 0 && currentVel.magnitude < maxSpeed) _rb.AddForce(Vector2.up * vMov * Time.deltaTime * acceleration);

        float angle = Mathf.Atan2(currentVel.y, currentVel.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle + 90, Vector3.forward);
    }
}
