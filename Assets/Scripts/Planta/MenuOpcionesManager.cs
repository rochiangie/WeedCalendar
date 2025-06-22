using System.Collections.Generic;
using System.Security.Cryptography;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuOpcionesManager : MonoBehaviour
{
    public TextMeshProUGUI nombrePlantaText;
    public TextMeshProUGUI fechaSiembraText;
    public TextMeshProUGUI notasLogText;
    public TMP_InputField inputNota;
    public TMP_Dropdown dropdownPlantas;

    private PlantaData planta;

    void Start()
    {
        dropdownPlantas.onValueChanged.AddListener(LogSeleccion);

        if (DatosPlantaSeleccionada.instancia?.plantaSeleccionada == null)
        {
            Debug.LogWarning("⚠️ No hay planta seleccionada.");
            return;
        }

        planta = DatosPlantaSeleccionada.instancia.plantaSeleccionada;
        ActualizarUI();
    }
    void LogSeleccion(int index)
    {
        if (dropdownPlantas.options.Count > index)
        {
            Debug.Log("Seleccionado: " + dropdownPlantas.options[index].text);
        }
        else
        {
            Debug.Log("Índice inválido en dropdown: " + index);
        }
    }
    void ActualizarUI()
    {
        if (nombrePlantaText != null)
            nombrePlantaText.text = planta.nombre;

        if (fechaSiembraText != null)
            fechaSiembraText.text = planta.fechaSiembra;

        ActualizarNotasLog();
    }

    void ActualizarNotasLog()
    {
        if (notasLogText != null)
        {
            notasLogText.text = string.Join("\n• ", planta.notas);
        }
    }

    public void AgregarNota()
    {
        string nuevaNota = inputNota.text.Trim();
        if (!string.IsNullOrEmpty(nuevaNota))
        {
            planta.notas.Add(nuevaNota);
            inputNota.text = "";
            ActualizarNotasLog();
        }
    }

    public void EliminarPlanta()
    {
        if (DatosPlantaSeleccionada.instancia.plantaSeleccionada != null)
        {
            // Si en el futuro agregás listaPlantas, podés removerla ahí
            DatosPlantaSeleccionada.instancia.plantaSeleccionada = null;
            SceneManager.LoadScene("CultivoScene");
        }
    }

    public void VolverAlMenuPrincipal()
    {
        SceneManager.LoadScene("CultivoScene");
    }
}
