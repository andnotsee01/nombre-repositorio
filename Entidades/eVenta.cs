using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class eVenta
    {
        public int CodigoVenta { get; set; }
        public int DNIcliente { get; set; }
        public int CodigoProducto {get; set;}
        public int Cantidad { get; set; }
        public decimal Precio { get; set; }
        public DateTime Fecha { get; set; }
        public override string ToString()
        {
            return Convert.ToString(CodigoProducto);
        }

    }
}
