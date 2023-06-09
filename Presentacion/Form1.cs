using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Entidades;
using Negocio;

namespace Presentacion
{
    public partial class Form1 : Form
    {
        nCliente ncliente = new nCliente();
        nProducto gp = new nProducto();
        nVenta nventa = new nVenta();
        DataTable dt = new DataTable();
        int dni;
        int codProducto = -1;
        int codVenta;
        int contD = 0;
        int contC = 0;
        public Form1()
        {
            InitializeComponent();
            MostrarCliente();
            MostrarProductos();
            MostrarVenta();
            cmbCodeProdVenta.Items.Clear();
            foreach (var item in gp.Productos2())
            {
                cmbCodeProdVenta.Items.Add(item);
            }
            foreach (var item in ncliente.Clientes())
            {
                cmbDNIVenta.Items.Add(item);
            }

            dataGridView4.DataSource = nventa.Reporte1("0", "0");
            dataGridView5.DataSource = nventa.Reporte2("0");
            chart1.Visible = false;
            chart2.Visible = false;
            label21.Visible = false;
            label22.Visible = false;
            label23.Visible = false;

        }

        private void MostrarCliente()
        {
            dataGridView1.DataSource = ncliente.ListarCliente();
        }

        private void MostrarProductos()
        {
            dataGridView2.DataSource = gp.ListarProductos();
        }

        private void MostrarVenta()
        {
            dataGridView3.DataSource = nventa.ListarVenta();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int selec = e.RowIndex;
            if (e.RowIndex != -1)
            {
                if (selec != -1)
                {
                    dni = Convert.ToInt32(dataGridView1.Rows[selec].Cells[0].Value);
                    txtDNI.Text = Convert.ToString(dataGridView1.Rows[selec].Cells[0].Value);
                    txtNombre.Text = Convert.ToString(dataGridView1.Rows[selec].Cells[1].Value);
                    txtCelular.Text = Convert.ToString(dataGridView1.Rows[selec].Cells[2].Value);
                    txtDireccion.Text = Convert.ToString(dataGridView1.Rows[selec].Cells[3].Value);
                }
            }
            contD = 10;
            contC = 9;
        }

        private void btnAgregar_Click(object sender, EventArgs e)//cliente
        {
            if (txtDNI.Text != "" && txtNombre.Text != "" && txtCelular.Text != "" && txtDireccion.Text != "")
            {
                ncliente.InsertarCliente(Convert.ToInt32(txtDNI.Text.Trim()),txtNombre.Text.Trim(),Convert.ToInt32(txtCelular.Text.Trim()),txtDireccion.Text.Trim());
                txtDNI.Text = "";
                txtNombre.Text = "";
                txtCelular.Text = "";
                txtDireccion.Text = "";
                MostrarCliente();
                CB_Producto.Items.Clear();
                switch (CB_Categoria1.SelectedIndex)
                {
                    case 1: CB_Producto.Items.Add(gp.Productos("Carne Cruda")); break;
                    case 2: CB_Producto.Items.Add(gp.Productos("Carne Precocida")); break;
                    case 3: CB_Producto.Items.Add(gp.Productos("Lacteos")); break;
                }
                cmbDNIVenta.Items.Clear();
                foreach (var item in ncliente.Clientes())
                {
                    cmbDNIVenta.Items.Add(item);
                }
            }
            else
            {
                MessageBox.Show("Rellene todos los campos");
            }
            contD = 0;
            contC = 0;
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            contD = 10;
            contC = 9;
            if (dni != 0)
            {
                if (txtDNI.Text != "" && txtNombre.Text != "" && txtCelular.Text != "" && txtDireccion.Text != "") {
                    MessageBox.Show(ncliente.ModificarCliente(Convert.ToInt32(txtDNI.Text.Trim()), txtNombre.Text.Trim(), Convert.ToInt32(txtCelular.Text.Trim()), txtDireccion.Text.Trim()));
                    MostrarCliente();
                }
                else
                {
                    MessageBox.Show("Ingrese todos los datos.");
                }
            }
            else
            {
                MessageBox.Show("Debe seleccionar un empleo de la lista");
            }
            contD = 0;
            contC = 0;
        }

