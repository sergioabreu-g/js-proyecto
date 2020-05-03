using System.Collections;
using System.Collections.Generic;
using System.IO;
using System;
using UnityEngine; //debugs

//FAKE: server api mockup class
public static class ServerHandler {
    public static int getIP(string id) { return -1; }
    public static void sendtoIP(int ip, string data) { Debug.Log("Sending data to FAKE server"); }
}

/*
 * Envio de eventos a servidor (falso)
 * "Send" encola los eventos y "flush" envia todos los encolados
 */
// Guardado de eventos y los envia a un servidor cada cierto tiempo
public class FilePersistanceServer : IPersistance
{
    //cola donde guardar los archivos
    Queue<String> eventQueue = new Queue<String>();

    //Referencia al serializador a utilizar
    ISerializer serializer = null;
    //Strings fijos de la sesion
    string sesionID = null;
    //fake ip del servidor
    int serverIP = -0;

    public FilePersistanceServer(ISerializer s, string id) {
        serializer = s;
        sesionID = id;

        //connect to server for the sesion
        connectServer(sesionID);
    }

    //FAKE: conecta con el servido enviando la sesion id
    private void connectServer(string subpath) {
        try {
            serverIP = ServerHandler.getIP(sesionID);
        }
        catch (Exception ex) {
            Debug.Log("Error connecting to server: " + ex.ToString());
        }
    }

    //////////////////////////////////////////////////////////////////////////

    //encola evento
    public void Send(Event tEvent) {
        eventQueue.Enqueue(serializer.Serialize(tEvent));
    }

    //desencola y envia todos los eventos
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
