using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLight : MonoBehaviour
{
    public Player player;
    public float progressMultiplier = 1;

    protected Light _light;

    protected float baseRange;
    protected float baseIntensity;
    protected float baseAngle;

    void Start()
    {
        _light = GetComponent<Light>();

        baseAngle = _light.spotAngle;
        baseRange = _light.range;
        baseIntensity = _light.intensity;
    }

    public void updateLevel()
    {
        float spotlightMultiplier = Player.GetProgress().getSpotlightMultiplier();

        _light.spotAngle = baseAngle * spotlightMultiplier * progressMultiplier;
        _light.range = baseRange * spotlightMultiplier * progressMultiplier;
        _light.intensity = baseIntensity * spotlightMultiplier * progressMultiplier;
    }
}
