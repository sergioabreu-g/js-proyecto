using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


[RequireComponent(typeof(Text))]
public class UI_Oxygen : MonoBehaviour
{
    public Oxygen oxygen;
    private Text _text;

    private void Start() {
        _text = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        _text.text = ((int)oxygen.getCurrentOxygen()).ToString();
    }
}
