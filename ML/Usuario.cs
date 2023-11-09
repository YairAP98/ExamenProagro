using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML
{
    public class Usuario
    {

        public List<object> Usuarios { get; set; }
        public int IdUsuario { get; set; }

        public string Contrasena { get; set; }

        public string Nombre { get; set; }

        public string Creacion { get; set; }

        public string Rfc { get; set; }
    }
}
