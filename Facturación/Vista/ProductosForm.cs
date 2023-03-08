using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Vista
{
    public partial class ProductosForm : Syncfusion.Windows.Forms.Office2010Form
    {
        public ProductosForm()
        {
            InitializeComponent();
        }
        string operacion;

        private void NuevoButton_Click(object sender, EventArgs e)
        {
            operacion = "Nuevo";
            HabilitarControles();
        }
        public void HabilitarControles()
        {
            CodigoTextBox.Enabled = true;
            DescripcionTextBox.Enabled = true;
            ExistenciaTextBox.Enabled = true;
            PrecioTextBox.Enabled = true;
            AdjuntarImagenButton.Enabled = true;
            GuardarButton.Enabled = true;
            CancelarButton.Enabled = true;
            NuevoButton.Enabled = false;
        }
        public void DesabilitarControles()
        {
            CodigoTextBox.Enabled = false;
            DescripcionTextBox.Enabled = false;
            ExistenciaTextBox.Enabled = false;
            PrecioTextBox.Enabled = false;
            AdjuntarImagenButton.Enabled = false;
            GuardarButton.Enabled = false;
            CancelarButton.Enabled = false;
            NuevoButton.Enabled = true;
        }

        private void CancelarButton_Click(object sender, EventArgs e)
        {
            DesabilitarControles();
        }

        private void ModificarButton_Click(object sender, EventArgs e)
        {
            operacion = "Modificar";
        }

        private void GuardarButton_Click(object sender, EventArgs e)
        {
            if (operacion == "Nuevo")
            {
                if (string.IsNullOrEmpty(CodigoTextBox.Text))
                {
                    errorProvider1.SetError(CodigoTextBox, "Ingrese un Código");
                    CodigoTextBox.Focus();
                    return;

                }
                errorProvider1.Clear();
                if (string.IsNullOrEmpty(DescripcionTextBox.Text))
                {
                    errorProvider1.SetError(DescripcionTextBox, "Ingrese una Descripción");
                    DescripcionTextBox.Focus();
                    return;

                }
                errorProvider1.Clear();
                if (string.IsNullOrEmpty(ExistenciaTextBox.Text))
                {
                    errorProvider1.SetError(ExistenciaTextBox, "Ingrese una Existencia");
                    ExistenciaTextBox.Focus();
                    return;

                }
                errorProvider1.Clear();
                if (string.IsNullOrEmpty(PrecioTextBox.Text))
                {
                    errorProvider1.SetError(PrecioTextBox, "Ingrese un Precio");
                    PrecioTextBox.Focus();
                    return;

                }
                errorProvider1.Clear();
            }
            


        }
        //Para Evitar que se ingrese un numero
        private void ExistenciaTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            //hay que quitar el Signo de exclamacion
            if (!char.IsNumber(e.KeyChar))
            {
                e.Handled = true;
            }
            //else
            //{
            //    e.Handled = true;
            //}
        }

        private void PrecioTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
            //Evitar que se Agreguen mas de 2 decimales
            if((e.KeyChar == '.') && (sender as TextBox).Text.IndexOf('.') > -1)
            {
                e.Handled = true;
            }

        }
    }
}
