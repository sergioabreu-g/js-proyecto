using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//struct TrackerConfig { } //directamente un prefab configurado desde el editor

[System.Serializable]
public enum PersistenceType { LOCAL_SYNCHRO, LOCAL_ASYNCHO, SERVER_ASYNCHRO};
[System.Serializable]
public enum SerializeType { JSON, CSV};

public class EventTracker : MonoBehaviour
{
    [Header("General")] [SerializeField]
    private bool trackingActive = true;
    [SerializeField] private bool debug = false;

    [Tooltip("Flush cada cierta cantidad de tiempo (recomendado)")]
    [SerializeField] private bool timedFlush = true;
    [Tooltip("Cantidad de tiempo en segundos")]
    [SerializeField] private int flushTimer = 15;

    [Tooltip("Tipo de persistencia")]
    [SerializeField] private PersistenceType pType = PersistenceType.LOCAL_SYNCHRO;
    [Tooltip("Tipo de serializacion")]
    [SerializeField] private SerializeType sType = SerializeType.CSV;
    [Tooltip("Forzar eventos de una linea (JSON menos legible)")]
    [SerializeField] private bool forzeOneLine = true;

    [Tooltip("Ordenados: START, END, PHOTO, DEATH, UPGRADE, ENTERBOAT")]
    [SerializeField] private bool[] eTypesIgnored = new bool[((int)EventType.ENTERBOAT+1)];

    //Tipo de persistencia y serializacion configurable en el editor
    private IPersistance persistence;
    private ISerializer serializer;


    private string id;
    private static EventTracker Instance = null;

    //Crea el tracker segun la configuracion
    private void Start() {
        if (Instance == null) {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            //Id creado utilizando la fecha
            id = System.DateTime.Now.ToString();
            id = id.Replace("/", "").Replace(" ", "_").Replace(":", ""); // folder format issue

            SetupSerializer(); //antes de la persistencia
            SetupPersistence();
            //RegisterStartEvent();

            //Tiempo entre flush automatico configurable
            if (timedFlush) InvokeRepeating("Flush", flushTimer, flushTimer);
        }
        else
            Destroy(gameObject);
    }

    //Evento final de partida
    private void OnDestroy() {
        if (Instance == this)
            RegisterEndEvent();
    }

    //Tipo de persistencia y serializacion configurable en el editor
    private void SetupSerializer()
    {
        Event.SetOneLineJSON(forzeOneLine);

        switch (sType)
        {
            case SerializeType.CSV:
                serializer = new myCSVSerializer();
                break;
            case SerializeType.JSON:
                serializer = new myJsonSerializer();
                break;
        }
    }
    private void SetupPersistence() {
        switch (pType) {
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


    //Patron singleton
    public static EventTracker GetInstance() {
        return Instance;
    }

    //Llamada al flush del sistema de persistencia
    public void Flush() {
        if (debug)
            Debug.Log("Telemetry: Event Tracker flushing");

        persistence.Flush();
    }

    //Metodo para mandar eventos puros
    public void TrackEvent(Event ev) {
        if (!trackingActive)
            return;

        // Ignora el evento si así lo hemos asignado
        if (!eTypesIgnored[(int)ev.GetEventType()])
        {
            if (debug)
                Debug.Log("Telemetry: tracking event " + ev.GetType() + ".");
            persistence.Send(ev);
            if (ev.GetFlush())
                Flush();
        }
    }

    //Metodos para mandar directamente los eventos
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
