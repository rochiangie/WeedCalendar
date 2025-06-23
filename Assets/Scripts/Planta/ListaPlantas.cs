using System;
using System.Collections.Generic;

[Serializable]
public class ListaPlantas
{
    public List<Planta> plantas;

    public ListaPlantas(List<Planta> plantas)
    {
        this.plantas = plantas;
    }
}
