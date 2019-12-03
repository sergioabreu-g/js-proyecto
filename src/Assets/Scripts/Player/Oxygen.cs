using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Player))]
public class Oxygen : MonoBehaviour {
    public float maxOxygen = 15;
    private float currentOxygen = 15;

    private Player _player;

    // Start is called before the first frame update
    void Start() {
        _player = GetComponent<Player>();
    }

    // Update is called once per frame
    void Update() {
        if (_player.insideWater()) currentOxygen -= Time.deltaTime;
        else currentOxygen = maxOxygen;

        if (currentOxygen < 0) {
            _player.die();
            currentOxygen = maxOxygen;
        }
    }

    public float getCurrentOxygen() {
        return currentOxygen;
    }

    public float getCurrentOxygenPercent() {
        return currentOxygen / maxOxygen;
    }

    public void updateLevel()
    {
        maxOxygen = _player.GetProgress().getOxygenTime();
    }
}
