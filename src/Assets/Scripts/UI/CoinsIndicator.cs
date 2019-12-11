using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinsIndicator : MonoBehaviour
{
    private Text _text;

    // Start is called before the first frame update
    void Start()
    {
        _text = GetComponentInChildren<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        _text.text = Player.GetProgress().getCurrentCoins().ToString();
    }
}
