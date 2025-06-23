using System;
using System.Collections.Generic;

[Serializable]
public class Planta
{
    public string nombre;
    public string fechaSiembra;
    public string estadoActual;
    public List<string> notas = new List<string>();
    public List<string> rutasFotos = new List<string>();
    public List<string> eventos = new List<string>();
}
