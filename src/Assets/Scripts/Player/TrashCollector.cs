using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Player))]
public class TrashCollector : MonoBehaviour
{
    public int maxTrash;
    private int currentTrash = 0;

    private Player _player;

    void Start() {
        _player = GetComponent<Player>();
    }

    public bool addTrash(int trash = 1) {
        if (trash > 0 && currentTrash < maxTrash) {
            currentTrash = Mathf.Clamp(currentTrash + trash, 0, maxTrash);
            _player.updateTrash();
            return true;
        }

        return false;
    }

    public void clearTrash() {
        currentTrash = 0;
        _player.updateTrash();
    }

    public int getCurrentTrash() {
        return currentTrash;
    }

    public float getTrashPercentage() {
        return (float)currentTrash / maxTrash;
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        Trash trash = collision.gameObject.GetComponent<Trash>();
        if (trash != null && addTrash()) {
            Destroy(collision.gameObject);
        }
    }

    public void updateLevel()
    {
        maxTrash = _player.GetProgress().getMaxTrash();
    }
}
