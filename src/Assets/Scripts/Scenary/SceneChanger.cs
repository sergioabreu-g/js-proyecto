﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    public string sceneName;
    public bool onCancel = true;
    public bool resetProgress = false;

    public void ChangeScene() {
        if (resetProgress)
        {
#if UNITY_EDITOR
            Debug.LogWarning("RESETTING PROGRESS");
#endif
            Player.ResetProgress();
        }

        SceneManager.LoadScene(sceneName);
    }

    private void Update() {
        if (onCancel && Input.GetButtonUp("Cancel"))
        {
            ChangeScene();
        }
    }
}
