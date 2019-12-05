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

    private PlayerLight[] _playerLights;

    private bool _insideWater = false;
    private Rigidbody2D _rb;
    private TrashCollector _trashCollector;
    private PlayerMovement _playerMovement;
    private Oxygen _oxygen;
    private PlayerPhotos _playerPhotos;

    public PlayerSpotlight _playerSpotlight;
    public FadingUI deadUI;

    public void Awake()
    {
        _progress = new Progress();
        _playerLights = GetComponentsInChildren<PlayerLight>();
    }

    public void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _trashCollector = GetComponent<TrashCollector>();
        _playerMovement = GetComponent<PlayerMovement>();
        _oxygen = GetComponent<Oxygen>();
        _playerPhotos = GetComponentInChildren<PlayerPhotos>();

        updateAllLevels();

        resetTransform();
    }

    public void Update() {
        updateInsideWater();

#if UNITY_EDITOR
        if (Input.GetKeyDown(KeyCode.Alpha1)) {
            _progress.upgradeOxygenLevel();
            _progress.upgradeSpeedLevel();
            _progress.upgradeSpotlightLevel();
            _progress.upgradeTrashLevel();
            updateAllLevels();
        }
#endif
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

            _progress.addTrash(_trashCollector.getCurrentTrash());
            _trashCollector.clearTrash();
        }

    }

    public bool insideWater()
    {
        return _insideWater;
    }

    public void die() {
        deadUI.gameObject.SetActive(true);

        deactivateBehaviours();
        float deadTime = deadUI.activeTime - deadUI.fadeTime - 0.1f;
        Invoke("activateBehaviours", deadTime);
        Invoke("resetTransform", deadTime);
    }

    public void resetTransform()
    {
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

    public void updateAllLevels()
    {
        _playerMovement.updateLevel();
        foreach (PlayerLight pLight in _playerLights)
        {
            pLight.updateLevel();
        }

        _trashCollector.updateLevel();
        _oxygen.updateLevel();
    }

    private void deactivateBehaviours()
    {
        _oxygen.enabled = false;
        _playerMovement.enabled = false;
        _playerPhotos.enabled = false;
        _rb.gravityScale = 0;
    }

    public void activateBehaviours()
    {
        _oxygen.enabled = true;
        _playerMovement.enabled = true;
        _playerPhotos.enabled = true;
        _rb.gravityScale = _airGravityScale;

        updateInsideWater();
    }
}
