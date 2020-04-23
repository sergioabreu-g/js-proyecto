using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Newtonsoft.Json; //Libreria para JSON .Net
using System;
using System.Text;
using System.IO; //writing custom jsons

public enum EventType { START, END, PHOTO, DEATH, UPGRADE, ENTERBOAT}

public abstract class Event
{
    protected EventType type;
    protected double timeStamp;

    //override by each event type
    protected abstract void writeJSON(JsonWriter writer);

    public string ToJSON() {
        StringBuilder sb = new StringBuilder();
        StringWriter sw = new StringWriter(sb);

        using (JsonWriter writer = new JsonTextWriter(sw)) {
            writer.Formatting = Formatting.Indented;

            writer.WriteStartObject();
            writer.WritePropertyName("EventType");
            writer.WriteValue(type);
            writer.WritePropertyName("timeStamp");
            writer.WriteValue(timeStamp);

            writeJSON(writer); //write son event properties

            writer.WriteEndObject();
        }

        return sb.ToString();
    }
}

[System.Serializable]
public class StartEvent: Event//, ISerializable
{
    string id;
    public StartEvent(string ident)
    {
        type = EventType.START;
        id = ident;
        timeStamp = Time.time;
    }

    protected override void writeJSON(JsonWriter writer) {
        writer.WritePropertyName("id");
        writer.WriteValue(id);
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

    protected override void writeJSON(JsonWriter writer) {
        throw new NotImplementedException();
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

    protected override void writeJSON(JsonWriter writer) {
        throw new NotImplementedException();
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

    protected override void writeJSON(JsonWriter writer) {
        throw new NotImplementedException();
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

    protected override void writeJSON(JsonWriter writer) {
        throw new NotImplementedException();
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

    protected override void writeJSON(JsonWriter writer) {
        throw new NotImplementedException();
    }
}
