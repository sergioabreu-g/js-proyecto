using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Float : MonoBehaviour
{
    private float movTime = 0;
    public float speed = 1, amplitude = 1;
    private Vector3 iniPos;
    // Start is called before the first frame update
    void Start()
    {
        iniPos = transform.position;
        speed = Random.Range(0.80f, 1.2f) * speed;
        amplitude = Random.Range(0.80f, 1.2f) * amplitude;
    }

    // Update is called once per frame
    void Update()
    {
        float time = Time.deltaTime;
        movTime += time * speed;
        float mov = Mathf.Sin(movTime) * amplitude;

        transform.position = iniPos + new Vector3(0, mov, 0);
    }
}
