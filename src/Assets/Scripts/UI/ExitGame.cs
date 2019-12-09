using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitGame : MonoBehaviour {
    public bool onCancel = false;
    public void ChangeScene() {
        Application.Quit();
    }

    private void Update() {
        if (onCancel && Input.GetButtonUp("Cancel")) Application.Quit();
    }
}
