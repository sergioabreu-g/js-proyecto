using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(TrashCollector))]
[RequireComponent(typeof(PlayerMovement))]
public class Player : MonoBehaviour
{
    private Progress _progress;

    public Transform initialPos;

    [SerializeField]
    private uint _id = 0;
    [SerializeField]
    private bool _spotlight_active = false;
    [SerializeField]
    private float _waterGravityScale = 0.1f;
    [SerializeField]
    private float _airGravityScale = 1f;
    [SerializeField]
    private float _waterUpperLimit = -1.3f;
    [SerializeField]
    private float _airDrag = 0;
    [SerializeField]
    private float _waterDrag = 2f;

    private bool _insideWater = false;
    private Rigidbody2D _rb;
    private TrashCollector _trashCollector;
    private PlayerMovement _playerMovement;

    public void Start()
    {
        _progress = new Progress();

        _rb = GetComponent<Rigidbody2D>();
        _trashCollector = GetComponent<TrashCollector>();
        _playerMovement = GetComponent<PlayerMovement>();

        die();
    }

    public void Update() {
        updateInsideWater();
    }

    public bool isSpotlightActive() {
        return _spotlight_active;
    }

    public void setSpotlightActive(bool spotlight_active) {
        _spotlight_active = spotlight_active;
    }

    public uint getID() {
        return _id;
    }

    public void updateInsideWater()
    {
        if (_insideWater == transform.position.y < _waterUpperLimit) return;
        _insideWater = transform.position.y < _waterUpperLimit;

        if (_insideWater) {
            _rb.gravityScale = _waterGravityScale;
            _rb.drag = _waterDrag;
        } else {
            _rb.gravityScale = _airGravityScale;
            _rb.drag = _airDrag;

            _trashCollector.clearTrash();
        }

    }

    public bool insideWater()
    {
        return _insideWater;
    }

    public void die() {
        transform.position = initialPos.position;
        transform.rotation = initialPos.rotation;
        _rb.velocity = Vector3.zero;

        updateInsideWater();
    }

    public void updateTrash() {
        _playerMovement.movementModifier = 1 - _trashCollector.getTrashPercentage();
    }

    public Progress GetProgress()
    {
        return _progress;
    }
}
