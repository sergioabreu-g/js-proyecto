using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuCoins : MonoBehaviour
{
    Text _coinText;

    // Start is called before the first frame update
    void Start()
    {
        _coinText = GetComponentInChildren<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        _coinText.text = Player.GetProgress().getCurrentCoins().ToString();
    }
}
