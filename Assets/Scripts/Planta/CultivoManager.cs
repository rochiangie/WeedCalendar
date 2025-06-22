using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using TMPro;

public class CultivoManager : MonoBehaviour
{
    public List<PlantaData> plantas = new List<PlantaData>();
    //public GameObject tarjetaPlantaPrefab;
    //public Transform contenedorTarjetas;
    //public GameObject togglePlantaPrefab;
    public Transform contenedorToggles;
    public TMP_Dropdown dropdownPlantas;

    private string rutaArchivo => Path.Combine(Application.persistentDataPath, "plantas.json");

    void Start()
    {
        //DesactivarUIVieja();

        CargarPlantas();
        if (plantas.Count == 0)
        {
            CrearPlantasDePrueba();
            GuardarPlantas();
        }

        MostrarDropdown();
    }
    /*void DesactivarUIVieja()
    {
        GameObject canvas = GameObject.Find("Canvas");
        if (canvas != null)
        {
            foreach (Transform hijo in canvas.transform)
            {
                if (!hijo.name.Contains("TarjetaPlanta") && !hijo.name.Contains("ContenedorTarjetas"))
                {
                    hijo.gameObject.SetActive(false); // Apagamos todo lo que no es nuestro
                }
            }
        }
    }*/

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

    void MostrarDropdown()
    {
        dropdownPlantas.ClearOptions();

        List<string> nombres = new List<string>();
        foreach (PlantaData planta in plantas)
        {
            nombres.Add(planta.nombre);
        }

        dropdownPlantas.AddOptions(nombres);

        // Opcional: mostrar detalles de la primera al iniciar
        MostrarDetallePlanta(0);
    }

    public void MostrarDetallePlanta(int index)
    {
        if (index >= 0 && index < plantas.Count)
        {
            PlantaData seleccionada = plantas[index];
            Debug.Log($"Planta seleccionada: {seleccionada.nombre}");
            // Acá podrías mostrarla en pantalla, activar panel, etc.
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
