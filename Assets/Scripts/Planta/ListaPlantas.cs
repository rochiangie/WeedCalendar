using System;
using System.Collections.Generic;

[Serializable]
public class ListaPlantas
{
    public List<PlantaData> plantas;

    public ListaPlantas(List<PlantaData> lista)
    {
        plantas = lista;
    }
}
