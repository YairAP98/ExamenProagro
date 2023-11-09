using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML
{
    public class GeoReferencia
    {
        public List<object> GeoReferencias { get; set; }
        public int IdGeoreferencia { get; set; }
        public decimal Latitud { get; set; }
        public decimal Longitud { get; set; }
        //prop de navegacion 

        public ML.Estado Estado { get; set; }
    }
}
