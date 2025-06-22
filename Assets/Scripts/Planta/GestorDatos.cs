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

    public static List<Planta> CargarPlantas()
    {
        if (!File.Exists(RutaArchivo))
        {
            Debug.Log("⚠️ No existe archivo de datos. Se crea uno nuevo vacío.");
            GuardarPlantas(new List<Planta>());
        }

        string json = File.ReadAllText(RutaArchivo);
        ContenedorPlantas contenedor = JsonUtility.FromJson<ContenedorPlantas>(json);
        return contenedor.plantas;
    }
}
