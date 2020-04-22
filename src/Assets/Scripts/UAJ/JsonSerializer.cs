using System.Collections;
using System.Collections.Generic;

public class JsonSerializer : ISerializer
{
    public string Serialize(TrackerEvent tEvent) {
        return tEvent.ToJSON();
    }
}
