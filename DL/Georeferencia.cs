using System;
using System.Collections.Generic;

namespace DL;

public partial class Georeferencia
{
    public int IdGeoreferencia { get; set; }

    public int? IdEstado { get; set; }

    public decimal? Latitud { get; set; }

    public decimal? Longitud { get; set; }

    public virtual Estado? IdEstadoNavigation { get; set; }
}
