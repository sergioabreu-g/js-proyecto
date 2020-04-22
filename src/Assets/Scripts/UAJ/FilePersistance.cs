using System.Collections;
using System.Collections.Generic;
using System.IO;
using System;
using UnityEngine; //debugs

// Guardado de eventos en archivo fisico
// send guarda el evento directamente
public class FilePersistance : IPersistance
{
    ISerializer serializer = null;
    string sesionID = null;
    string filePath = null;
    public FilePersistance(ISerializer s, string id) {
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
        try {
            using (StreamWriter writer = File.AppendText(filePath)) {
                writer.WriteLine(serializer.Serialize(tEvent));
            }
        }
        catch (Exception ex) {
            Debug.Log("Error writing to file: " + ex.ToString());
        }
    }

    public void Flush() {

    }
}
