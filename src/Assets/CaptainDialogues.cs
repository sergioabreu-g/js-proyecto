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
        int progress = Player.GetProgress().getStoryProgress();
        if (progress < dialogues.Length)
            _text.text = dialogues[progress]; 
        else {
#if UNITY_EDITOR
            Debug.LogError("No dialogue defined for level " + progress);
#endif
        }
    }
}
