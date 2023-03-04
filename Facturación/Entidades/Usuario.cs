using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class Usuario
    {
        public string CodigoUsuario { get; set; }

        public string Nombre { get; set; }

        public string Contraseña { get; set; }

        public string Rol { get; set; }

        public string Correo { get; set; }

        public byte [] Foto { get; set; }

        public DateTime FechaCreacion { get; set; }

        public bool EstaActivo { get; set; }

        public Usuario()
        {

        }

        public Usuario(string codigoUsuario, string nombre, string contraseña, string rol, string correo, byte[] foto, DateTime fechaCreacion, bool estaActivo)
        {
            CodigoUsuario = codigoUsuario;
            Nombre = nombre;
            Contraseña = contraseña;
            Rol = rol;
            Correo = correo;
            Foto = foto;
            FechaCreacion = fechaCreacion;
            EstaActivo = estaActivo;
        }


    }
}
