using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(PolygonCollider2D))]
public class PlayerPhotos : MonoBehaviour
{
    public Player player;
    public GameObject photoUI;

    private PolygonCollider2D _photoCollider;
    private Text _photoUIText;
    private Image _photoUIImage;

    // Start is called before the first frame update
    void Start()
    {
        _photoCollider = GetComponent<PolygonCollider2D>();
        _photoUIText = photoUI.GetComponentInChildren<Text>();
        _photoUIImage = photoUI.GetComponentsInChildren<Image>()[1];
    }

    // Update is called once per frame
    void Update()
    {
        _photoCollider.enabled = Input.GetButtonDown("Photo");
        if (!_photoCollider.enabled) return;

        Vector3 mouseScreen = Input.mousePosition;
        Vector3 mouse = Camera.main.ScreenToWorldPoint(mouseScreen);
        float mouseAngle = -Mathf.Atan2(mouse.y - transform.position.y, mouse.x - transform.position.x) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.Euler(0, 0, -mouseAngle);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Fish fish = collision.gameObject.GetComponent<Fish>();
        if (fish != null) photographFish(fish);
    }

    public void photographFish(Fish fish)
    {
        if (!player.GetProgress().getFishPhoto(fish.fishType))
        {
            player.GetProgress().photographFish(fish.fishType);
            photoUI.SetActive(true);
            _photoUIText.text = fish.fishName;
            _photoUIImage.sprite = fish.getSprite();
            fish.deactivateTrail();
        }
    }
}