        private void btnAgregarProducto_Click(object sender, EventArgs e)
        {
            if (TB_NombreProducto.Text != "" && TB_Precio.Text != "" && CB_Categoria.SelectedIndex != -1 && Num_Cantidad.Value != 0)
            {
                MessageBox.Show(gp.RegistrarProducto(TB_NombreProducto.Text, Convert.ToDecimal(TB_Precio.Text), CB_Categoria.SelectedItem.ToString(), Convert.ToInt32(Num_Cantidad.Value)));
                MostrarProductos();
                TB_NombreProducto.Clear();
                TB_Precio.Clear();
                CB_Categoria.SelectedIndex = -1;
                Num_Cantidad.Value = 0;
                cmbCodeProdVenta.Items.Clear();
                foreach (var item in gp.Productos2())
                {
                    cmbCodeProdVenta.Items.Add(item);
                }
            }
            else
            {
                MessageBox.Show("Por favor complete todos los campos.");
            }
            
        }

        private bool ValidateTB_NombreProducto()
        {
            string text = TB_NombreProducto.Text.Trim();

            if (string.IsNullOrWhiteSpace(text) || text.Length > 20 || !text.All(char.IsLetter))
            {
                TB_NombreProducto.Focus();
                TB_NombreProducto.SelectAll();
                return false;
            }

            return true;
        }

        private void btnModificarProducto_Click(object sender, EventArgs e)
        {
 
            if (codProducto != -1)
            {
                if (!ValidateTB_NombreProducto())
				{
                    MessageBox.Show("No se puede modificar, se ingresó incorrectamente el siguente campo: Nombre");
				}
                else if (TB_NombreProducto.Text != "" && TB_Precio.Text != "" && CB_Categoria.SelectedIndex != -1 && Num_Cantidad.Value != 0)
                {
                    MessageBox.Show(gp.ModificarProducto(codProducto, TB_NombreProducto.Text, Convert.ToDecimal(TB_Precio.Text), CB_Categoria.SelectedItem.ToString(), Convert.ToInt32(Num_Cantidad.Value)));
                    MostrarProductos();
                    TB_NombreProducto.Clear();
                    TB_Precio.Clear();
                    CB_Categoria.SelectedIndex = -1;
                    Num_Cantidad.Value = 0;
                    codProducto = -1;
                }
                else
                {
                    MessageBox.Show("Ingrese todos los datos.");
                }
            }
            else
            {
                MessageBox.Show("Por favor seleccione un producto de la lista.");
            }
        }

        private void btnEliminarProducto_Click(object sender, EventArgs e)
        {
            if (codProducto != -1)
            {
                MessageBox.Show(gp.EliminarProducto(codProducto));
                MostrarProductos();
                TB_NombreProducto.Clear();
                TB_Precio.Clear();
                CB_Categoria.SelectedIndex = -1;
                Num_Cantidad.Value = 0;
                cmbCodeProdVenta.Items.Clear();
                foreach (var item in gp.Productos2())
                {
                    cmbCodeProdVenta.Items.Add(item);
                }
            }
            else
            {
                MessageBox.Show("Por favor seleccione un producto de la lista.");
            }
        }

        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int selec = e.RowIndex;
            if (e.RowIndex != -1)
            {
                if (selec != -1)
                {
                    codProducto = Convert.ToInt32(dataGridView2.Rows[selec].Cells[0].Value);
                    TB_NombreProducto.Text = Convert.ToString(dataGridView2.Rows[selec].Cells[1].Value);
                    TB_Precio.Text = Convert.ToString(dataGridView2.Rows[selec].Cells[2].Value);
                    CB_Categoria.SelectedItem = Convert.ToString(dataGridView2.Rows[selec].Cells[3].Value);
                    Num_Cantidad.Value = Convert.ToInt32(dataGridView2.Rows[selec].Cells[4].Value);
                }
            }
        }

