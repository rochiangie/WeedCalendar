using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CultivoManager : MonoBehaviour
{
    public List<PlantaData> plantas = new List<PlantaData>();
    public TMP_Dropdown dropdownPlantas;
    //public GameObject panelConfirmacion; // opcional si querés pedir confirmación

    private string rutaArchivo => Path.Combine(Application.persistentDataPath, "plantas.json");

    void Start()
    {
        CargarPlantas();
        MostrarDropdown();
    }





    void CargarPlantas()
    {
        if (File.Exists(rutaArchivo))
        {
            string json = File.ReadAllText(rutaArchivo);
            plantas = JsonUtility.FromJson<ListaPlantas>(json).plantas;
            Debug.Log("✅ Plantas cargadas desde: " + rutaArchivo);
        }
        else
        {
            Debug.Log("⚠️ No se encontró archivo de plantas. Se crearán nuevas.");
            CrearPlantasDePrueba();
            GuardarPlantas();
        }
    }
    /*public void MostrarPanelConfirmacion()
    {
        if (dropdownPlantas.options.Count == 0 || dropdownPlantas.value < 0)
        {
            Debug.LogWarning("⚠️ No hay planta seleccionada para eliminar");
            return;
        }

        panelConfirmacion.SetActive(true);
    }*/
    void MostrarDropdown()
    {
        dropdownPlantas.ClearOptions();

        List<string> nombres = new List<string>();
        foreach (PlantaData planta in plantas)
            nombres.Add(planta.nombre);

        dropdownPlantas.AddOptions(nombres);

        dropdownPlantas.onValueChanged.RemoveAllListeners();
        dropdownPlantas.onValueChanged.AddListener(MostrarDetallePlanta);

        if (nombres.Count > 0)
            MostrarDetallePlanta(0);
    }

    public GameObject notasLog; // Asignalo desde el Inspector

    public void MostrarDetallePlanta(int index)
    {
        if (index >= 0 && index < plantas.Count)
        {
            PlantaData seleccionada = plantas[index];
            Debug.Log($"🌱 Planta seleccionada: {seleccionada.nombre}");

            // ✅ Activar o desactivar el log de notas según haya notas
            if (seleccionada.notas != null && seleccionada.notas.Count > 0)
                notasLog.SetActive(true);
            else
                notasLog.SetActive(false);
        }
    }

    public void ActualizarListaPlantas()
    {
        CargarPlantas();     // vuelve a cargar desde el JSON
        MostrarDropdown();   // actualiza visualmente el dropdown
        Debug.Log("🔁 Lista de plantas actualizada manualmente");
    }

    public void AgregarNuevaPlanta()
    {
        PlantaData nueva = new PlantaData("Nueva Planta", DateTime.Now, DateTime.Now.AddDays(60));
        plantas.Add(nueva);
        GuardarPlantas();
        RefrescarDropdown();
    }


    public void CargarPlantaSeleccionada()
    {
        if (dropdownPlantas.options.Count == 0 || dropdownPlantas.value < 0)
        {
            Debug.LogWarning("⚠️ No hay planta seleccionada");
            return;
        }

        string nombre = dropdownPlantas.options[dropdownPlantas.value].text;
        PlantaData plantaSeleccionada = plantas.Find(p => p.nombre == nombre);

        if (plantaSeleccionada != null)
        {
            Debug.Log($"🌱 Planta cargada: {plantaSeleccionada.nombre}");
            DatosPlantaSeleccionada.instancia.plantaSeleccionada = plantaSeleccionada;
            SceneManager.LoadScene("MenuOpciones");
        }
    }

    public void EliminarPlantaSeleccionada()
    {
        int index = dropdownPlantas.value;

        if (index < 0 || index >= plantas.Count)
        {
            Debug.LogWarning("⚠️ No hay planta válida seleccionada para eliminar.");
            return;
        }

        string nombre = plantas[index].nombre;
        Debug.Log($"🗑️ Eliminando planta: {nombre}");

        // Eliminar planta
        plantas.RemoveAt(index);

        // Guardar la lista actualizada
        GuardarPlantas();

        // Refrescar el dropdown
        RefrescarDropdown();
    }

    void RefrescarDropdown()
    {
        dropdownPlantas.ClearOptions();

        List<string> nombres = new List<string>();
        foreach (PlantaData planta in plantas)
            nombres.Add(planta.nombre);

        dropdownPlantas.AddOptions(nombres);

        Debug.Log($"🌿 Dropdown actualizado con {nombres.Count} plantas");

        dropdownPlantas.onValueChanged.RemoveAllListeners();
        dropdownPlantas.onValueChanged.AddListener(MostrarDetallePlanta);

        if (nombres.Count > 0)
        {
            dropdownPlantas.value = 0;
            dropdownPlantas.captionText.text = nombres[0];
            MostrarDetallePlanta(0);  // ✅ muestra detalles iniciales
        }
        else
        {
            dropdownPlantas.captionText.text = "Seleccionar...";
        }

        dropdownPlantas.RefreshShownValue();
    }




    void GuardarPlantas()
    {
        string json = JsonUtility.ToJson(new ListaPlantas(plantas));
        File.WriteAllText(rutaArchivo, json);
        Debug.Log("💾 Plantas guardadas en: " + rutaArchivo);
    }

    void CrearPlantasDePrueba()
    {
        plantas.Add(new PlantaData("Sour Diesel", DateTime.Now, DateTime.Now.AddDays(70)));
        plantas.Add(new PlantaData("OG Kush", DateTime.Now.AddDays(-3), DateTime.Now.AddDays(65)));
        plantas.Add(new PlantaData("Medusa", DateTime.Now.AddDays(-7), DateTime.Now.AddDays(60)));
        plantas.Add(new PlantaData("Gorilla", DateTime.Now.AddDays(-10), DateTime.Now.AddDays(55)));
        plantas.Add(new PlantaData("Girl Scout Cookies", DateTime.Now.AddDays(-1), DateTime.Now.AddDays(80)));
    }
}
