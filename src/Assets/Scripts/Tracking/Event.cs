using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EventType { START, END, PHOTO, DEATH, UPGRADE, ENTERBOAT}

public abstract class Event 
{
    protected EventType type;
    protected double timeStamp;
}

public class StartEvent: Event
{
    string id;
    public StartEvent(string ident)
    {
        type = EventType.START;
        id = ident;
        timeStamp = Time.time;
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