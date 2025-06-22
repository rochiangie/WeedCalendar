using UnityEngine;

public class DatosPlantaSeleccionada : MonoBehaviour
{
    public static DatosPlantaSeleccionada instancia;

    public PlantaData plantaSeleccionada;

    void Awake()
    {
        if (instancia == null)
        {
            instancia = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void EstablecerPlanta(PlantaData planta)
    {
        plantaSeleccionada = planta;
    }
}
