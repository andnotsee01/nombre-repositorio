using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Entidades;
using Negocio;

namespace Presentacion
{
    public partial class frmLogin : Form
    {
        nLogin gl = new nLogin();
        public frmLogin()
        {
            InitializeComponent();
            label3.Visible = false;
            label4.Visible = false;
            label5.Visible = false;
            label6.Visible = false;
            label7.Visible = false;
            txtUsuarioIdent.Visible = false;
            txtContraseña1.Visible = false;
            txtContraseña2.Visible = false;
            btnGuardar.Visible = false;
            pictureBox2.Visible = false;
            btnRegreso.Visible = false;
        }
        private void btnIngresar_Click(object sender, EventArgs e)
        {
            if (txtUsuario.Text != "" && txtContra.Text != "")
            {
                if (gl.Ingresar(txtUsuario.Text, txtContra.Text) == true)
                {
                    Form1 form = new Form1();
                    form.Show();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("Usuario y/o contraseña incorrecta");
                    txtUsuario.Clear();
                    txtContra.Clear();
                }
            }
            else
            {
                MessageBox.Show("Rellene su usuario y contraseña");
            }
        }

        private void btnRestaurarContra_Click(object sender, EventArgs e)
        {
            label1.Visible = false;
            label2.Visible = false;
            txtContra.Visible = false;
            txtUsuario.Visible = false;
            btnRestaurarContra.Visible = false;
            pictureBox1.Visible = false;
            btnIngresar.Visible = false;
            label3.Visible = true;
            label4.Visible = true;
            label5.Visible = true;
            label6.Visible = true;
            label7.Visible = true;
            txtUsuarioIdent.Visible = true;
            txtContraseña1.Visible = true;
            txtContraseña2.Visible = true;
            btnGuardar.Visible = true;
            pictureBox2.Visible = true;
            btnRegreso.Visible = true;
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (txtUsuarioIdent.Text != "" && txtContraseña1.Text != "" && txtContraseña2.Text != "")
            {
                if (txtContraseña1.Text == txtContraseña2.Text)
                {
                    MessageBox.Show(gl.RestaurarContrasenia(txtUsuarioIdent.Text, txtContraseña1.Text));
                }
                else
                {
                    MessageBox.Show("Las contraseñas no coinciden");
                }
            }
            else
            {
                MessageBox.Show("Rellene su usuario y contraseña");
            }
            txtUsuarioIdent.Clear();
            txtContraseña1.Clear();
            txtContraseña2.Clear();
        }

        private void btnRegreso_Click(object sender, EventArgs e)
        {
            label1.Visible = true;
            label2.Visible = true;
            txtContra.Visible = true;
            txtUsuario.Visible = true;
            btnRestaurarContra.Visible = true;
            pictureBox1.Visible = true;
            btnIngresar.Visible = true;
            label3.Visible = false;
            label4.Visible = false;
            label5.Visible = false;
            label6.Visible = false;
            label7.Visible = false;
            txtUsuarioIdent.Visible = false;
            txtContraseña1.Visible = false;
            txtContraseña2.Visible = false;
            btnGuardar.Visible = false;
            pictureBox2.Visible = false;
            btnRegreso.Visible = false;
        }
    }
}
