using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using Entidades;


namespace Datos
{
    public class dVenta
    {
        Database db = new Database();
        SqlConnection con = new SqlConnection();
        public string RegistrarVenta(eVenta o)
        {
            try
            {
                SqlConnection con = db.ConectaDb();
                string insert = "AgregarVenta";
                SqlCommand cmd = new SqlCommand(insert, con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@DNICliente", o.DNIcliente);
                cmd.Parameters.AddWithValue("@CodigoProducto", o.CodigoProducto);
                cmd.Parameters.AddWithValue("@cantidad", o.Cantidad);
                cmd.Parameters.AddWithValue("@precio", o.Precio);
                cmd.Parameters.AddWithValue("@fecha", o.Fecha);
                cmd.ExecuteNonQuery();
                cmd.Parameters.Clear();
                return "Insertado";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
            finally
            {
                db.DesconectaDb();
            }
        }
        public string ModificarVenta(eVenta o)
        {
            try
            {
                SqlConnection con = db.ConectaDb();
                string update = "ModificarVenta";
                SqlCommand cmd = new SqlCommand(update, con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@CodigoVenta", o.CodigoVenta);
                cmd.Parameters.AddWithValue("@DNICliente", o.DNIcliente);
                cmd.Parameters.AddWithValue("@CodigoProducto", o.CodigoProducto);
                cmd.Parameters.AddWithValue("@cantidad", o.Cantidad);
                cmd.Parameters.AddWithValue("@precio", o.Precio);
                cmd.Parameters.AddWithValue("@fecha", o.Fecha);
                cmd.ExecuteNonQuery();
                cmd.Parameters.Clear();
                return "Modificado";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
            finally
            {
                db.DesconectaDb();
            }
        }
        public string EliminarVenta(int cod)
        {
            try
            {
                SqlConnection con = db.ConectaDb();
                string delete = "EliminarVenta";
                SqlCommand cmd = new SqlCommand(delete, con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@codigoVenta", cod);
                cmd.ExecuteNonQuery();
                return "Eliminado";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
            finally
            {
                db.DesconectaDb();
            }
        }
        public DataTable ListarTodo()
        {
            DataTable tabla = new DataTable();
            try
            {
                SqlConnection con = db.ConectaDb();
                string show = "ListarVenta";
                SqlCommand cmd = new SqlCommand(show, con);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataReader reader = cmd.ExecuteReader();
                tabla.Load(reader);
            }
            catch (Exception ex)
            {
                return null;
            }
            finally
            {
                db.DesconectaDb();
            }
            return tabla;
        }
        public DataTable Reporte1(string mes, string producto)
        {
            DataTable tabla = new DataTable();
            try
            {
                SqlConnection con = db.ConectaDb();
                string show = "ListarReporte1";
                SqlCommand cmd = new SqlCommand(show, con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@mes", mes);
                cmd.Parameters.AddWithValue("@producto", producto);
                SqlDataReader reader = cmd.ExecuteReader();
                tabla.Load(reader);
            }
            catch(Exception ex)
            {
                return null;
            }
            finally
            {
                db.DesconectaDb();
            }
            return tabla;
        }
        public DataTable Reporte2(string mes)
        {
            DataTable tabla = new DataTable();
            try
            {
                SqlConnection con = db.ConectaDb();
                string show = "Reporte2";
                SqlCommand cmd = new SqlCommand(show, con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@mes", mes);
                SqlDataReader reader = cmd.ExecuteReader();
                tabla.Load(reader);
            }
            catch (Exception ex)
            {
                return null;
            }
            finally
            {
                db.DesconectaDb();
            }
            return tabla;
        }
        public string Cantidad(string mes, string producto)
        {
            try
            {
                SqlConnection con = db.ConectaDb();
                string show = "Cantidad";
                SqlCommand cmd = new SqlCommand(show, con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@mes", mes);
                cmd.Parameters.AddWithValue("@producto", producto);
                SqlDataReader reader = cmd.ExecuteReader();
                int X = 0;
                while (reader.Read())
                {
                    X = (int)reader[""];
                }
                return Convert.ToString(X);
                reader.Close();
            }
            catch (Exception ex)
            {
                return null;
            }
            finally
            {
                db.DesconectaDb();
            }
        }
        public int Cantidad(int cod)
        {
            try
            {
                SqlConnection con = db.ConectaDb();
                string select = "CantidadProductoVenta";
                SqlCommand cmd = new SqlCommand(select, con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@codigoVenta", cod);
                SqlDataReader reader = cmd.ExecuteReader();
                int X = 0;
                while (reader.Read())
                {
                    X = (int)reader["Cantidad"];
                }
                return X;
            }
            catch (Exception ex)
            {
                return 0;
            }
            finally
            {
                db.DesconectaDb();
            }
        }
        public DataTable Estadistica()
        {
            DataTable tabla = new DataTable();
            try
            {
                SqlConnection con = db.ConectaDb();
                string grafico = "GraficoEstadistico";
                SqlCommand cmd = new SqlCommand(grafico, con);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataReader reader = cmd.ExecuteReader();
                tabla.Load(reader);
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                db.DesconectaDb();
            }
            return tabla;
        }

    }
}
