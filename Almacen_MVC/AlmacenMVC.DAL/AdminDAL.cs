using Almacen_MVC.AlmacenMVC.Entities;
using Almacen_MVC.Contratos;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Configuration;


namespace Almacen_MVC.AlmacenMVC.DAL
{
    public class AdminDAL : IAdmin
    {
        string dbconexion;
        public AdminDAL()
        {
            dbconexion = ConfigurationManager.ConnectionStrings["ConectaProductos"].ConnectionString;

        }
        public async Task<List<Venta>> ObtenerProductoFecha(DateTime fecha)
        {
            List<Venta> ListVentas = new List<Venta>();
            using (SqlConnection con = new SqlConnection(dbconexion))
            {
                SqlCommand cmd = new SqlCommand("ObtenerProductoPorFecha", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@FechaProducto", fecha);
                try
                {
                    await con.OpenAsync();
                    SqlDataReader sdr = await cmd.ExecuteReaderAsync();
                    if (sdr.HasRows)
                    {
                        while (sdr.Read())
                        {
                            ListVentas.Add(new Venta
                            {
                                VentaId = Convert.ToInt16(sdr["VentaId"]),
                                ProductoId = Convert.ToInt16(sdr["ProductoId"]),
                                Cantidad = Convert.ToInt16(sdr["Cantidad"]),
                                TotalVenta = Convert.ToDouble(sdr["TotalVenta"]),
                                Fecha = Convert.ToDateTime(sdr["Fecha"]),

                            });
                        }
                        con.Close();
                    }
                    else
                    {
                        ListVentas = null;
                    }
                }
                catch (Exception)
                {
                    con.Close();
                }
                return (ListVentas);
            }
        }

        public async Task<bool> AgregarProducto(Producto prd)
        {
            using(SqlConnection con = new SqlConnection(dbconexion))
            {
                SqlCommand cmd = new SqlCommand("AgregarProducto", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Nombre", prd.Nombre);
                cmd.Parameters.AddWithValue("@Presentacion", prd.Presentacion);
                cmd.Parameters.AddWithValue("@PMayoreo", prd.PMayoreo);
                cmd.Parameters.AddWithValue("@PMenudeo", prd.PMenudeo);
                cmd.Parameters.AddWithValue("@Existencia", prd.Existencia);
                cmd.Parameters.AddWithValue("@CostoUnitario", prd.CostoUnitario);
                cmd.Parameters.AddWithValue("@ImagenPath", prd.ImagenPath);
                try
                {
                    await con.OpenAsync();
                    cmd.ExecuteNonQuery();
                    con.Close();
                    return true;
                }
                catch (Exception x)
                {
                    string m = x.Message;
                    con.Close();
                    return false;
                }

            }
        }
    }
}