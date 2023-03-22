using Entidades;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos
{
    public class FacturaDB
    {
        string cadena = "server=localhost; user=root; database=Factura; password=SemitaSql";

        public bool Guardar(Factura factura, List<DetalleFactura> detalles)
        {
            bool inserto = false;
            int idFactura = 0;
            try
            {
                StringBuilder sqlFactura = new StringBuilder();
                sqlFactura.Append(" INSERT INTO factura (Fecha, IdentidadCliente, CodigoUsuario, ISV, Descuento, SubTotal, Total) VALUES (@Fecha, @IdentidadCliente, @CodigoUsuario, @ISV, @Descuento, @SubTotal, @Total); ");
                //devuelve el ultimo id o llave primaria de la tabla que acaba de insertar
                sqlFactura.Append(" SELECT LAST_INSERT_ID(); ");

                StringBuilder sqlDetalle = new StringBuilder();
                sqlDetalle.Append(" INSERT INTO detallefactura (IdFactura, CodigoProducto, Precio, Cantidad, Total) VALUES (@IdFactura, @CodigoProducto, @Precio, @Cantidad, @Total); ");

                StringBuilder sqlExistencia = new StringBuilder();
                //Actualizar un Valor
                sqlExistencia.Append(" UPDATE producto SET Existencia = Existencia - @Cantidad WHERE Codigo = @Codigo; ");

                using (MySqlConnection con = new MySqlConnection(cadena))
                {
                    con.Open();

                    //transacciones: Evita fallos en ejecuciones de base de datos
                    MySqlTransaction transaccion = con.BeginTransaction();

                    try
                    {
                        using (MySqlCommand cmd1 = new MySqlCommand(sqlFactura.ToString(), con, transaccion))
                        {
                            cmd1.CommandType = System.Data.CommandType.Text;
                            cmd1.Parameters.Add("@Fecha", MySqlDbType.DateTime).Value = factura.Fecha;
                            cmd1.Parameters.Add("@IdentidadCliente", MySqlDbType.VarChar,25).Value = factura.IdentidadCliente;
                            cmd1.Parameters.Add("@CodigoUsuario", MySqlDbType.VarChar,50).Value = factura.CodigoUsuario;
                            cmd1.Parameters.Add("@ISV", MySqlDbType.Decimal).Value = factura.ISV;
                            cmd1.Parameters.Add("@Descuento", MySqlDbType.Decimal).Value = factura.Descuento;
                            cmd1.Parameters.Add("@SubTotal", MySqlDbType.Decimal).Value = factura.SubTotal;
                            cmd1.Parameters.Add("@Total", MySqlDbType.Decimal).Value = factura.Total;
                            idFactura = Convert.ToInt32(cmd1.ExecuteScalar());
                        }

                        //Para iterar listas
                        foreach(DetalleFactura detalle in detalles)
                        {
                            using (MySqlCommand cmd2 = new MySqlCommand(sqlDetalle.ToString(), con, transaccion))
                            {
                                cmd2.CommandType = System.Data.CommandType.Text;
                                cmd2.Parameters.Add("@Factura", MySqlDbType.Int32).Value = idFactura;
                                cmd2.Parameters.Add("@CodigoProducto", MySqlDbType.VarChar, 50).Value = detalle.CodigoProducto;
                                cmd2.Parameters.Add("@Precio", MySqlDbType.Decimal).Value = detalle.Precio;
                                cmd2.Parameters.Add("@Cantidad", MySqlDbType.Decimal).Value = detalle.Cantidad;
                                cmd2.Parameters.Add("@Total", MySqlDbType.Decimal).Value = detalle.Total;
                                cmd2.ExecuteNonQuery();
                            }

                            using (MySqlCommand cmd3 = new MySqlCommand(sqlExistencia.ToString(), con, transaccion))
                            {
                                cmd3.CommandType = System.Data.CommandType.Text;
                                cmd3.Parameters.Add("@Cantidad", MySqlDbType.Decimal).Value = detalle.Cantidad;
                                cmd3.Parameters.Add("@Codigo", MySqlDbType.VarChar, 80).Value = detalle.CodigoProducto;
                                cmd3.ExecuteNonQuery();
                            }
                        }
                        transaccion.Commit();
                        inserto = true;
                    }
                    catch (System.Exception ex)
                    {
                        inserto = false;
                        transaccion.Rollback();
                    }

                }
            }
            catch (System.Exception)
            {

            }
            return inserto;
        }
    }
}
