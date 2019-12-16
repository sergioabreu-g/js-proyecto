using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaveAlpha : MonoBehaviour
{
    public float speed = 1;
    private float movTime = 0;
    private Image image;
    void Start()
    {
        image = GetComponent<Image>();
    }

    void Update()
    {
        float time = Time.deltaTime;
        movTime += time * speed;

        var tempColor = image.color;
        tempColor.a = (Mathf.Sin(movTime) + 1)/ 2;
        

        image.color = tempColor;
    }
}
