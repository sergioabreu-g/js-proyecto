using System.Collections;
using System.Collections.Generic;

/*
 * Serializador CSV
 */
public class myJsonSerializer : ISerializer
{
    public string Serialize(Event tEvent) {
        //using Json.net third party package to build the json on each event
        return tEvent.ToJSON();
    }
}
