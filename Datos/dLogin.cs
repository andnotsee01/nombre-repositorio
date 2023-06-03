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
    public class dLogin
    {
        Database db = new Database();
        public bool Ingresar(eLogin o)
        {
            try
            {
                SqlConnection con = db.ConectaDb();
                string login = "Ingresar";
                SqlCommand cmd = new SqlCommand(login, con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@usuario", o.Usuario);
                cmd.Parameters.AddWithValue("@contraseña", o.Contrasenia);
                SqlDataReader reader = cmd.ExecuteReader();
                cmd.Parameters.Clear();
                return reader.HasRows;
            }
            catch (Exception ex)
            {
                return false;
            }
            finally
            {
                db.DesconectaDb();
            }
        }
        public string RestaurarContrasenia(eLogin o)
        {
            try
            {
                SqlConnection con = db.ConectaDb();
                string update = "Restaurar_Contraseña";
                SqlCommand cmd = new SqlCommand(update, con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@usuario", o.Usuario);
                cmd.Parameters.AddWithValue("@contraseña", o.Contrasenia);
                cmd.ExecuteNonQuery();
                cmd.Parameters.Clear();
                return "Actualizado";
            }
            catch (Exception)
            {
                return "Este usuario no esta registrado";
            }
            finally
            {

            }
        }
    }
}
