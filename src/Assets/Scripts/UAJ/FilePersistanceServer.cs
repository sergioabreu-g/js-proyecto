using System.Collections;
using System.Collections.Generic;
using System.IO;
using System;
using UnityEngine; //debugs

//fake server api mockup class
public static class ServerHandler {
    public static int getIP(string id) { return -1; }
    public static void sendtoIP(int ip, string data) { Debug.Log("Sending data"); }
}

// Guardado de eventos y los envia a un servidor cada cierto tiempo
public class FilePersistanceServer : IPersistance
{
    //cola donde guardar los archivos
    Queue<String> eventQueue = new Queue<String>();

    ISerializer serializer = null;
    string sesionID = null;
    int serverIP = -0;
    public FilePersistanceServer(ISerializer s, string id) {
        serializer = s;
        sesionID = id;

        //create file for the sesion
        connectServer(sesionID);
    }

    //conecta con el servido enviando la sesion id
    private void connectServer(string subpath) {
        try {
            serverIP = ServerHandler.getIP(sesionID);
        }
        catch (Exception ex) {
            Debug.Log("Error connecting to server: " + ex.ToString());
        }
    }

    //////////////////////////////////////////////////////////////////////////

    public void Send(TrackerEvent tEvent) {
        eventQueue.Enqueue(serializer.Serialize(tEvent));
    }

    public void Flush() {
        try {
            //se podria controlar que cada llamada haya retrasmitido la cantidad de bytes adecuada etc
            while (eventQueue.Count != 0) ServerHandler.sendtoIP(serverIP, eventQueue.Dequeue());
        }
        catch (Exception ex) {
            Debug.Log("Error sending to server: " + ex.ToString());
        }
    }
}
