using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PolygonCollider2D))]
public class PlayerPhotos : MonoBehaviour
{
    public Player player;

    private PolygonCollider2D _photoCollider;
    
    // Start is called before the first frame update
    void Start()
    {
        _photoCollider = GetComponent<PolygonCollider2D>();
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
        if (fish == null) return;
        
        if (!player.GetProgress().getFishPhoto(fish.fishType)) {
            player.GetProgress().photographFish(fish.fishType);
            fish.deactivateTrail();
        }
    }
}
