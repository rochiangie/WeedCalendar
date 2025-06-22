using TMPro;
using UnityEngine;

public class TarjetaPlanta : MonoBehaviour
{
    public TMP_Text nombrePlantaTexto;

    private PlantaData datos;

    public void Inicializar(PlantaData datosPlanta)
    {
        datos = datosPlanta;
        nombrePlantaTexto.text = datos.nombre;
    }
}
