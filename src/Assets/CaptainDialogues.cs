using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class CaptainDialogues : MonoBehaviour {
    public string[] dialogues;
    private Text _text;

    void Start()
    {
        _text = GetComponent<Text>();
        _text.text = dialogues[Player.GetProgress().getStoryProgress()]; 
    }
}
