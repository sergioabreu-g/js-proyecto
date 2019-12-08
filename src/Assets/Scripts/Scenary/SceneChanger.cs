using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    public string sceneName;
    public bool onCancel = true;
    public void ChangeScene() {
        SceneManager.LoadScene(sceneName);
    }

    private void Update() {
        if (onCancel && Input.GetButtonUp("Cancel")) ChangeScene();
    }
}
