using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.IO;
using Newtonsoft.Json;
public class CultivoManager : MonoBehaviour
{
    public TMP_Dropdown dropdown;
    public string archivoJSON = "plantas.json"; // En StreamingAssets
    private Dictionary<string, PlantaData> plantasDict;

    void Start()
    {
        CargarPlantasDesdeJSON();
        ActualizarDropdown();
    }

    void CargarPlantasDesdeJSON()
    {
        try
        {
            string path = Path.Combine(Application.streamingAssetsPath, archivoJSON);
            if (!File.Exists(path))
            {
                Debug.LogError("No se encontró el archivo JSON en: " + path);
                return;
            }

            string json = File.ReadAllText(path);

            plantasDict = JsonConvert.DeserializeObject<Dictionary<string, PlantaData>>(json);

            if (plantasDict == null)
            {
                Debug.LogError("❌ Error al deserializar el JSON, el diccionario es null.");
                return;
            }

            Debug.Log("✅ Plantas cargadas correctamente: " + plantasDict.Count);
        }
        catch (System.Exception ex)
        {
            Debug.LogError("🔥 Error al cargar plantas: " + ex.Message);
        }
    }

    void ActualizarDropdown()
    {
        dropdown.ClearOptions();
        dropdown.AddOptions(new List<string>(plantasDict.Keys));
    }

    public void SeleccionarPlanta(int index)
    {
        string seleccionada = dropdown.options[index].text;
        if (plantasDict.TryGetValue(seleccionada, out PlantaData planta))
        {
            Debug.Log("Planta seleccionada: " + planta.nombre);
            // Aquí podés actualizar la UI con sus datos
        }
    }
}
