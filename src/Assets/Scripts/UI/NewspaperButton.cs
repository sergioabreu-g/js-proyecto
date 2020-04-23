using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewspaperButton : SceneChanger
{
    private void Start()
    {
        if (!Player.GetProgress().isGameFinished())
            gameObject.SetActive(false);
        else if (!Player.GetProgress().wasNewsPaperShown())
            ChangeScene();
    }
}
