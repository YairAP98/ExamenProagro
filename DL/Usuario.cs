using System;
using System.Collections.Generic;

namespace DL;

public partial class Usuario
{
    public int IdUsuario { get; set; }

    public string? Contrasena { get; set; }

    public string? Nombre { get; set; }

    public DateTime? Creacion { get; set; }

    public string? Rfc { get; set; }
}
