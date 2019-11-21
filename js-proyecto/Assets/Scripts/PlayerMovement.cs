using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour {
    public float acceleration;
    public float maxSpeed;
    public float rotationSpeed;
    public float rotationOffset;

    private Rigidbody2D _rb;

	// Use this for initialization
	void Start () {
        _rb = GetComponent<Rigidbody2D>();
	}

    // Update is called once per frame
    void Update() {
        Vector2 currentVel = _rb.velocity;
        Vector2 currentDir = Vector2.up;

        float hMov = Input.GetAxis("Horizontal");
        float vMov = Input.GetAxis("Vertical");

        if (hMov != 0 || vMov != 0) {
            currentDir = new Vector2(hMov, vMov).normalized;

            if (currentDir.magnitude < maxSpeed) {
                _rb.AddForce(Vector2.right * hMov * Time.deltaTime * acceleration);
                _rb.AddForce(Vector2.up * vMov * Time.deltaTime * acceleration);
            }
        }

        float angle = Mathf.Atan2(currentDir.y, currentDir.x) * Mathf.Rad2Deg + rotationOffset;
        Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, q, Time.deltaTime * rotationSpeed);
    }
}
