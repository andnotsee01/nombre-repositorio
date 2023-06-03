using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entidades;
using Datos;
using System.Data;

namespace Negocio
{
    public class nCliente
    {
        dCliente clientedao;

        public nCliente()
        {
            clientedao = new dCliente();
        }

        public string InsertarCliente(int dni, string nombre, int celular, string direccion)
        {
            eCliente cliente = new eCliente()
            {
                dni = dni,
                Nombre = nombre,
                Celular = celular,
                Direccion = direccion
            };
            return clientedao.Agregar(cliente);
        }

        public DataTable ListarCliente()
        {
            DataTable tabla = new DataTable();
            tabla = clientedao.ListarCliente();
            return tabla;
        }

        public string ModificarCliente(int dni, string nombre, int celular, string direccion)
        {
            eCliente cliente = new eCliente()
            {
                dni = dni,
                Nombre = nombre,
                Celular = celular,
                Direccion = direccion
            };
            return clientedao.Actualizar(cliente);
        }


        public List<eCliente> Clientes()
        {
            return clientedao.Clientes();
        }
        public DataTable Estadistica()
        {
            return clientedao.Estadistica();
        }

    }
}
