using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class eCliente
    {
        public int dni { get; set; }
        public string Nombre { get; set; }
        public int Celular { get; set; }
        public string Direccion { get; set; }

        public override string ToString()
        {
            return Convert.ToString(dni);
        }
    }
}
