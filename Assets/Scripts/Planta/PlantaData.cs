using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PlantaData
{
    public string nombre;
    public string fechaSiembra;
    public string fechaCosechaEstimada;
    public List<string> notas = new List<string>();
    public List<string> rutasFotos = new List<string>();
    public string estadoActual;
    public List<EventoCalendario> eventos = new List<EventoCalendario>();

    public PlantaData(string nombre, DateTime siembra, DateTime cosechaEstimada)
    {
        this.nombre = nombre;
        this.fechaSiembra = siembra.ToString("yyyy-MM-dd");
        this.fechaCosechaEstimada = cosechaEstimada.ToString("yyyy-MM-dd");
        this.estadoActual = "Reciente";
    }

    public void AgregarNota(string nota)
    {
        notas.Add($"[{DateTime.Now:yyyy-MM-dd}] {nota}");
    }

    public void AgregarFoto(string ruta)
    {
        rutasFotos.Add(ruta);
    }

    public void AgregarEvento(string titulo, DateTime fecha)
    {
        eventos.Add(new EventoCalendario(titulo, fecha));
    }
}

[Serializable]
public class EventoCalendario
{
    public string titulo;
    public string fecha;

    public EventoCalendario(string titulo, DateTime fecha)
    {
        this.titulo = titulo;
        this.fecha = fecha.ToString("yyyy-MM-dd");
    }
}
