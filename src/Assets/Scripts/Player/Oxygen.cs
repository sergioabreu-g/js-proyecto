using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Player))]
public class Oxygen : MonoBehaviour {
    public float maxOxygen = 15;

    public Light[] playerLights;
    public Color fullOxygenColor;
    public Color lowOxygenColor;

    public float initialColorDeadzone, finalColorDeadzone;
    public Text oxygenNumber;

    private float currentOxygen = 15;
    private float[] _baseLightIntensities;
    private Player _player;

    void Awake()
    {
        _player = GetComponent<Player>();
        _baseLightIntensities = new float[playerLights.Length];
    }

    // Start is called before the first frame update
    void Start() {
    }

    // Update is called once per frame
    void Update() {
        if (_player.insideWater()) currentOxygen -= Time.deltaTime;
        else currentOxygen = maxOxygen;

        if (currentOxygen < 0) {
            _player.die();
            currentOxygen = maxOxygen;
        }

        // HUD
        float fixedColorPercent = getCurrentOxygenPercent();
        fixedColorPercent = (fixedColorPercent - finalColorDeadzone) / (initialColorDeadzone - finalColorDeadzone);
        fixedColorPercent = Mathf.Clamp(fixedColorPercent, 0, 1);
        Color currentColor = Color.Lerp(lowOxygenColor, fullOxygenColor, fixedColorPercent);

        oxygenNumber.text = ((int)(getCurrentOxygen() + 1)).ToString();
        oxygenNumber.color = currentColor;

        float intensityModifier = 3 / (currentColor.r + currentColor.g + currentColor.b);

        for (int i =0; i < playerLights.Length; i++)
        {
            playerLights[i].color = currentColor;
            playerLights[i].intensity = _baseLightIntensities[i] * intensityModifier;
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
        maxOxygen = Player.GetProgress().getOxygenTime();
        currentOxygen = maxOxygen;
        updateBaseLightIntensities();
    }

    private void updateBaseLightIntensities()
    {
        for (int i = 0; i < playerLights.Length; i++)
        {
            _baseLightIntensities[i] = playerLights[i].intensity;
        }
    }
}
