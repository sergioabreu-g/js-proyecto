using System.Collections;
using System.Collections.Generic;

using System.Xml;
using System.Xml.Serialization;

//public class XmlSerializer : ISerializer
//{
//    int maxSize = 100;
//    XmlSerializer serializer = null;
//    public BinarySerializer() {
//        serializer = new XmlSerializer(typeof(TrackerEvent));
//    }

//    public string Serialize(Event tEvent) {
//        //using csharp xml serialization tool (events have required methods)
//        MemoryStream stream = new MemoryStream(maxSize);
//        serializer.Serialize(stream, po);

//        //read string from stream and close them
//        StreamReader reader = new StreamReader(stream);
//        stream.Close();
//        string s = reader.ReadToEnd();
//        reader.Close();

//        return s;
//    }
//}
