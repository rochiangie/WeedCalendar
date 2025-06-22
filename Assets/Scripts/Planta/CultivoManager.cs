using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using TMPro;

public class CultivoManager : MonoBehaviour
{
    public List<PlantaData> plantas = new List<PlantaData>();

    [Header("UI")]
    public TMP_Dropdown dropdownPlantas;

    private string rutaArchivo => Path.Combine(Application.persistentDataPath, "plantas.json");

    void Start()
    {
        CargarPlantas();

        if (plantas.Count == 0)
        {
            CrearPlantasDePrueba();
            GuardarPlantas();
        }

        MostrarDropdown();
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
        Debug.Log("✅ Plantas guardadas en: " + rutaArchivo);
    }

    public void CargarPlantas()
    {
        if (File.Exists(rutaArchivo))
        {
            string json = File.ReadAllText(rutaArchivo);
            plantas = JsonUtility.FromJson<ListaPlantas>(json).plantas;
            Debug.Log("✅ Plantas cargadas desde: " + rutaArchivo);
        }
        else
        {
            Debug.Log("No se encontró archivo de plantas. Se crearán nuevas.");
        }
    }

    void MostrarDropdown()
    {
        dropdownPlantas.ClearOptions();

        List<string> nombres = new List<string>();
        foreach (PlantaData planta in plantas)
        {
            nombres.Add(planta.nombre);
        }

        dropdownPlantas.AddOptions(nombres);
        dropdownPlantas.onValueChanged.RemoveAllListeners();
        dropdownPlantas.onValueChanged.AddListener(MostrarDetallePlanta);

        MostrarDetallePlanta(0); // Mostrar la primera al iniciar
    }

    public void MostrarDetallePlanta(int index)
    {
        if (index >= 0 && index < plantas.Count)
        {
            PlantaData seleccionada = plantas[index];
            Debug.Log($"🌱 Planta seleccionada: {seleccionada.nombre} — Siembra: {seleccionada.fechaSiembra}");
            // A futuro: actualizar panel con detalles
        }
    }

    public void AgregarNuevaPlanta()
    {
        Debug.Log("🧪 BOTÓN FUNCIONA");

        PlantaData nueva = new PlantaData("Prueba Botón", DateTime.Now, DateTime.Now.AddDays(50));
        plantas.Add(nueva);
        GuardarPlantas();
        MostrarDropdown();
    }

}
