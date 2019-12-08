using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    public string sceneName;

    public void ChangeScene() {
        SceneManager.LoadScene(sceneName);
    }

    private void Update() {
        if (Input.GetButtonUp("Cancel")) ChangeScene();
    }
}
