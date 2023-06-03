using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entidades;
using Datos;

namespace Negocio
{
    public class nLogin
    {
        dLogin logindato;
        public nLogin()
        {
            logindato = new dLogin();
        }
        public bool Ingresar(string usuario, string contrasenia)
        {
            eLogin login = new eLogin
            {
                Usuario = usuario,
                Contrasenia = contrasenia
            };
            return logindato.Ingresar(login);
        }
        public string RestaurarContrasenia(string usuario, string contrasenia)
        {
            eLogin login = new eLogin
            {
                Usuario = usuario,
                Contrasenia = contrasenia
            };
            return logindato.RestaurarContrasenia(login);
        }
    }
}
