// CultivoManager.cs
using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class CultivoManager : MonoBehaviour
{
    public List<PlantaData> plantas = new List<PlantaData>();

    [Header("UI")]
    public TMP_Dropdown dropdownPlantas;

    private string rutaArchivo => Path.Combine(Application.persistentDataPath, "plantas.json");

    void Start()
    {
        // CargarPlantas(); // sigue comentado
        dropdownPlantas.ClearOptions();
        if (plantas.Count == 0)
        {
            Debug.Log("🌱 Lista vacía, se crean plantas de prueba");
            CrearPlantasDePrueba();
            GuardarPlantas();
        }

        MostrarDropdown();
    }

    public void RefrescarDropdown()
    {
        Debug.Log("🔄 Refrescando dropdown... Plantas en memoria: " + plantas.Count);

        dropdownPlantas.ClearOptions();

        List<string> nombres = new List<string>();
        foreach (PlantaData planta in plantas)
            nombres.Add(planta.nombre);

        dropdownPlantas.AddOptions(nombres);

        dropdownPlantas.value = -1;
        dropdownPlantas.captionText.text = "Seleccionar...";
        dropdownPlantas.RefreshShownValue();
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

    public void CargarPlantaSeleccionada()
    {
        int index = dropdownPlantas.value;
        if (index >= 0 && index < plantas.Count)
        {
            DatosPlantaSeleccionada.instancia.EstablecerPlanta(plantas[index]);
            SceneManager.LoadScene("MenuOpciones");
        }
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

        Debug.Log($"Dropdown cargado con {nombres.Count} opciones");

        dropdownPlantas.onValueChanged.RemoveAllListeners();
        dropdownPlantas.onValueChanged.AddListener(MostrarDetallePlanta);

        if (nombres.Count > 0)
            MostrarDetallePlanta(0); // opcional
    }




    public void MostrarDetallePlanta(int index)
    {
        if (index >= 0 && index < plantas.Count)
        {
            PlantaData seleccionada = plantas[index];
            Debug.Log($"🌱 Planta seleccionada: {seleccionada.nombre} — Siembra: {seleccionada.fechaSiembra}");
        }
    }

    public void AgregarNuevaPlanta()
    {
        PlantaData nueva = new PlantaData("Nueva Planta", DateTime.Now, DateTime.Now.AddDays(60));
        plantas.Add(nueva);
        GuardarPlantas();
        MostrarDropdown();
    }
}
