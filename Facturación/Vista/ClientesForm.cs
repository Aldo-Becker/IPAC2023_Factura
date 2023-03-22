using Datos;
using Entidades;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace Vista
{
    public partial class ClientesForm : Syncfusion.Windows.Forms.Office2010Form
    {
        public ClientesForm()
        {
            InitializeComponent();
        }
        
        string clienteOperacion;
        Cliente cliente = new Cliente();
        ClienteDB clienteDB = new ClienteDB();
        

        private void NuevoButton_Click(object sender, EventArgs e)
        {
            IdentidadTextBox.Focus();
            clienteOperacion = "Nuevo";
            HabilitarControles();
        }
        private void ModificarButton_Click(object sender, EventArgs e)
        {
            clienteOperacion = "Modificar";
            if (ClientesDataGridView.SelectedRows.Count > 0)
            {
                IdentidadTextBox.Text = ClientesDataGridView.CurrentRow.Cells["Identidad"].Value.ToString();
                NombreTextBox.Text = ClientesDataGridView.CurrentRow.Cells["Nombre"].Value.ToString();
                TelefonoTextBox.Text = ClientesDataGridView.CurrentRow.Cells["Telefono"].Value.ToString();
                CorreoTextBox.Text = ClientesDataGridView.CurrentRow.Cells["Correo"].Value.ToString();
                DireccionTextBox.Text = ClientesDataGridView.CurrentRow.Cells["Direccion"].Value.ToString();
                EstaActivoCheckBox.Checked = Convert.ToBoolean(ClientesDataGridView.CurrentRow.Cells["EstaActivo"].Value);

                HabilitarControles();
            }
            else
            {
                MessageBox.Show("Debe Seleccionar un Registro");
            }
        }

        public void HabilitarControles()
        {
            IdentidadTextBox.Enabled = true;
            NombreTextBox.Enabled = true;
            TelefonoTextBox.Enabled = true;
            CorreoTextBox.Enabled = true;
            DireccionTextBox.Enabled = true;
            FechaNacimientoDateTimePicker.Enabled = true;
            EstaActivoCheckBox.Enabled = true;
            GuardarButton.Enabled = true;
            CancelarButton.Enabled = true;
            ModificarButton.Enabled = false;
        }
        private void DeshabilitarControles()
        {
            IdentidadTextBox.Enabled = false;
            NombreTextBox.Enabled = false;
            TelefonoTextBox.Enabled = false;
            CorreoTextBox.Enabled = false;
            DireccionTextBox.Enabled = false;
            FechaNacimientoDateTimePicker.Enabled = false;
            EstaActivoCheckBox.Enabled = false;
            GuardarButton.Enabled = false;
            CancelarButton.Enabled = false;
            ModificarButton.Enabled = true;
        }
        private void LimpiarControles()
        {
            IdentidadTextBox.Clear();
            NombreTextBox.Clear();
            TelefonoTextBox.Clear();
            CorreoTextBox.Clear();
            DireccionTextBox.Clear();
            FechaNacimientoDateTimePicker = null;
            EstaActivoCheckBox.Checked = false; 
        }

        private void GuardarButton_Click(object sender, EventArgs e)
        {
            
            if (clienteOperacion == "Nuevo")
            {
                if (string.IsNullOrEmpty(IdentidadTextBox.Text))
                {
                    errorProvider1.SetError(IdentidadTextBox, "Ingrese una Identidad");
                    IdentidadTextBox.Focus();
                    return;
                }
                errorProvider1.Clear();
                if (string.IsNullOrEmpty(NombreTextBox.Text))
                {
                    errorProvider1.SetError(NombreTextBox, "Ingrese un Nombre");
                    NombreTextBox.Focus();
                    return;
                }
                errorProvider1.Clear();
                if (string.IsNullOrEmpty(TelefonoTextBox.Text))
                {
                    errorProvider1.SetError(TelefonoTextBox, "Ingrese un Número de Teléfono");
                    TelefonoTextBox.Focus();
                    return;
                }
                errorProvider1.Clear();
                if (string.IsNullOrEmpty(CorreoTextBox.Text))
                {
                    errorProvider1.SetError(CorreoTextBox, "Ingrese un Correo");
                    CorreoTextBox.Focus();
                    return;
                }
                errorProvider1.Clear();
                if (string.IsNullOrEmpty(DireccionTextBox.Text))
                {
                    errorProvider1.SetError(DireccionTextBox, "Ingrese una Dirección");
                    DireccionTextBox.Focus();
                    return;
                }
                errorProvider1.Clear();

                cliente.Identidad = IdentidadTextBox.Text;
                cliente.Nombre = NombreTextBox.Text;
                cliente.Telefono = TelefonoTextBox.Text;
                cliente.Correo = CorreoTextBox.Text;
                cliente.Direccion = DireccionTextBox.Text;
                cliente.FechaNacimiento = FechaNacimientoDateTimePicker.Value;
                cliente.EstaActivo = EstaActivoCheckBox.Checked;

                //Insertar en la Base de Datos
                bool inserto = clienteDB.Insertar(cliente);
                if (inserto)
                {
                    LimpiarControles();
                    DeshabilitarControles();
                    TraerClientes();
                    MessageBox.Show("Registro Guardado");
                }
                else
                {
                    MessageBox.Show("No se Pudo Guardar el Registro");
                }
            }
            else if (clienteOperacion == "Modificar")
            {
                cliente.Identidad = IdentidadTextBox.Text;
                cliente.Nombre = NombreTextBox.Text;
                cliente.Telefono = TelefonoTextBox.Text;
                cliente.Correo = CorreoTextBox.Text;
                cliente.Direccion = DireccionTextBox.Text;
                cliente.FechaNacimiento = FechaNacimientoDateTimePicker.Value;
                cliente.EstaActivo = EstaActivoCheckBox.Checked;
               
                bool modifico = clienteDB.Editar(cliente);

                if (modifico)
                {
                    //IdentidadTextBox.ReadOnly = false;
                    DeshabilitarControles();
                    LimpiarControles();
                    TraerClientes();
                    MessageBox.Show("Registro Acctualizado con Exito", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("No se Pudo Actualizar el Registro", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void CancelarButton_Click(object sender, EventArgs e)
        {
            DeshabilitarControles();
            LimpiarControles();
        }

        private void TraerClientes()
        {
            ClientesDataGridView.DataSource = clienteDB.DevolverClientes();
        }

        private void EliminarButton_Click(object sender, EventArgs e)
        {
            if (ClientesDataGridView.SelectedRows.Count > 0)
            {
                DialogResult resultado = MessageBox.Show("Esta Seguro de Eliminar el Registro", "Advertencia", MessageBoxButtons.YesNo);

                if (resultado == DialogResult.Yes)
                {
                    bool elimino = clienteDB.Eliminar(ClientesDataGridView.CurrentRow.Cells["Identidad"].Value.ToString());

                    if (elimino)
                    {
                        LimpiarControles();
                        DeshabilitarControles();
                        TraerClientes();
                        MessageBox.Show("Registro Eliminado");
                    }
                    else
                    {
                        MessageBox.Show("No se Pudo Eliminar el Registro");
                    }
                }
            }
        }

        private void ClientesForm_Load(object sender, EventArgs e)
        {
            TraerClientes();
        }

        private void FechaNacimientoDateTimePicker_ValueChanged(object sender, EventArgs e)
        {
            FechaNacimientoDateTimePicker.CustomFormat = "dd/MM/yyyy";
        }

        private void FechaNacimientoDateTimePicker_KeyDown(object sender, KeyEventArgs e)
        {
            if((e.KeyCode == Keys.Back) || (e.KeyCode == Keys.Delete))
            {
                FechaNacimientoDateTimePicker.CustomFormat = " ";
            }
        }
    }
}
