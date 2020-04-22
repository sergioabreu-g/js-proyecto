using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EventType { START, END, PHOTO, DEATH, UPGRADE, ENTERBOAT}

public abstract class Event
{
    protected EventType type;
    protected double timeStamp;
}

[System.Serializable]
public class StartEvent: Event, ISerializable
{
    string id;
    public StartEvent(string ident)
    {
        type = EventType.START;
        id = ident;
        timeStamp = Time.time;
    }

    public StartEvent() { id = "" } // Empty constructor required to compile.
    // Implement this method to serialize data. The method is called on serialization.
    public void GetObjectData(SerializationInfo info, StreamingContext context)
    {
        // Use the AddValue method to specify serialized values.
        info.AddValue("type", type, typeof(EventType));
        info.AddValue("id", id, typeof(string));
        info.AddValue("timeStamp", timeStamp, typeof(double));
    }
    // The special constructor is used to deserialize values.
    public MyItemType(SerializationInfo info, StreamingContext context)
    {
        // Reset the property value using the GetValue method.
        type = (string) info.GetValue("type", typeof(EventType));
        id = (string) info.GetValue("id", typeof(string));
        timeStamp = (string) info.GetValue("timeStamp", typeof(double));
    }
}

public class EndEvent : Event
{
    string id;
    public EndEvent(string ident)
    {
        type = EventType.END;
        id = ident;
        timeStamp = Time.time;
    }
}

public class PhotoEvent : Event
{
    Progress.Fish fishType;
    public PhotoEvent(Progress.Fish fType)
    {
        fishType = fType;
        type = EventType.PHOTO;
        timeStamp = Time.time;
    }
}

public class DeathEvent : Event
{
    int numGarbage, fishPhotos, percentUpgrade;
    public DeathEvent(int nGarbage, int fPhotos, int pUpgrades) {
        numGarbage = nGarbage;
        fishPhotos = fPhotos;
        percentUpgrade = pUpgrades;
        type = EventType.DEATH;
        timeStamp = Time.time;
    }
}

public class BuyUpgradeEvent : Event
{
    int upgradeLevel, upgradeType;
    public BuyUpgradeEvent(int upLevel, int upType)
    {
        upgradeLevel = upLevel;
        upgradeType = upType;
        type = EventType.UPGRADE;
        timeStamp = Time.time;
    }
}

public class EnterBoatEvent : Event
{
    int numGarbage, money;
    public EnterBoatEvent(int nGarbage, int mon)
    {
        numGarbage = nGarbage;
        money = mon;
        type = EventType.ENTERBOAT;
        timeStamp = Time.time;
    }
}
