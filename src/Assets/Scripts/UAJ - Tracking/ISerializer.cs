using System.Collections;
using System.Collections.Generic;

/*
 * Interfaz comun para los diferentes tipos de serializacion
 */
public interface ISerializer
{
    string Serialize (Event tEvent);
}
