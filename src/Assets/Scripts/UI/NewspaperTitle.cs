using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class NewspaperTitle : MonoBehaviour
{
    public string[] finals;
    private Text _text;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = true;

        _text = GetComponent<Text>();

        if (finals.Length != Progress.trashCheckpoints.Length)
        {
            Debug.LogError("Wrong number of finals. Finals lenght must " +
                "match trash checkpoint lenght (Progress.cs)");
        }

        _text.text = finals[Player.GetProgress().getTrashStoryLevel()];
    }
}
