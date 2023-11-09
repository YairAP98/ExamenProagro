using System;
using System.Collections.Generic;

namespace DL;

public partial class Permiso
{
    public int? IdUsuario { get; set; }

    public int? IdEstado { get; set; }

    public virtual Estado? IdEstadoNavigation { get; set; }

    public virtual Usuario? IdUsuarioNavigation { get; set; }
}
