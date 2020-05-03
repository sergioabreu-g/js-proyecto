using System.Collections;
using System.Collections.Generic;

/*
 * Serializador CSV
 */
public class myCSVSerializer : ISerializer
{
    public string Serialize(Event tEvent) {
        //simply building the csv writing values and commas
        return tEvent.ToCSV();
    }
}
