using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Progress
{
    public enum Fish {
        ATUN,
    }

    public static readonly float[] oxygenTimes = { 10, 15, 25, 40, 70 };
    public static readonly int[] maxTrash = { 5, 7, 12, 15, 20 };
    public static readonly float[] speedMultiplier = { 1, 1.1f, 1.2f, 1.4f, 1.7f };
    public static readonly float[] spotLight = { 1, 1.1f, 1.2f, 1.4f, 1.7f };

    public int oxygenLevel, trashLevel, speedLevel, spotlightLevel;

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

    public void photographFish(Fish fish)
    {
        if (!_fishPhotos[fish])
        {
            _fishPhotos[fish] = true;
            _lastCapture = fish;
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
}
