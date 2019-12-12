using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BoatInteraction : MonoBehaviour
{
    public string boatSceneName = "Boat";
    public string newspaperSceneName = "Final";
    public UI_InteractionTip controlsTip;
    public string additionalText = "para entrar al barco";

    private Player _player = null;

    void Update()
    {
        if (_player != null && Input.GetButtonUp("Interact")) {
            _player.updateProgress(false);

            if (Player.GetProgress().isGameFinished() && !Player.GetProgress().wasNewsPaperShown()) {
                Player.GetProgress().setNewspaperShown(true);
                SceneManager.LoadScene(newspaperSceneName);
            }
            else {
                SceneManager.LoadScene(boatSceneName);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        Player player = collision.GetComponent<Player>();
        if (player == null) return;

        _player = player;
        _player.giveTrash();
        controlsTip.setTip(additionalText);
    }

    private void OnTriggerExit2D(Collider2D collision) {
        Player player = collision.GetComponent<Player>();
        if (player == null) return;

        controlsTip.unsetTip();
        _player = null;
    }
}
