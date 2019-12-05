using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Light))]
public class PlayerSpotlight : PlayerLight {
    public Oxygen oxygen;

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
}
