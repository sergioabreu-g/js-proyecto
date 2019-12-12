using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class Progress {
    public enum Fish {
        ATUN,
        ALACHA,
        BONITO,
        CIRUJANO_AZUL,
        EMPERADOR,
        GRAMMA_LORETO,
        JUREL,
        MERO,
        MOJARRA,
        PARGO_ROJO,
        LORO,
        PEZ_TROMPETERO,
        PEZ_GLOBO,
        PES_MARIPOSA_NARIGONA,
        PEZ_PAYASO,
        PEZ_VERDE_FREDDY,
        SALMON,
        SALMONETE_DE_ROCA,
        TORDO_DE_5MANCHAS
    }

    public static readonly string[] FishName = {
        "ATÚN",
        "ALACHA",
        "BONITO",
        "CIRUJANO AZUL",
        "EMPERADOR",
        "GRAMMA LORETO",
        "JUREL",
        "MERO",
        "MOJARRA",
        "PARGO ROJO",
        "LORO",
        "PEZ TROMPETERO",
        "PEZ GLOBO",
        "PEZ MARIPOSA NARIGONA",
        "PEZ PAYASO",
        "PEZ VERDE FREDDY",
        "SALMÓN",
        "SALMONETE DE ROCA",
        "TORDO DE 5 MANCHAS",
    };

    public static readonly int[] oxygenTimes = { 40, 60, 80, 100, 130 };
    public static readonly int[] maxTrash = { 5, 8, 12, 16, 20 };
    public static readonly float[] speedMultipliers = { 1f, 1.08f, 1.17f, 1.26f, 1.35f };
    public static readonly float[] spotlightMultipliers = { 1, 1.15f, 1.3f, 1.5f, 1.7f };

    public const int coinsPerTrash = 5;
    public const int coinsPerPhoto = 50;

    private int _currentCoins = 0;
    private int _collectedTrash = 0;

    private int _oxygenLevel = 0, _trashLevel = 0, _speedLevel = 0, _spotlightLevel = 0;
    private int _storyProgress = 0;
    private int[] _storyPhotoCheckpoints = { 1, 5, 10, 15, 19 };
    private bool _gameFinished = false;
    private bool _newspaperWasShown = false;

    private Dictionary<Fish, bool> _fishPhotos;

    public Progress()
    {
        _fishPhotos = new Dictionary<Fish, bool>();
        foreach (Fish fish in Enum.GetValues(typeof(Fish)))
            _fishPhotos.Add(fish, false);
    }

    public bool getFishPhoto(Fish fish)
    {
        return _fishPhotos[fish];
    }

    public void photographFish(Fish fish, bool earnCoins = true)
    {
        if (!_fishPhotos[fish])
        {
            _fishPhotos[fish] = true;
            if (earnCoins) addCoins(coinsPerPhoto);
        }
    }

    public void removePhoto(Fish fish) {
        _fishPhotos[fish] = false;
    }

    public int photosMade()
    {
        int count = 0;
        foreach (KeyValuePair<Fish, bool> photo in _fishPhotos)
            if (photo.Value) count++;

        return count;
    }

    public float photosPercentage() {
        return (float)photosMade() / _fishPhotos.Count;
    }

    public int totalFish()
    {
        return _fishPhotos.Count;
    }

    public int photosNotMade()
    {
        return totalFish() - photosMade();
    }

    public int getOxygenLevel()
    {
        return _oxygenLevel;
    }

    public bool upgradeOxygenLevel()
    {
        if (_oxygenLevel >= oxygenTimes.Length - 1) return false;
        _oxygenLevel++;
        return true;
    }

    public int getTrashLevel()
    {
        return _trashLevel;
    }

    public bool upgradeTrashLevel()
    {
        if (_trashLevel >= maxTrash.Length - 1) return false;
        _trashLevel++;
        return true;
    }

    public int getSpeedLevel()
    {
        return _speedLevel;
    }

    public bool upgradeSpeedLevel()
    {
        if (_speedLevel >= speedMultipliers.Length - 1) return false;
        _speedLevel++;
        return true;
    }

    public int getSpotlightLevel()
    {
        return _spotlightLevel;
    }

    public bool upgradeSpotlightLevel()
    {
        if (_spotlightLevel >= spotlightMultipliers.Length - 1) return false;
        _spotlightLevel++;
        return true;
    }

    public int getOxygenTime()
    {
        return oxygenTimes[_oxygenLevel];
    }

    public int getMaxTrash()
    {
        return maxTrash[_trashLevel];
    }

    public float getSpeedMultiplier()
    {
        return speedMultipliers[_speedLevel];
    }

    public float getSpotlightMultiplier()
    {
        return spotlightMultipliers[_spotlightLevel];
    }

    public int getCurrentCoins()
    {
        return _currentCoins;
    }

    public void addTrash(int trash, bool earnCoins = true)
    {
        if (trash <= 0) return;
        _collectedTrash += trash;
        if (earnCoins) addCoins(trash * coinsPerTrash);
    }

    public void addCoins(int coins)
    {
        if (coins <= 0) return;
        _currentCoins += coins;
    }

    public void removeCoins(int coins)
    {
        _currentCoins = Math.Max(0, _currentCoins - Math.Abs(coins));
    }

    public int getMaxOxygenLevel() {
        return oxygenTimes.Length;
    }

    public int getMaxSpeedLevel() {
        return speedMultipliers.Length;
    }

    public int getMaxTrashLevel() {
        return maxTrash.Length;
    }
    public int getMaxSpotlightLevel() {
        return spotlightMultipliers.Length;
    }

    public bool isOxygenMaxed() {
        return _oxygenLevel == oxygenTimes.Length - 1;
    }

    public bool isSpeedMaxed() {
        return _speedLevel == speedMultipliers.Length - 1;
    }

    public bool isTrashMaxed() {
        return _trashLevel == maxTrash.Length - 1;
    }

    public bool isSpotlightMaxed() {
        return _spotlightLevel == spotlightMultipliers.Length - 1;
    }

    public int getMoneyPerPhoto() {
        return coinsPerPhoto;
    }

    public int getMoneyPerTrash() {
        return coinsPerTrash;
    }

    public void setStoryProgress(int storyProgress) {
        _storyProgress = storyProgress;

        if (_storyProgress == _storyPhotoCheckpoints.Length - 1)
            _gameFinished = true;
    }

    public int getStoryProgress() {
        int photos = photosMade();
        int i = 0;
        for (; i < _storyPhotoCheckpoints.Length; i++) {
            if (photos < _storyPhotoCheckpoints[i]) break;
        }

        return i;
    }

    public bool isGameFinished()
    {
        return _gameFinished;
    }

    public void setNewspaperShown(bool shown)
    {
        _newspaperWasShown = shown;
    }

    public bool wasNewsPaperShown()
    {
        return _newspaperWasShown;
    }
}
