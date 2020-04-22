using System.Collections;
using System.Collections.Generic;
using System.IO;
using System;
using UnityEngine; //debugs

// Guardado de eventos en archivo fisico
// guarda los eventos en una cola y los vuelca al sistema de archivos en flush
public class FilePersistanceQueued : IPersistance
{
    //cola donde guardar los archivos
    Queue<String> eventQueue = new Queue<String>();

    ISerializer serializer = null;
    string sesionID = null;
    string filePath = null;
    public FilePersistanceQueued(ISerializer s, string id) {
        serializer = s;
        sesionID = id;

        //create file for the sesion
        createFile(sesionID);
    }

    //crea el archivo utilizando la sesion id
    private void createFile(string subpath) {
        filePath = Application.persistentDataPath + "/" + subpath;

        try {
            FileStream fileStream = new FileStream(filePath, FileMode.Create);
            fileStream.Close();
        }
        catch (Exception ex) {
            Debug.Log("Error creating file: " + ex.ToString());
        }
    }

    //////////////////////////////////////////////////////////////////////////

    public void Send(TrackerEvent tEvent) {
        eventQueue.Enqueue(serializer.Serialize(tEvent));
    }

    public void Flush() {
        try {
            using (StreamWriter writer = File.AppendText(filePath)) {
                while (eventQueue.Count != 0) writer.WriteLine(eventQueue.Dequeue());
            }
        }
        catch (Exception ex) {
            Debug.Log("Error writing to file: " + ex.ToString());
        }
    }
}
