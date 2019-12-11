using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CanvasGroup))]
[RequireComponent(typeof(RectTransform))]
public class HiddenUI : MonoBehaviour
{
    public Vector3 hiddenPos;
    public float animationTime = 0.3f;
    public bool animateAlpha = true;

    private RectTransform _rectTransform;
    private CanvasGroup _canvasGroup;
    private Vector3 _initialPos;
    private float _timer = 0;

    private bool _visible = false;

    // Start is called before the first frame update
    void Start()
    {
        _rectTransform = GetComponent<RectTransform>();
        _canvasGroup = GetComponent<CanvasGroup>();
        _initialPos = _rectTransform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("ShowInfo")) showUI();
        else if (Input.GetButtonUp("ShowInfo")) hideUI();

        if (_visible) {
            if (_timer < animationTime) _timer += Time.deltaTime;
            else _timer = animationTime;
        }
        else {
            if (_timer > 0) _timer -= Time.deltaTime;
            else _timer = 0;
        }

        float animPercent = _timer / animationTime;
        if (animateAlpha) _canvasGroup.alpha = animPercent;
        _rectTransform.localPosition = Vector3.Lerp(hiddenPos, _initialPos, animPercent);
    }

    public void showUI() {
        _visible = true;
    }

    public void hideUI() {
        _visible = false;
    }
}
