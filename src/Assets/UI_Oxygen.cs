using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


[RequireComponent(typeof(Text))]
public class UI_Oxygen : MonoBehaviour
{
    public Oxygen oxygen;
    public Color initialColor, finalColor;
    public float initialColorDeadzone, finalColorDeadzone;
    private Text _text;

    private void Start() {
        _text = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        _text.text = ((int)oxygen.getCurrentOxygen()).ToString();

        float fixedColorPercent = oxygen.getCurrentOxygenPercent();
        fixedColorPercent = (fixedColorPercent - finalColorDeadzone) / (initialColorDeadzone - finalColorDeadzone);
        fixedColorPercent = Mathf.Clamp(fixedColorPercent, 0, 1);

        _text.color = Color.Lerp(finalColor, initialColor, fixedColorPercent);
    }
}
