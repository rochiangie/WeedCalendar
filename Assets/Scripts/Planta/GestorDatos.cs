using System.Collections.Generic;
using System.IO;
using UnityEngine;

public static class GestorDatos
{
    private static string archivo = "cultivo_data.json";
    public static string RutaArchivo => Path.Combine(Application.persistentDataPath, archivo);

    [System.Serializable]
    private class ContenedorPlantas
    {
        public List<Planta> plantas = new List<Planta>();
    }

    public static void GuardarPlantas(List<Planta> plantas)
    {
        ContenedorPlantas contenedor = new ContenedorPlantas { plantas = plantas };
        string json = JsonUtility.ToJson(contenedor, true);
        File.WriteAllText(RutaArchivo, json);
        Debug.Log($"📁 Datos guardados en: {RutaArchivo}");
    }

    public static void BorrarPlantas()
    {
        if (File.Exists(RutaArchivo))
        {
            File.Delete(RutaArchivo);
            Debug.Log("🗑️ Archivo de plantas eliminado correctamente.");
        }
    }

    public static List<Planta> CargarPlantas()
    {
        if (!File.Exists(RutaArchivo))
        {
            Debug.Log("⚠️ No existe archivo de datos. Se crean plantas por defecto.");

            var plantasPorDefecto = new List<Planta>
        {
            new Planta { nombre = "Gorilla", fechaSiembra = "16/05/2025", notas = new List<string>() },
            new Planta { nombre = "Sour Diesel", fechaSiembra = "17/05/2025", notas = new List<string>() },
            new Planta { nombre = "Girl Scout", fechaSiembra = "18/05/2025", notas = new List<string>() },
            new Planta { nombre = "OG Kush", fechaSiembra = "19/05/2025", notas = new List<string>() },
            new Planta { nombre = "Medusa", fechaSiembra = "20/05/2025", notas = new List<string>() }
        };

            GuardarPlantas(plantasPorDefecto);
        }

        string json = File.ReadAllText(RutaArchivo);
        ContenedorPlantas contenedor = JsonUtility.FromJson<ContenedorPlantas>(json);
        return contenedor.plantas;
    }

}
