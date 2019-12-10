using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class TextAlphaChanger : MonoBehaviour
{
    public float speed = 1;
    private float _time = 0;
    private Text _text;

    void Start() {
        _text = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        Color temp = _text.color;
        temp.a = (Mathf.Sin(_time += Time.deltaTime * speed) + 1) /2;
        _text.color = temp;
    }
}
