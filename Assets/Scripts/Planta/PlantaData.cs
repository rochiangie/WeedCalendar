using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PlantaData
{
    public string nombre;
    public string tipo;
    public string thc;
    public string cbd;
    public string genetica;
    public string ciclo_luz;
    public string cultivo;
    public string altura;
    public string estado;
    public string siembra;
    public string cosecha_estimada;
    public string cosecha_real;
    public string problemas_cosecha;
    public List<Nota> notas = new List<Nota>();
}

[Serializable]
public class Nota
{
    public string fecha;
    public string nota;
    public string color;
}
