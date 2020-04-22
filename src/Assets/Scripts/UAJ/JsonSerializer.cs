using System.Collections;
using System.Collections.Generic;

using System.Text.Json;
using System.Text.Json.Serialization;

public class JsonSerializer : ISerializer
{
    public string Serialize(TrackerEvent tEvent) {
        //using csharp json serialization tool
        return JsonSerializer.Serialize(tEvent);
    }
}
