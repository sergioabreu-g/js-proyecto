using System.Collections;
using System.Collections.Generic;
using System.IO;
using System;
using UnityEngine; //debugs

/*
 * Guardado de eventos en archivo fisico
 * "Send" directamente escribe en el sistema fisico y "flush" en este caso no hace nada
 * No es muy recomendable, pero empezamos por aqui
 */
public class FilePersistance : IPersistance
{
    //Referencia al serializador a utilizar
    ISerializer serializer = null;
    //Strings fijos de la sesion
    string sesionID = null, filePath = null;

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
            Debug.Log("Creating file: " + filePath);
            FileStream fileStream = new FileStream(filePath, FileMode.Create);
            fileStream.Close();
        }
        catch (Exception ex) {
            Debug.Log("Error creating file: " + ex.ToString());
        }
    }

    //////////////////////////////////////////////////////////////////////////

    //directamente escribe el evento
    public void Send(Event tEvent) {
        try {
            using (StreamWriter writer = File.AppendText(filePath)) {
                writer.WriteLine(serializer.Serialize(tEvent));
            }
        }
        catch (Exception ex) {
            Debug.Log("Error writing to file: " + ex.ToString());
        }
    }

    //no hace nada en este serializer
    public void Flush() {

    }
}
