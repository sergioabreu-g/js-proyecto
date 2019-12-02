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

    public void photographFish(Fish fish)
    {
        _fishPhotos[fish] = true;
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
