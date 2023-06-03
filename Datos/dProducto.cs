using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using Entidades;

namespace Datos
{
    public class dProducto
    {
        Database db = new Database();
        public string Insertar(eProducto o)
        {
            try
            {
                SqlConnection con = db.ConectaDb();
                string insert = "Insertar_Producto";
                SqlCommand cmd = new SqlCommand(insert, con);
                cmd.CommandType = CommandType.StoredProcedure;
                //cmd.Parameters.AddWithValue("@codigoProducto", o.CodigoProducto);
                cmd.Parameters.AddWithValue("@nombreProducto", o.NombreProducto);
                cmd.Parameters.AddWithValue("@precio", o.Precio);
                cmd.Parameters.AddWithValue("@categoria", o.Categoria);
                cmd.Parameters.AddWithValue("@cantidad", o.Cantidad);
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
        public string Modificar(eProducto o)
        {
            try
            {
                SqlConnection con = db.ConectaDb();
                string update = "Modificar_Producto";
                SqlCommand cmd = new SqlCommand(update, con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@codigoProducto", o.CodigoProducto);
                cmd.Parameters.AddWithValue("@nombreProducto", o.NombreProducto);
                cmd.Parameters.AddWithValue("@precio", o.Precio);
                cmd.Parameters.AddWithValue("@categoria", o.Categoria);
                cmd.Parameters.AddWithValue("@cantidad", o.Cantidad);
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
        public string Eliminar(int cod)
        {
            try
            {
                SqlConnection con = db.ConectaDb();
                string delete = "Eliminar_Producto";
                SqlCommand cmd = new SqlCommand(delete, con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@codigoProducto", cod);
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
                string show = "Mostrar_Productos";
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
        public List<eProducto> Productos(string categoria)//r1
        {
            try
            {
                SqlConnection con = db.ConectaDb();
                List<eProducto> lsproductos = new List<eProducto>();
                eProducto producto = null;
                string select = string.Format("select CodigoProducto from Producto where Categoria = '{0}'", categoria);
                SqlCommand cmd = new SqlCommand(select, con);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    producto = new eProducto();
                    producto.CodigoProducto = (int)reader["CodigoProducto"];
                    if (!lsproductos.Exists(X => X.CodigoProducto == producto.CodigoProducto))
                    {
                        lsproductos.Add(producto);
                    }
                }
                reader.Close();
                return lsproductos;
            }
            catch(Exception ex)
            {
                return null;
            }
            finally
            {
                db.DesconectaDb();
            }
        }
        public List<eProducto> Productos2()
        {
            try
            {
                SqlConnection con = db.ConectaDb();
                List<eProducto> lsproductos = new List<eProducto>();
                eProducto producto = null;
                string select = string.Format("select CodigoProducto from Producto");
                SqlCommand cmd = new SqlCommand(select, con);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    producto = new eProducto();
                    producto.CodigoProducto = (int)reader["CodigoProducto"];
                    if (!lsproductos.Exists(X => X.CodigoProducto == producto.CodigoProducto))
                    {
                        lsproductos.Add(producto);
                    }
                }
                reader.Close();
                return lsproductos;
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
                string select = "CantidadProducto";
                SqlCommand cmd = new SqlCommand(select, con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@codigo", cod);
                SqlDataReader reader = cmd.ExecuteReader();
                int X = 0;
                while(reader.Read())
                {
                    X =  (int)reader["Cantidad"];
                }
                return X;
            }
            catch(Exception ex)
            {
                return 0;
            }
            finally
            {
                db.DesconectaDb();
            }
        }
        public void ModificarCantidad(int cant, int cod)
        {
            try
            {
                SqlConnection con = db.ConectaDb();
                string update = "ModificarCantidad";
                SqlCommand cmd = new SqlCommand(update, con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@cantidad", cant);
                cmd.Parameters.AddWithValue("@codigo", cod);
                cmd.ExecuteNonQuery();
                cmd.Parameters.Clear();
            }
            catch(Exception ex)
            {

            }
            finally
            {
                db.DesconectaDb();
            }
        }
        public Decimal Precio(int cod)
        {
            try
            {
                SqlConnection con = db.ConectaDb();
                string select = "PrecioProducto";
                SqlCommand cmd = new SqlCommand(select, con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@codigo", cod);
                SqlDataReader reader = cmd.ExecuteReader();
                Decimal X = 0;
                while (reader.Read())
                {
                    X = (Decimal)reader["Precio"];
                }
                X= Decimal.Round(X, 2);
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
    }
}
