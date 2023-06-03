using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using Entidades;
using Datos;

namespace Negocio
{
    public class nProducto
    {
        dProducto productoDatos;
        public nProducto()
        {
            productoDatos = new dProducto();
        }
        public string RegistrarProducto(string nombre, Decimal precio, string categoria, int cantidad)
        {
            eProducto producto = new eProducto()
            {
                NombreProducto = nombre,
                Precio = precio,
                Categoria = categoria,
                Cantidad = cantidad
            };
            return productoDatos.Insertar(producto);
        }
        public string ModificarProducto(int codigo, string nombre, Decimal precio, string categoria, int cantidad)
        {
            eProducto producto = new eProducto()
            {
                CodigoProducto = codigo,
                NombreProducto = nombre,
                Precio = precio,
                Categoria = categoria,
                Cantidad = cantidad
            };
            return productoDatos.Modificar(producto);
        }
        public string EliminarProducto(int codigo)
        {
            return productoDatos.Eliminar(codigo);
        }
        public DataTable ListarProductos()
        {
            return productoDatos.ListarTodo();
        }
        public List<eProducto> Productos(string categoria)
        {
            return productoDatos.Productos(categoria);
        }
        public List<eProducto> Productos2()
        {
            return productoDatos.Productos2();
        }
        public int Cantidad(int cod)
        {
            return productoDatos.Cantidad(cod);
        }
        public void ModificarCantidad(int cant, int cod)
        {
            productoDatos.ModificarCantidad(cant, cod);
        }
        public Decimal Precio(int cod)
        {
            return productoDatos.Precio(cod);
        }
    }
}
