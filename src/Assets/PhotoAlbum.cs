using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PhotoAlbum : MonoBehaviour
{
    public Image fishImage;
    public string notPhotographedText = "DESCONOCIDO";
    public Color notPhotographedColor = Color.black;
    public float separation = 350;
    public float bottomFreeSpace = 150;
    public Sprite[] sprites;

    // Start is called before the first frame update
    void Start()
    {
        int fishSize = Progress.Fish.GetNames(typeof(Progress.Fish)).Length;
        if (sprites.Length != fishSize) {
            Debug.LogError("Wrong number of sprites. Size must match the length of the Fish enum.");
            return;
        }

        RectTransform rectTransf = GetComponent<RectTransform>();

        rectTransf.sizeDelta = new Vector2(rectTransf.sizeDelta.x, separation * fishSize + bottomFreeSpace);

        for (int i = 0; i < sprites.Length; i++) {
            Image temp = Instantiate(fishImage, this.transform);
            temp.sprite = sprites[i];
            Text tempText = temp.GetComponentInChildren<Text>();
            if (Player.GetProgress().getFishPhoto((Progress.Fish)i))
                tempText.text = Progress.FishName[i];
            else {
                tempText.text = notPhotographedText;
                temp.color = notPhotographedColor;
            }

            Vector3 pos = temp.transform.localPosition;
            pos.y -= separation * (i + 1);
            temp.transform.localPosition = pos;
        }
    }

    // Update is called once per frame
    void Update()
    {
     
    }
}
