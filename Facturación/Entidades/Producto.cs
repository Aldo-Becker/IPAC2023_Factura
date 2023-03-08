using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class Producto
    {
        public string Código { get; set; }
        public string Descripción { get; set; }
        public int Existencia { get; set; }
        public decimal Precio { get; set; }
        public byte[] Imagen { get; set; }

        public Producto()
        {

        }

        public Producto(string código, string descripción, int existencia, decimal precio, byte[] imagen)
        {
            Código = código;
            Descripción = descripción;
            Existencia = existencia;
            Precio = precio;
            Imagen = imagen;
        }
    }
}
