using System.Collections;
using System.Collections.Generic;
using UnityEngine; //access unity time

using Newtonsoft.Json; //Libreria para JSON .Net
using System;
using System.Text;
using System.IO; //writing custom jsons

//tipos de eventos utilizados en el juego
public enum EventType { START, END, PHOTO, DEATH, UPGRADE, ENTERBOAT }

/*
 * Clase abstracta comun a todos los eventos, implementa logica comun (escritura comun de json, csv, etc)
 */
public abstract class Event
{
    static protected bool oneLineJSON = true;
    static public void SetOneLineJSON(bool oneLine) { oneLineJSON = oneLine; }

    protected EventType type;
    protected int timeStamp;

    //used by tracker to check if the type should be ignored
    public EventType GetEventType() {
        return type;
    }

    //used by specific events to force flush
    protected bool flush = false;
    public void SetFlush(bool flush) { this.flush = flush; }
    public bool GetFlush() { return flush; }

    //override by each event type
    protected abstract void writeJSON(JsonWriter writer);
    //write the common sections of all events
    public string ToJSON() {
        StringBuilder sb = new StringBuilder();
        StringWriter sw = new StringWriter(sb);

        using (JsonWriter writer = new JsonTextWriter(sw)) {
            writer.Formatting = Formatting.Indented;

            //write type and timestamp
            writer.WriteStartObject();
            writer.WritePropertyName("EventType");
            writer.WriteValue(type.ToString()); //to string more readable
            writer.WritePropertyName("timeStamp");
            writer.WriteValue(timeStamp);

            writeJSON(writer); //write son event properties
            writer.WriteEndObject();
        }

        //write to string the whole json + optional one line
        String s = sb.ToString();
        if (oneLineJSON) s = s.Replace("\n", " ").Replace("\r", " ");
        return s;
    }

    //override by each event type
    protected abstract void writeCSV(StringWriter writer);
    //write the common csv sections of all events
    public string ToCSV() {
        StringBuilder sb = new StringBuilder();
        StringWriter sw = new StringWriter(sb);

        //write type and timestamp
        sw.Write(type.ToString());
        sw.Write(",");
        sw.Write(timeStamp);
        sw.Write(",");

        writeCSV(sw); //write son event properties

        return sb.ToString();
    }
}

public class StartEvent: Event
{
    string id;
    DateTime date;
    public StartEvent(string ident)
    {
        type = EventType.START;
        id = ident;
        timeStamp = (int)(Time.time*1000);
        date = System.DateTime.Now;
        flush = true;
    }

    protected override void writeJSON(JsonWriter writer) {
        writer.WritePropertyName("id");
        writer.WriteValue(id);
        writer.WritePropertyName("date");
        writer.WriteValue(date);
    }
    protected override void writeCSV(StringWriter writer) {
        writer.Write(id); writer.Write(",");
        writer.Write(date);
    }
}

// Falta basura que tiene encima, mejoras desbloqueadas y peces fotografiados
//      * Los computariamos posteriormente como se indica en el documento
public class EndEvent : Event
{
    string id;
    public EndEvent(string ident)
    {
        type = EventType.END;
        id = ident;
        timeStamp = (int)(Time.time * 1000);
        flush = true;
    }

    protected override void writeJSON(JsonWriter writer) {
        writer.WritePropertyName("id");
        writer.WriteValue(id);

    }
    protected override void writeCSV(StringWriter writer) {
        writer.Write(id);
    }
}

public class PhotoEvent : Event
{
    Progress.Fish fishType;
    public PhotoEvent(Progress.Fish fType)
    {
        fishType = fType;
        type = EventType.PHOTO;
        timeStamp = (int)(Time.time * 1000);
    }

    protected override void writeJSON(JsonWriter writer) {
        writer.WritePropertyName("FishType");
        writer.WriteValue(fishType);
    }
    protected override void writeCSV(StringWriter writer) {
        writer.Write(fishType);
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
        timeStamp = (int)(Time.time * 1000);
        flush = true;
    }

    protected override void writeJSON(JsonWriter writer) {
        writer.WritePropertyName("numGarbage");
        writer.WriteValue(numGarbage);
        writer.WritePropertyName("percentUpgrade");
        writer.WriteValue(percentUpgrade);
        writer.WritePropertyName("numPhotos");
        writer.WriteValue(fishPhotos);
    }
    protected override void writeCSV(StringWriter writer) {
        writer.Write(numGarbage); writer.Write(",");
        writer.Write(percentUpgrade); writer.Write(",");
        writer.Write(fishPhotos);
    }
}

public class BuyUpgradeEvent : Event
{
    int upgradeLevel;
    Progress.UpgradeType upgradeType;

    public BuyUpgradeEvent(int upLevel, Progress.UpgradeType upType)
    {
        upgradeLevel = upLevel;
        upgradeType = upType;
        type = EventType.UPGRADE;
        timeStamp = (int)(Time.time * 1000);
    }

    protected override void writeJSON(JsonWriter writer) {
        writer.WritePropertyName("upgradeType");
        writer.WriteValue(upgradeType);
        writer.WritePropertyName("upgradeLevel");
        writer.WriteValue(upgradeLevel);
    }
    protected override void writeCSV(StringWriter writer) {
        writer.Write(upgradeType); writer.Write(",");
        writer.Write(upgradeLevel);
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
        timeStamp = (int)(Time.time * 1000);
    }

    protected override void writeJSON(JsonWriter writer) {
        writer.WritePropertyName("numGarbage");
        writer.WriteValue(numGarbage);
        writer.WritePropertyName("money");
        writer.WriteValue(money);
    }
    protected override void writeCSV(StringWriter writer) {
        writer.Write(numGarbage); writer.Write(",");
        writer.Write(money);
    }
}
