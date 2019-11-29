using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BoatInteraction : MonoBehaviour
{
    public string boatSceneName = "Boat";
    public UI_InteractionTip controlsTip;
    public string additionalText = "para entrar al barco";

    private bool onRange = false;

    // Update is called once per frame
    void Update()
    {
        if (onRange && Input.GetButtonUp("Interact")) {
            SceneManager.LoadScene(boatSceneName);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        onRange = true;
        controlsTip.setTip(additionalText);
    }

    private void OnTriggerExit2D(Collider2D collision) {
        onRange = false;
        controlsTip.unsetTip();
    }
}
