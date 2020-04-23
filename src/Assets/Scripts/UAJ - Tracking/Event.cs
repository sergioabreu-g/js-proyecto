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
    protected int timeStamp;

    //override by each event type
    protected abstract void writeJSON(JsonWriter writer);
    public string ToJSON() {
        StringBuilder sb = new StringBuilder();
        StringWriter sw = new StringWriter(sb);

        using (JsonWriter writer = new JsonTextWriter(sw)) {
            writer.Formatting = Formatting.Indented;

            writer.WriteStartObject();
            writer.WritePropertyName("EventType");
            writer.WriteValue(type.ToString()); //to string more readable
            writer.WritePropertyName("timeStamp");
            writer.WriteValue(timeStamp);

            writeJSON(writer); //write son event properties
            writer.WriteEndObject();
        }

        return sb.ToString();
    }

    protected abstract void writeCSV(StringWriter writer);
    public string ToCSV() {
        StringBuilder sb = new StringBuilder();
        StringWriter sw = new StringWriter(sb);

        sw.Write(type.ToString());
        sw.Write(",");
        sw.Write(timeStamp);
        sw.Write(",");

        writeCSV(sw); //write son event properties

        return sb.ToString();
    }
}

public class StartEvent: Event//, ISerializable
{
    string id;
    DateTime date;
    public StartEvent(string ident)
    {
        type = EventType.START;
        id = ident;
        timeStamp = (int)(Time.time*1000);
        date = System.DateTime.Now;
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

// Aquí faltarían basura que tiene encima, mejoras desbloqueadas y
// peces fotografiados

// ¿Los peces fotografiados los guardamos solo como un int nPeces?
// ¿No queríamos saber qué peces hay fotografiados y cuales no para ver los dificiles?
// ¿O se "reconstruye" en base a eventos de fotografia?
public class EndEvent : Event
{
    string id;
    public EndEvent(string ident)
    {
        type = EventType.END;
        id = ident;
        timeStamp = (int)(Time.time * 1000);
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
