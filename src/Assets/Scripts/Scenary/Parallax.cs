using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    public Transform cam;
    public float parallaxCoeficient;
    public bool alongX_axis = true, alongY_axis = false;

    private Vector2 _startPos;

    void Start() {
        _startPos = transform.position;
    }

    void LateUpdate() {
        Vector3 pos = transform.position;

        if (alongX_axis) pos.x = _startPos.x + (cam.position.x - _startPos.x) * parallaxCoeficient;
        if (alongY_axis) pos.y = _startPos.y + (cam.position.y - _startPos.y) * parallaxCoeficient;

        transform.position = pos;
    }
}
