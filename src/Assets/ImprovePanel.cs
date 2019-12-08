using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImprovePanel : MonoBehaviour
{
    public int coinsPerUpgrade = 65;
    public GameObject plusOxygen, plusSpeed, plusTrash, plusSpotlight;
    public GameObject oxygenMark, speedMark, trashMark, spotlightMark;
    public float markDistance;

    private Progress _progress;

    private void Start() {
        _progress = Player.GetProgress();
        _progress.addCoins(5000);

        UpdateIcons(false);
    }

    public void UpgradeOxygen() {
        if (_progress.getCurrentCoins() >= coinsPerUpgrade && _progress.upgradeOxygenLevel()) {
            _progress.removeCoins(coinsPerUpgrade);
            updateOxygen();
        }
    }

    public void UpgradeSpeed() {
        if (_progress.getCurrentCoins() >= coinsPerUpgrade && _progress.upgradeSpeedLevel()) {
            _progress.removeCoins(coinsPerUpgrade);
            updateSpeed();
        }
    }

    public void UpgradeTrash() {
        if (_progress.getCurrentCoins() >= coinsPerUpgrade && _progress.upgradeTrashLevel()) {
            _progress.removeCoins(coinsPerUpgrade);
            updateTrash();
        }
    }

    public void UpgradeSpotlight() {
        if (_progress.getCurrentCoins() >= coinsPerUpgrade && _progress.upgradeSpotlightLevel()) {
            _progress.removeCoins(coinsPerUpgrade);
            updateSpotlight();
        }
    }

    public void UpdateIcons(bool updateOnlyLast = true) {
        updateOxygen(updateOnlyLast);
        updateSpeed(updateOnlyLast);
        updateTrash(updateOnlyLast);
        updateSpotlight(updateOnlyLast);
    }

    void updateOxygen(bool updateOnlyLast = true) {
        if (_progress.isOxygenMaxed()) plusOxygen.SetActive(false);
        if (_progress.getOxygenLevel() > 0) oxygenMark.SetActive(true);

        int i = updateOnlyLast ? _progress.getOxygenLevel() - 1 : 1;
        for (; i < _progress.getOxygenLevel(); i++) {
            GameObject temp = Instantiate(oxygenMark, transform);
            Vector2 pos = temp.transform.position;
            pos.x += i * markDistance;
            temp.transform.position = pos;
        }
    }

    void updateSpeed(bool updateOnlyLast = true) {
        if (_progress.isSpeedMaxed()) plusSpeed.SetActive(false);
        if (_progress.getSpeedLevel() > 0) speedMark.SetActive(true);

        int i = updateOnlyLast ? _progress.getSpeedLevel() - 1 : 1;
        for (; i < _progress.getSpeedLevel(); i++) {
            GameObject temp = Instantiate(speedMark, transform);
            Vector2 pos = temp.transform.position;
            pos.x += i * markDistance;
            temp.transform.position = pos;
        }
    }

    void updateTrash(bool updateOnlyLast = true) {
        if (_progress.isTrashMaxed()) plusTrash.SetActive(false);
        if (_progress.getTrashLevel() > 0) trashMark.SetActive(true);

        int i = updateOnlyLast ? _progress.getTrashLevel() - 1 : 1;
        for (; i < _progress.getTrashLevel(); i++) {
            GameObject temp = Instantiate(trashMark, transform);
            Vector2 pos = temp.transform.position;
            pos.x += i * markDistance;
            temp.transform.position = pos;
        }
    }

    void updateSpotlight(bool updateOnlyLast = true) {
        if (_progress.isSpotlightMaxed()) plusSpotlight.SetActive(false);
        if (_progress.getSpotlightLevel() > 0) spotlightMark.SetActive(true);

        int i = updateOnlyLast ? _progress.getSpotlightLevel() - 1 : 1;
        for (; i < _progress.getSpotlightLevel(); i++) {
            GameObject temp = Instantiate(spotlightMark, transform);
            Vector2 pos = temp.transform.position;
            pos.x += i * markDistance;
            temp.transform.position = pos;
        }
    }
}
