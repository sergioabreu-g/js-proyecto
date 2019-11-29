using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class UI_InteractionTip : MonoBehaviour
{
    public string genericText = "Presiona E ";
    private Text _text;

    // Start is called before the first frame update
    void Start()
    {
        _text = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setTip(string text) {
        _text.text = genericText + text;
    }

    public void unsetTip() {
        _text.text = "";
    }
}
