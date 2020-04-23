using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//struct TrackerConfig { }

public class EventTracker : MonoBehaviour
{
    private List<Event> events;
    private string id;
    public static EventTracker Instance = null;
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
            Destroy(this);
        id = System.DateTime.Now.ToString();
        events = new List<Event>(0);
    }

    public void TrackEvent(Event ev)
    {
        events.Add(ev);
    }

    public void RegisterStartEvent() {
        StartEvent ev = new StartEvent(id);
        TrackEvent(ev);
    }
    public void RegisterEndEvent()
    {
        EndEvent ev = new EndEvent(id);
        TrackEvent(ev);
        //Clear?
    }
    public void RegisterDeathtEvent(int photos, int garbage, int lightUp, int speedUp, int oxygenUp, int bagUp, int maxUp)
    {
        int result = (int)(100 * (lightUp + speedUp + oxygenUp + bagUp) / (float)maxUp);
        DeathEvent ev = new DeathEvent(photos, garbage, result);
        TrackEvent(ev);
    }

    public void RegisterPhotoEvent(Progress.Fish fType)
    {
        PhotoEvent ev = new PhotoEvent(fType);
        TrackEvent(ev);
    }

    public void RegisterUpgradeEvent(int upLevel, int upType)
    {
        BuyUpgradeEvent ev = new BuyUpgradeEvent(upLevel, upType);
        TrackEvent(ev);
    }

    public void RegisterEnterEvent(int nGarbage, int mon)
    {
        EnterBoatEvent ev = new EnterBoatEvent(nGarbage, mon);
        TrackEvent(ev);
    }
}
