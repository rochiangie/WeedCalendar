using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class CultivoManager : MonoBehaviour
{
    public List<PlantaData> plantas = new List<PlantaData>();
    public GameObject tarjetaPlantaPrefab;
    public Transform contenedorTarjetas;

    private string rutaArchivo => Path.Combine(Application.persistentDataPath, "plantas.json");

    void Start()
    {
        CargarPlantas();
        if (plantas.Count == 0)
        {
            CrearPlantasDePrueba();
            GuardarPlantas();
        }

        MostrarTarjetas();
    }


    void CrearPlantasDePrueba()
    {
        plantas.Add(new PlantaData("Sour Diesel", DateTime.Now, DateTime.Now.AddDays(70)));
        plantas.Add(new PlantaData("OG Kush", DateTime.Now.AddDays(-3), DateTime.Now.AddDays(65)));
        plantas.Add(new PlantaData("Medusa", DateTime.Now.AddDays(-7), DateTime.Now.AddDays(60)));
        plantas.Add(new PlantaData("Gorilla", DateTime.Now.AddDays(-10), DateTime.Now.AddDays(55)));
        plantas.Add(new PlantaData("Girl Scout Cookies", DateTime.Now.AddDays(-1), DateTime.Now.AddDays(80)));
    }

    public void GuardarPlantas()
    {
        string json = JsonUtility.ToJson(new ListaPlantas(plantas));
        File.WriteAllText(rutaArchivo, json);
        Debug.Log("Guardado en: " + rutaArchivo);
    }

    void MostrarTarjetas()
    {
        foreach (PlantaData planta in plantas)
        {
            GameObject tarjetaGO = Instantiate(tarjetaPlantaPrefab, contenedorTarjetas);
            TarjetaPlanta tarjetaScript = tarjetaGO.GetComponent<TarjetaPlanta>();
            tarjetaScript.Inicializar(planta);
        }
    }

    public void CargarPlantas()
    {
        if (File.Exists(rutaArchivo))
        {
            string json = File.ReadAllText(rutaArchivo);
            plantas = JsonUtility.FromJson<ListaPlantas>(json).plantas;
            Debug.Log("Cargado desde: " + rutaArchivo);
        }
        else
        {
            Debug.Log("No se encontró archivo, se va a crear uno nuevo.");
        }
    }
}
