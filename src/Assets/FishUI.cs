using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CanvasGroup))]
public class FishUI : MonoBehaviour
{
    public float activeTime = 2;
    public float fadeTime = 0.1f;

    private float _currentTime = 0;
    private CanvasGroup _canvasGroup;

    // Start is called before the first frame update
    void Start()
    {
        _canvasGroup = GetComponent<CanvasGroup>();
    }

    // Update is called once per frame
    void Update()
    {
        _currentTime += Time.deltaTime;

        if (_currentTime < fadeTime)
            _canvasGroup.alpha = _currentTime / fadeTime;
        else if (_currentTime > activeTime - fadeTime && _currentTime < activeTime)
            _canvasGroup.alpha =  (activeTime - _currentTime) / fadeTime;
        else if
            (_currentTime >= activeTime) {
            _currentTime = 0;
            _canvasGroup.alpha = 0;
            gameObject.SetActive(false);
        }
        else _canvasGroup.alpha = 1;
    }

}
