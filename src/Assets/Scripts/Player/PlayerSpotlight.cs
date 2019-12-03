using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Light))]
public class PlayerSpotlight : MonoBehaviour {
    private Light _light;

    private float baseRange;
    private float baseIntensity;
    private float baseAngle;

    public Player player;

    private void Start() {
        _light = GetComponent<Light>();

        baseAngle = _light.spotAngle;
        baseIntensity = _light.intensity;
        baseRange = _light.range;
    }

    void Update()
    {
        if (Input.GetButtonDown("Spotlight")) {
            player.setSpotlightActive(!player.isSpotlightActive());
            _light.enabled = player.isSpotlightActive();
        }

        if (!player.isSpotlightActive()) return;

        Vector3 mouseScreen = Input.mousePosition;
        Vector3 mouse = Camera.main.ScreenToWorldPoint(mouseScreen);
        float mouseAngle = -Mathf.Atan2(mouse.y - transform.position.y, mouse.x - transform.position.x) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.Euler(mouseAngle, 90, 0);
    }

    public void updateLevel()
    {
        float spotlightMultiplier = player.GetProgress().getSpotlightMultiplier();

        _light.spotAngle = baseAngle * spotlightMultiplier;
        _light.range = baseRange * spotlightMultiplier;
        _light.intensity = baseIntensity * spotlightMultiplier;
    }
}