        private void dataGridView3_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int selec = e.RowIndex;
            if (e.RowIndex != -1)
            {
                if (selec != -1)
                {
                    codVenta = Convert.ToInt32(dataGridView3.Rows[selec].Cells[0].Value);
                    cmbDNIVenta.Text = Convert.ToString(dataGridView3.Rows[selec].Cells[1].Value);
                    cmbCodeProdVenta.Text = Convert.ToString(dataGridView3.Rows[selec].Cells[2].Value);
                    Num_Cantidad_Venta.Value = Convert.ToInt32(dataGridView3.Rows[selec].Cells[3].Value);
                    lbl_Precio.Text = Convert.ToString(dataGridView3.Rows[selec].Cells[4].Value);
                    DT_Fecha.Value = Convert.ToDateTime(dataGridView3.Rows[selec].Cells[5].Value);
                }
            }
        }

        private void btnAgregarVenta_Click(object sender, EventArgs e)
        {
            if (cmbDNIVenta.SelectedIndex != -1 && cmbCodeProdVenta.SelectedIndex != -1 && Num_Cantidad_Venta.Value != -1 && DT_Fecha.Value != null)
            {
                int cantidad = gp.Cantidad(Convert.ToInt32(cmbCodeProdVenta.SelectedItem.ToString()));
                int comprar = Convert.ToInt32(Num_Cantidad_Venta.Value);
                if (comprar <= cantidad)
                {
                    nventa.AgregarVenta(Convert.ToInt32(cmbDNIVenta.SelectedItem.ToString().Trim()), Convert.ToInt32(cmbCodeProdVenta.SelectedItem.ToString().Trim()), Convert.ToInt32(Num_Cantidad_Venta.Value), Convert.ToDecimal(lbl_Precio.Text), DT_Fecha.Value);
                    MostrarVenta();
                    int actual = cantidad - comprar;
                    gp.ModificarCantidad(actual, Convert.ToInt32(cmbCodeProdVenta.SelectedItem.ToString()));
                    MostrarProductos();
                    cmbDNIVenta.SelectedIndex = -1;
                    cmbCodeProdVenta.SelectedIndex = -1;
                    Num_Cantidad_Venta.Value = 0;
                    lbl_Precio.Text = "";
                    DT_Fecha.Value = DateTime.Now;
                }
                else
                {
                    MessageBox.Show("No contamos con esta cantidad en el inventario.");
                }
            }
            else
            {
                MessageBox.Show("Rellene todos los campos");
            }
        }

        private void btnModificarVenta_Click(object sender, EventArgs e)
        {
            if (codVenta != -1)
            {
                int producto = gp.Cantidad(Convert.ToInt32(cmbCodeProdVenta.SelectedItem.ToString()));
                int cantidadRegistrada = nventa.Cantidad(codVenta);
                int comprar = Convert.ToInt32(Num_Cantidad_Venta.Value);
                if (comprar <= producto)
                {
                    producto += (cantidadRegistrada - comprar);
                    MessageBox.Show(nventa.ModificarVenta(codVenta, Convert.ToInt32(cmbDNIVenta.Text.Trim()), Convert.ToInt32(cmbCodeProdVenta.Text.Trim()), Convert.ToInt32(Num_Cantidad_Venta.Value), Convert.ToDecimal(lbl_Precio.Text), DT_Fecha.Value));
                    gp.ModificarCantidad(producto, Convert.ToInt32(cmbCodeProdVenta.SelectedItem.ToString()));
                    MostrarProductos();
                    MostrarVenta();
                }
                cmbDNIVenta.SelectedIndex = -1;
                cmbCodeProdVenta.SelectedIndex = -1;
                Num_Cantidad_Venta.Value = 0;
                lbl_Precio.Text = "0.00";
                DT_Fecha.Value = DateTime.Now;
            }
            else
            {
                MessageBox.Show("Por favor seleccione una venta de la lista.");
            }

        }

        private void btnEliminarVenta_Click(object sender, EventArgs e)
        {
            if (codVenta != -1)
            {
                int producto = gp.Cantidad(Convert.ToInt32(cmbCodeProdVenta.SelectedItem.ToString()));
                int cantidadRegistrada = Convert.ToInt32(Num_Cantidad_Venta.Value);
                producto += cantidadRegistrada;
                gp.ModificarCantidad(producto, Convert.ToInt32(cmbCodeProdVenta.SelectedItem.ToString()));
                MostrarProductos();
                MessageBox.Show(nventa.EliminarVenta(codVenta));
                MostrarVenta();
                cmbDNIVenta.SelectedIndex = -1;
                cmbCodeProdVenta.SelectedIndex = -1;
                Num_Cantidad_Venta.Value = 0;
                lbl_Precio.Text = "0.00";
                DT_Fecha.Value = DateTime.Now;
            }
            else
            {
                MessageBox.Show("Por favor seleccione una venta de la lista.");
            }
        }

        private void btn_Mostrar_Click(object sender, EventArgs e)
        {
            if (CB_Categoria1.SelectedIndex != -1 && CB_Producto.SelectedIndex != -1 && Num_Mes.Value != 0)
            {
                dataGridView4.DataSource = nventa.Reporte1(Convert.ToString(Num_Mes.Value), CB_Producto.SelectedItem.ToString());
                lbl_Cantidad.Text = nventa.Cantidad(Convert.ToString(Num_Mes.Value), CB_Producto.SelectedItem.ToString());
            }
            else
            {
                MessageBox.Show("Por favor seleccione en todos los campos.");
            }
        }

        private void CB_Categoria1_SelectedIndexChanged(object sender, EventArgs e)
        {
            CB_Producto.Items.Clear();
            switch (CB_Categoria1.SelectedIndex)
            {
                case 0:
                    foreach (var item in gp.Productos("Carne Cruda"))
                    {
                        CB_Producto.Items.Add(item);
                    } 
                    break;
                case 1: 
                    foreach (var item in gp.Productos("Carne Precocida"))
                    {
                        CB_Producto.Items.Add(item);
                    } 
                    break;
                case 2: 
                    foreach (var item in gp.Productos("Lacteos"))
                    {
                        CB_Producto.Items.Add(item);
                    } 
                    break;
            }
        }

        private void btnMostrar2_Click(object sender, EventArgs e)
        {
            if (Num_mes2.Value != 0)
            {
                dataGridView5.DataSource = nventa.Reporte2(Convert.ToString(Num_mes2.Value));
            }
            else
            {
                MessageBox.Show("Por favor seleccione en todos los campos.");
            }
        }

        private void Num_Cantidad_Venta_ValueChanged(object sender, EventArgs e)
        {
            if(cmbCodeProdVenta.SelectedIndex != -1 && Num_Cantidad_Venta.Value != 0)
            {
                Decimal precio = gp.Precio(Convert.ToInt32(cmbCodeProdVenta.SelectedItem.ToString())) * Convert.ToDecimal(Num_Cantidad_Venta.Value);
                lbl_Precio.Text = Convert.ToString(precio);
            }
        }

        private void cmbCodeProdVenta_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Num_Cantidad_Venta.Value != 0 && cmbCodeProdVenta.SelectedIndex != -1)
            {
                Decimal precio = gp.Precio(Convert.ToInt32(cmbCodeProdVenta.SelectedItem.ToString())) * Convert.ToDecimal(Num_Cantidad_Venta.Value);
                lbl_Precio.Text = Convert.ToString(precio);
            }
        }

        private void btnEstadistica_Click(object sender, EventArgs e)
        {

            chart1.Visible = true;
            label21.Visible = true;
            label22.Visible = true;
            label23.Visible = true;
            dataGridView3.Visible = false;


            chart1.Series["Producto"].Points.Clear();
            dt = nventa.Estadistica();
            int xnrofilas = dt.Rows.Count;
            decimal xsuma_soles = 0;
            for (int i = 0; i <= xnrofilas - 1; i++)
            {
                chart1.Series["Producto"].Points.AddXY(dt.Rows[i]["Producto"], dt.Rows[i]["VentaEnSoles"]);
                xsuma_soles = xsuma_soles + Convert.ToDecimal(dt.Rows[i]["VentaEnSoles"]);
            }
            label23.Text = Convert.ToString(xsuma_soles);
        }

        private void btnVenta_Click(object sender, EventArgs e)
        {

            dataGridView3.Visible = true;
            chart1.Visible = false;
            label21.Visible = false;
            label22.Visible = false;
            label23.Visible = false;
        }

        private void txtDNI_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (txtDNI.Text == "")
            {
                contD = 0;
            }
            if (contD < 8)
            {
                if (Char.IsDigit(e.KeyChar))
                {
                    e.Handled = false;
                    contD++;
                }
                else if (Char.IsControl(e.KeyChar))
                {
                    e.Handled = false;
                    if (contD > 0)
                    {
                        contD--;
                    }
                }
                else
                {
                    e.Handled = true;
                }
            }
            else if (contD == 8)
            {
                if (Char.IsControl(e.KeyChar))
                {
                    e.Handled = false;
                    contD--;
                }
                else
                {
                    e.Handled = true;
                }
            }
            else
            {
                e.Handled = true;
            }
        }

        private void txtNombre_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsLetter(e.KeyChar))
            {
                e.Handled = false;
            }
            else if (Char.IsSeparator(e.KeyChar))
            {
                e.Handled = false;
            }
            else if (Char.IsControl(e.KeyChar))
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }

        private void txtCelular_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (txtCelular.Text == "")
            {
                contC = 0;
            }
            if (contC < 9)
            {
                if (Char.IsDigit(e.KeyChar))
                {
                    e.Handled = false;
                    contC++;
                }
                else if (Char.IsControl(e.KeyChar))
                {
                    e.Handled = false;
                    if (contC > 0)
                    {
                        contC--;
                    }
                }
                else
                {
                    e.Handled = true;
                }
            }
            else if (contC == 9)
            {
                if (Char.IsControl(e.KeyChar))
                {
                    e.Handled = false;
                    contC--;
                }
                else
                {
                    e.Handled = true;
                }
            }
            else
            {
                e.Handled = true;
            }
        }

        private void TB_NombreProducto_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsLetterOrDigit(e.KeyChar))
            {
                e.Handled = false;
            }
            else if (Char.IsSeparator(e.KeyChar))
            {
                e.Handled = false;
            }
            else if(Char.IsControl(e.KeyChar))
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }

        private void TB_Precio_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(Char.IsDigit(e.KeyChar))
            {
                e.Handled = false;
            }
            else if (Char.IsPunctuation(e.KeyChar))
            {
                e.Handled = false;
            }
            else if (Char.IsControl(e.KeyChar))
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }

        private void CB_Categoria_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void cmbDNIVenta_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void cmbCodeProdVenta_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void CB_Categoria1_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void CB_Producto_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void Num_Cantidad_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void Num_Cantidad_Venta_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void Num_Mes_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void Num_mes2_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void btnCliente_Click(object sender, EventArgs e)
        {

            dataGridView1.Visible = true;
            chart2.Visible = false;
        }

        private void btnEstadisticaCliente_Click(object sender, EventArgs e)
        {

            chart2.Visible = true;
            dataGridView1.Visible = false;

            chart2.Series["Nombre"].Points.Clear();
            dt = ncliente.Estadistica();
            int xnrofilas = dt.Rows.Count;
            for (int i = 0; i <= xnrofilas - 1; i++)
            {
                chart2.Series["Nombre"].Points.AddXY(dt.Rows[i]["Nombre"], dt.Rows[i]["ComprasSoles"]);
            }
        }

	}
	
}
