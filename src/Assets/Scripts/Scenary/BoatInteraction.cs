using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BoatInteraction : MonoBehaviour
{
    public string boatSceneName = "Boat";
    public UI_InteractionTip controlsTip;
    public string additionalText = "para entrar al barco";

    private Player _player = null;

    void Update()
    {
        if (_player != null && Input.GetButtonUp("Interact")) {
            _player.updateProgress(false);
            SceneManager.LoadScene(boatSceneName);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        _player = collision.GetComponent<Player>();
        if (_player == null) return;

        controlsTip.setTip(additionalText);
    }

    private void OnTriggerExit2D(Collider2D collision) {
        _player = collision.GetComponent<Player>();
        if (_player == null) return;

        controlsTip.unsetTip();
        _player = null;
    }
}
