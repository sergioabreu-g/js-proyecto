using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//struct TrackerConfig { }

[System.Serializable]
public enum PersistenceType { LOCAL_SYNCHRO, LOCAL_ASYNCHO, SERVER_ASYNCHRO};
[System.Serializable]
public enum SerializeType { JSON, CSV};

public class EventTracker : MonoBehaviour
{
    [Header("General")] [SerializeField]
    private bool trackingActive = true;
    [SerializeField] private bool debug = false;
    [SerializeField] private int flushTimer = 15;

    [SerializeField] private PersistenceType pType = PersistenceType.LOCAL_SYNCHRO;
    [SerializeField] private SerializeType sType = SerializeType.CSV;


    private IPersistance persistence;
    private ISerializer serializer;


    // unused, en el enunciado aparece una lista de trackers?¿?¿?
    private List<Event> events;

    private string id;
    private static EventTracker Instance = null;

    private void Start() {
        if (Instance == null) {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            id = System.DateTime.Now.ToString();
            id = id.Replace("/", "").Replace(" ", "_").Replace(":", ""); // folder format issue
            events = new List<Event>(0);

            SetupPersistence();

            //RegisterStartEvent();

            InvokeRepeating("Flush", flushTimer, flushTimer);
        }
        else
            Destroy(gameObject);
    }

    private void OnDestroy() {
        if (Instance == this)
            RegisterEndEvent();
    }

    private void SetupPersistence()
    {
        switch (sType)
        {
            case SerializeType.CSV:
                serializer = new myCSVSerializer();
                break;
            case SerializeType.JSON:
                serializer = new myJsonSerializer();
                break;
        }

        switch (pType)
        {
            case PersistenceType.LOCAL_SYNCHRO:
                persistence = new FilePersistanceQueued(serializer, id);
                break;
            case PersistenceType.LOCAL_ASYNCHO:
                persistence = new FilePersistanceQueued(serializer, id);
                break;
            case PersistenceType.SERVER_ASYNCHRO:
                persistence = new FilePersistanceServer(serializer, id);
                break;
        }
    }

    public static EventTracker GetInstance() {
        return Instance;
    }

    public void Flush() {
        if (debug)
            Debug.Log("Telemetry: Event Tracker flushing");

        persistence.Flush();
    }

    public void TrackEvent(Event ev) {
        if (!trackingActive)
            return;

        if (debug)
            Debug.Log("Telemetry: tracking event " + ev.GetType() + ".");

        persistence.Send(ev);

        if (ev.GetFlush())
            Flush();
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
