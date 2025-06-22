using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;
using System.Numerics;

public class UISelector : MonoBehaviour
{
    [Header("Referencias UI")]
    public TMP_Dropdown dropdownPlantas;
    public Button btnCargarPlanta;
    public Button btnNuevaPlanta;
    public TMP_InputField inputNuevaPlanta;
    public Button btnConfirmarNueva;

    private List<Planta> plantas = new List<Planta>();
    private Planta plantaSeleccionada;

    void Start()
    {
        // Ocultar elementos de nueva planta
        inputNuevaPlanta.gameObject.SetActive(false);
        btnConfirmarNueva.gameObject.SetActive(false);

        // Cargar datos y actualizar UI
        plantas = GestorDatos.CargarPlantas();
        ActualizarDropdown();

        // Eventos
        btnNuevaPlanta.onClick.AddListener(MostrarInputNuevaPlanta);
        btnConfirmarNueva.onClick.AddListener(ConfirmarNuevaPlanta);
        btnCargarPlanta.onClick.AddListener(CargarPlantaSeleccionada);
    }

    void ActualizarDropdown()
    {
        dropdownPlantas.ClearOptions();

        List<string> nombres = new List<string>();
        foreach (var planta in plantas)
        {
            nombres.Add(planta.nombre);
        }

        if (nombres.Count == 0)
        {
            nombres.Add("Sin plantas");
            dropdownPlantas.interactable = false;
            btnCargarPlanta.interactable = false;
        }
        else
        {
            dropdownPlantas.interactable = true;
            btnCargarPlanta.interactable = true;
        }

        dropdownPlantas.AddOptions(nombres);
    }

    void MostrarInputNuevaPlanta()
    {
        inputNuevaPlanta.text = "";
        inputNuevaPlanta.gameObject.SetActive(true);
        btnConfirmarNueva.gameObject.SetActive(true);
    }

    void ConfirmarNuevaPlanta()
    {
        string nuevoNombre = inputNuevaPlanta.text.Trim();
        if (string.IsNullOrEmpty(nuevoNombre))
        {
            Debug.LogWarning("⚠️ Nombre inválido");
            return;
        }

        foreach (var p in plantas)
        {
            if (p.nombre == nuevoNombre)
            {
                Debug.LogWarning("❌ Ya existe una planta con ese nombre");
                return;
            }
        }

        Planta nueva = new Planta
        {
            nombre = nuevoNombre,
            estado = "En crecimiento",
            siembra = System.DateTime.Now.ToString("dd/MM/yyyy"),
            notas = new List<Nota>()
        };

        plantas.Add(nueva);
        GestorDatos.GuardarPlantas(plantas);
        ActualizarDropdown();

        inputNuevaPlanta.gameObject.SetActive(false);
        btnConfirmarNueva.gameObject.SetActive(false);

        // Seleccionarla en el dropdown
        int index = dropdownPlantas.options.FindIndex(o => o.text == nuevoNombre);
        if (index != -1) dropdownPlantas.value = index;
    }

    void CargarPlantaSeleccionada()
    {
        if (dropdownPlantas.options.Count == 0 || dropdownPlantas.value < 0)
        {
            Debug.LogWarning("⚠️ No hay planta seleccionada");
            return;
        }

        string nombre = dropdownPlantas.options[dropdownPlantas.value].text;
        plantaSeleccionada = plantas.Find(p => p.nombre == nombre);

        if (plantaSeleccionada != null)
        {
            Debug.Log($"🌱 Planta cargada: {plantaSeleccionada.nombre}");
            PlayerPrefs.SetString("PlantaActiva", plantaSeleccionada.nombre);
            // TODO: Activar el siguiente panel o escena
        }
    }
}
