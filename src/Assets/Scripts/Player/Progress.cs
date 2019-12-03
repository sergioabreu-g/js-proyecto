using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Progress {
    public enum Fish {
        ATUN,
    }

    public static readonly int[] oxygenTimes = { 10, 15, 25, 40, 70 };
    public static readonly int[] maxTrash = { 5, 7, 12, 15, 20 };
    public static readonly float[] speedMultipliers = { 1, 1.25f, 1.45f, 1.65f, 1.8f };
    public static readonly float[] spotlightMultipliers = { 1, 1.3f, 1.6f, 1.8f, 2f };

    public const int coinsPerTrash = 5;
    public const int coinsPerPhoto = 50;

    private int _currentCoins = 0;
    private int _collectedTrash = 0;

    private int _oxygenLevel = 0, _trashLevel = 0, _speedLevel = 0, _spotlightLevel = 0;

    private Dictionary<Fish, bool> _fishPhotos;
    private Fish _lastCapture;

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
            _lastCapture = fish;

            if (earnCoins) addCoins(coinsPerPhoto);
        }
    }

    public int photosMade()
    {
        int count = 0;
        foreach (KeyValuePair<Fish, bool> photo in _fishPhotos)
            if (photo.Value) count++;

        return count;
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

    public void upgradeOxygenLevel()
    {
        _oxygenLevel = Math.Min(_oxygenLevel + 1, oxygenTimes.Length - 1);
    }

    public int getTrashLevel()
    {
        return _trashLevel;
    }

    public void upgradeTrashLevel()
    {
        _trashLevel = Math.Min(_trashLevel + 1, maxTrash.Length - 1);
    }

    public int getSpeedLevel()
    {
        return _speedLevel;
    }

    public void upgradeSpeedLevel()
    {
        _speedLevel = Math.Min(_speedLevel + 1, speedMultipliers.Length - 1);
    }

    public int getSpotlightLevel()
    {
        return _spotlightLevel;
    }

    public void upgradeSpotlightLevel()
    {
        _spotlightLevel = Math.Min(_spotlightLevel + 1, spotlightMultipliers.Length - 1);
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
        _currentCoins -= Math.Max(0, _currentCoins - Math.Abs(coins));
    }
}
