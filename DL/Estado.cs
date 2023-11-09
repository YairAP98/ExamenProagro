using System;
using System.Collections.Generic;

namespace DL;

public partial class Estado
{
    public int IdEstado { get; set; }

    public string? Nombre { get; set; }

    public string? Siglas { get; set; }

    public virtual ICollection<Georeferencia> Georeferencia { get; set; } = new List<Georeferencia>();
}
