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

        //quick testing file system writing
        Debug.Log("Quick testing file system writing");
        Event e = new StartEvent("testing sesion");
        ISerializer seri = new myJsonSerializer();
        IPersistance pers = new FilePersistanceQueued(seri, "testing sesion");

        pers.Flush();
        pers.Send(e);
        pers.Send(e);
        pers.Flush();
        pers.Send(e);
        pers.Flush();
        pers.Flush();
    }

    // Update is called once per frame
    void Update()
    {
        _coinText.text = Player.GetProgress().getCurrentCoins().ToString();
    }
}
