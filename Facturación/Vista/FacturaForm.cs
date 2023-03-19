using Datos;
using Entidades;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Vista
{
    public partial class FacturaForm : Form
    {
        public FacturaForm()
        {
            InitializeComponent();
        }

        Cliente miCliente = null;
        ClienteDB clienteDB = new ClienteDB();
        Producto miProducto = null;
        ProductoDB productoDB = new ProductoDB();
        List<DetalleFactura> listaDetalles = new List<DetalleFactura>(); 
        //instanciar
        FacturaDB facturaDB = new FacturaDB();
        decimal subTotal = 0;
        decimal isv = 0;
        decimal totalAPagar = 0;
        decimal descuento = 0;

        private void IdentidadTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter && !string.IsNullOrEmpty(IdentidadTextBox.Text))
            {
                miCliente = new Cliente();
                miCliente  = clienteDB.DevolverClientePorIdentidad(IdentidadTextBox.Text);
                NombreClienteTextBox.Text = miCliente.Nombre;
            }
            else
            {
                miCliente = null;
                NombreClienteTextBox.Clear();
            }
        }

        private void BuscarClienteButton_Click(object sender, EventArgs e)
        {
            BuscarClienteForm form = new BuscarClienteForm();
            form.ShowDialog();
            //Instanciar
            miCliente = new Cliente();
            miCliente = form.cliente;
            IdentidadTextBox.Text = miCliente.Identidad;
            NombreClienteTextBox.Text = miCliente.Nombre;

        }

        private void FacturaForm_Load(object sender, EventArgs e)
        {
            UsuarioTextBox.Text = System.Threading.Thread.CurrentPrincipal.Identity.Name;
        }

        private void CodigoProductoTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter && !string.IsNullOrEmpty(CodigoProductoTextBox.Text))
            {
                //instanciar
                miProducto = new Producto();
                miProducto = productoDB.DevolverProductoPorCodigo(CodigoProductoTextBox.Text);
                DescripcionProductoTextBox.Text = miProducto.Descripción;
                ExistenciaTextBox.Text = miProducto.Existencia.ToString();
            }
            else
            {
                miProducto = null;
                DescripcionProductoTextBox.Clear();
                ExistenciaTextBox.Clear();
            }
        }

        private void BuscarProductoutton_Click(object sender, EventArgs e)
        {
            BuscarProductoForm form = new BuscarProductoForm();
            form.ShowDialog();
            //Instanciar
            miProducto = new Producto();
            miProducto = form.producto;
            CodigoProductoTextBox.Text = miProducto.Código;
            DescripcionProductoTextBox.Text = miProducto.Descripción;
            ExistenciaTextBox.Text = miProducto.Existencia.ToString();
        }

        private void CantidadTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter && !string.IsNullOrEmpty(CantidadTextBox.Text))
            {
                //instanciar
                DetalleFactura detalle = new DetalleFactura();
                detalle.CodigoProducto = miProducto.Código;
                detalle.Cantidad = Convert.ToInt32(CantidadTextBox.Text);
                detalle.Precio = Convert.ToDecimal(miProducto.Precio);
                detalle.Total = Convert.ToInt32(CantidadTextBox.Text) * miProducto.Precio;
                detalle.Descripcion = miProducto.Descripción;

                subTotal = subTotal + detalle.Total;
                //para que lo lea como decimal se coloca una M
                isv = subTotal * 0.15M;
                totalAPagar = subTotal + isv - descuento;

                listaDetalles.Add(detalle);
                DetalleDataGridView.DataSource = null;
                DetalleDataGridView.DataSource = listaDetalles;

                SubTotalTextBox.Text = subTotal.ToString();
                ISVTextBox.Text = isv.ToString();
                TotalTextBox.Text = totalAPagar.ToString();

                miProducto = null;
                CodigoProductoTextBox.Clear();
                DescripcionProductoTextBox.Clear();
                ExistenciaTextBox.Clear();
                CantidadTextBox.Clear();
                CodigoProductoTextBox.Focus();

            }
        }

        private void GuardarButton_Click(object sender, EventArgs e)
        {
            //instanciar
            Factura miFactura = new Factura();
            miFactura.Fecha = FechaDateTimePicker.Value;
            miFactura.CodigoUsuario = System.Threading.Thread.CurrentPrincipal.Identity.Name;
            miFactura.IdentidadCliente = miCliente.Identidad;
            miFactura.SubTotal = subTotal;
            miFactura.ISV = isv;
            miFactura.Descuento = descuento;
            miFactura.Total = totalAPagar;

            bool inserto = facturaDB.Guardar(miFactura, listaDetalles);

            if (inserto)
            {
                LimpiarControles();
                IdentidadTextBox.Focus();
                MessageBox.Show("Factura Registrada Exitosamente");
            }
            else
            {
                MessageBox.Show("No se Pudo Registrar la Factura");
            }
        }

        private void LimpiarControles()
        {
            miCliente = null;
            miProducto = null;
            listaDetalles = null;
            FechaDateTimePicker.Value = DateTime.Now;
            IdentidadTextBox.Clear();
            NombreClienteTextBox.Clear();
            CodigoProductoTextBox.Clear();
            DescripcionProductoTextBox.Clear();
            ExistenciaTextBox.Clear();
            CantidadTextBox.Clear();
            DetalleDataGridView.DataSource = null;
            subTotal = 0;
            SubTotalTextBox.Clear();
            isv = 0;
            ISVTextBox.Clear();
            descuento = 0;
            DescuentoTextBox.Clear();
            totalAPagar =0;
            TotalTextBox.Clear();
        }
    }
}
