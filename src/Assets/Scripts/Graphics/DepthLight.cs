using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Light))]
public class DepthLight : MonoBehaviour
{
    private Light _light;

    public Transform player;
    public float higherY, lowestY, maxIntensity, minIntensity;

    // Start is called before the first frame update
    void Start()
    {
        _light = GetComponent<Light>();   
    }

    // Update is called once per frame
    void Update()
    {
        float currentIntensity = Mathf.Clamp((((Mathf.Abs(player.position.y - lowestY))/(higherY - lowestY)) * maxIntensity), minIntensity, maxIntensity);
        _light.intensity = currentIntensity;
    }
}
