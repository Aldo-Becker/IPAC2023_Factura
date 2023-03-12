using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class DetalleFactura
    {
        public int Id { get; set; }
        public int IdFactura { get; set; }
        public string CodigoProductor { get; set; }
        public decimal Precio { get; set; }
        public int Cantidad { get; set; }
        public decimal Total { get; set; }

        public DetalleFactura()
        {
        }

        public DetalleFactura(int id, int idFactura, string codigoProductor, decimal precio, int cantidad, decimal total)
        {
            Id = id;
            IdFactura = idFactura;
            CodigoProductor = codigoProductor;
            Precio = precio;
            Cantidad = cantidad;
            Total = total;
        }
    }
}
