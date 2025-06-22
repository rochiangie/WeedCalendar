using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TogglePlanta : MonoBehaviour
{
    public TMP_Text nombrePlantaText;
    public Toggle toggle;

    private PlantaData datos;

    public void Inicializar(PlantaData datosPlanta)
    {
        datos = datosPlanta;
        nombrePlantaText.text = datos.nombre;
    }

    public bool EstaSeleccionada()
    {
        return toggle.isOn;
    }

    public PlantaData ObtenerDatos()
    {
        return datos;
    }
}
