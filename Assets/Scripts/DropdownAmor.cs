using UnityEngine;
using TMPro;

public class DropdownAmor : MonoBehaviour
{
    public TMP_Dropdown dropdown;

    void Start()
    {
        // Limpiamos las opciones previas si hay
        dropdown.ClearOptions();

        // Creamos la lista con las frases
        var opciones = new System.Collections.Generic.List<string>
        {
            "Te amo",
            "Te amo mucho",
            "Te amo demasiado"
        };

        // Agregamos al Dropdown
        dropdown.AddOptions(opciones);

        // Opcional: suscribirse al evento si querés detectar cuándo cambia
        dropdown.onValueChanged.AddListener(OpcionSeleccionada);
    }

    void OpcionSeleccionada(int indice)
    {
        Debug.Log("Elegiste: " + dropdown.options[indice].text);
    }
}
