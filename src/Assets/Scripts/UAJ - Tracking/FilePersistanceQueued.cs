using System.Collections;
using System.Collections.Generic;
using System.IO;
using System;
using UnityEngine; //debugs

/*
 * Guardado de eventos en archivo fisico
 * "Send" encola los eventos y "flush" los vuelca al sistema fisico
 */
public class FilePersistanceQueued : IPersistance
{
    //cola donde guardar los archivos
    Queue<String> eventQueue = new Queue<String>();

    //Referencia al serializador a utilizar
    ISerializer serializer = null;
    //Strings fijos de la sesion
    string sesionID = null, filePath = null;

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
            Debug.Log("Creating file: " + filePath);
            FileStream fileStream = new FileStream(filePath, FileMode.Create);
            fileStream.Close();
        }
        catch (Exception ex) {
            Debug.Log("Error creating file: " + ex.ToString());
        }
    }

    //////////////////////////////////////////////////////////////////////////

    //encola evento
    public void Send(Event tEvent) {
        eventQueue.Enqueue(serializer.Serialize(tEvent));
    }

    //desencola y escribe todos los eventos
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
