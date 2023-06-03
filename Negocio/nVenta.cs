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
    public class nVenta
    {
        dVenta ventadao;

        public nVenta()
        {
            ventadao = new dVenta();
        }
        public string AgregarVenta(int dni, int cdproducto, int cantidad, decimal precio, DateTime fecha)
        {
            eVenta venta = new eVenta()
            {
                DNIcliente = dni,
                CodigoProducto = cdproducto,
                Cantidad = cantidad,
                Precio = precio,
                Fecha = fecha
            };
            return ventadao.RegistrarVenta(venta);
        }
        public DataTable ListarVenta()
        {
            DataTable tabla = new DataTable();
            tabla = ventadao.ListarTodo();
            return tabla;
        }
        public string ModificarVenta(int codigoventa, int dni, int cdproducto, int cantidad, decimal precio, DateTime fecha)
        {
            eVenta venta = new eVenta()
            {
                CodigoVenta = codigoventa,
                DNIcliente = dni,
                CodigoProducto = cdproducto,
                Cantidad = cantidad,
                Precio = precio,
                Fecha = fecha
            };
            return ventadao.ModificarVenta(venta);
        }
        public string EliminarVenta(int codigo)
        {
            return ventadao.EliminarVenta(codigo);
        }
        public DataTable Reporte1(string mes, string producto)
        {
            return ventadao.Reporte1(mes, producto);
        }
        public DataTable Reporte2(string mes)
        {
            return ventadao.Reporte2(mes);
        }
        public string Cantidad(string mes, string producto)
        {
            return ventadao.Cantidad(mes, producto);
        }
        public int Cantidad(int cod)
        {
            return ventadao.Cantidad(cod);
        }
        public DataTable Estadistica()
        {
            return ventadao.Estadistica();
        }
    }
}
