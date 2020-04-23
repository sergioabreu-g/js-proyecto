using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//struct TrackerConfig { }

public class EventTracker : MonoBehaviour
{
    [Header("General")] [SerializeField]
    private bool trackingActive = true;
    [SerializeField] private bool debug = false;

    private List<Event> events;
    private string id;
    private static EventTracker Instance = null;

    private void Start() {
        if (Instance == null) {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            id = System.DateTime.Now.ToString();
            events = new List<Event>(0);
            RegisterStartEvent();
        }
        else
            Destroy(gameObject);
    }

    private void OnDestroy() {
        if (Instance == this)
            RegisterEndEvent();
    }

    public static EventTracker GetInstance() {
        return Instance;
    }

    public void TrackEvent(Event ev) {
        if (!trackingActive)
            return;

        if (debug)
            Debug.Log("Telemetry: tracking event " + ev.GetType() + ".");

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

    public void RegisterUpgradeEvent(int upLevel, Progress.UpgradeType upType)
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
