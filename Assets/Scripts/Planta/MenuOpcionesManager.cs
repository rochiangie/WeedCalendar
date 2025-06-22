using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuOpcionesManager : MonoBehaviour
{
    public TextMeshProUGUI nombrePlantaText;
    public TextMeshProUGUI fechaSiembraText;
    public TextMeshProUGUI notasLogText;
    public TMP_InputField inputNota;

    private PlantaData planta;

    void Start()
    {
        if (DatosPlantaSeleccionada.instancia?.plantaSeleccionada == null)
        {
            Debug.LogWarning("⚠️ No hay planta seleccionada.");
            return;
        }

        planta = DatosPlantaSeleccionada.instancia.plantaSeleccionada;
        ActualizarUI();
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
