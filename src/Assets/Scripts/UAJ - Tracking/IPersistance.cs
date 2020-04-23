using System.Collections;
using System.Collections.Generic;

/*
 * Interfaz comun para los diferentes tipos de persistencia
 * - En principio "Send" obtiene el evento serializado y lo encola
 * - "Flush" se encarga de pasar al sistema fisico todos los eventos encolados
 */
public interface IPersistance
{
    void Send(Event tEvent);
    void Flush();
}
