using System.Collections;
using System.Collections.Generic;

public class TrackerEvent {
    public string ToJSON() { return "jsonlol"; }
};

/*
 * Interfaz comun para los diferentes tipos de serializacion
 */
public interface ISerializer
{
    string Serialize (TrackerEvent tEvent);
}
