using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextureMovement : MonoBehaviour
{
    public Vector2 ScrollSpeed = new Vector2(1.0f, 0.0f);
    private SpriteRenderer renderer;
    void Start()
    {
        renderer = GetComponent<SpriteRenderer>();
        renderer.material.SetVector("_ScrollSpeed", ScrollSpeed);
    }

    //Vector2 uvOffset = Vector2.zero;
    //void LateUpdate()
    //{
    //    uvOffset += (ScrollSpeed * Time.deltaTime);
    //    if (renderer.enabled)
    //    {
    //        //Debug.Log(uvOffset);
    //        renderer.material.SetVector("_ScrollSpeed", ScrollSpeed);

    //    }
    //}
}
