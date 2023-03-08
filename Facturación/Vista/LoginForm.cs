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
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
        }

        private void CancelarButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void AceptarButton_Click(object sender, EventArgs e)
        {
            if (UsuarioTextBox.Text == String.Empty)
            {
                errorProvider1.SetError(UsuarioTextBox, "Ingrese un Usuario");
                return;
            }
            errorProvider1.Clear();
            if (string.IsNullOrEmpty(ContraseñaTextBox.Text))
            {
                errorProvider1.SetError(ContraseñaTextBox, "Ingrese una Contraseña");
                ContraseñaTextBox.Focus();
                return;
            }
            errorProvider1.Clear();

            //Validar de la Base de Datos

            Login login = new Login(UsuarioTextBox.Text,ContraseñaTextBox.Text);
            Usuario usuario = new Usuario();
            UsuarioDB usuarioDB = new UsuarioDB();

            usuario = usuarioDB.Auntenticar(login);


            if (usuario != null)
            {
                if (usuario.EstaActivo)
                {
                    //Mostramos el Menú
                    Menu menuFormulario = new Menu();
                    this.Hide();
                    menuFormulario.Show();
                }
                else
                {
                    MessageBox.Show("El Usuario no Esta Activo", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
            else
            {
                MessageBox.Show("Datos de Usuario Incorrectos", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            //instanciar objeto
            
        }

        private void MostrarContraseñaButton_Click(object sender, EventArgs e)
        {
            if (ContraseñaTextBox.PasswordChar == '*')
            {
                //Forma de Utilizar null
                ContraseñaTextBox.PasswordChar = '\0';
            }
            else
            {
                ContraseñaTextBox.PasswordChar = '*';
            }
        }
    }
}
