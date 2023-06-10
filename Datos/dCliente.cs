using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using Entidades;


namespace Datos
{
    public class dCliente
    {
        Database db = new Database();

        //Agregrar a DB
        public string Agregar(eCliente o)
        {
            try
            {
                SqlConnection con = db.ConectaDb();
                string agregar = "AgregarCliente";
                SqlCommand cmd = new SqlCommand(agregar, con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@DNI", o.dni);
                cmd.Parameters.AddWithValue("@Nombre", o.Nombre);
                cmd.Parameters.AddWithValue("@celular", o.Celular);
                cmd.Parameters.AddWithValue("@direccion", o.Direccion);

                cmd.ExecuteNonQuery();
                cmd.Parameters.Clear();
                return "Inserto";
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

        public string Actualizar(eCliente o)
        {
            try
            {
                SqlConnection con = db.ConectaDb();
                string update = "ModificarCliente";
                SqlCommand cmd = new SqlCommand(update, con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@DNI", o.dni);
                cmd.Parameters.AddWithValue("@Nombre", o.Nombre);
                cmd.Parameters.AddWithValue("@celular", o.Celular);
                cmd.Parameters.AddWithValue("@direccion", o.Direccion);

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

        public DataTable ListarCliente()
        {
            DataTable tabla = new DataTable();
            try
            {
                SqlConnection con = db.ConectaDb();
                string mostrar = "MostrarCliente";
                SqlCommand cmd = new SqlCommand(mostrar, con);
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


        public List<eCliente> Clientes()
        {
            try
            {
                SqlConnection con = db.ConectaDb();
                List<eCliente> lsClients = new List<eCliente>();
                eCliente cliente = null;
                string select = string.Format("select DNI from Cliente");
                SqlCommand cmd = new SqlCommand(select, con);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    cliente = new eCliente();
                    cliente.dni = (int)reader["DNI"];
                    if (!lsClients.Exists(X => X.dni == cliente.dni))
                    {
                        lsClients.Add(cliente);
                    }
                }
                reader.Close(); 
                return lsClients;
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
        public DataTable Estadistica()
        {
            DataTable tabla = new DataTable();
            try
            {
                SqlConnection con = db.ConectaDb();
                string grafico = "GraficoEstadistico2";
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
