using System.Collections;
using System.Collections.Generic;

/*
 * Interfaz comun para los diferentes tipos de serializacion
 * Limitado a string para que los sistemas de persistencia trabajen con ello
 */
public interface ISerializer
{
    string Serialize (Event tEvent);
}
