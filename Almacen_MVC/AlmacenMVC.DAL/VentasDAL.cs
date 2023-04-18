using Almacen_MVC.AlmacenMVC.Entities;
using Almacen_MVC.Contratos;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Almacen_MVC.AlmacenMVC.DAL
{
    public class VentasDAL : IVentas
    {
        string dbconexion;
        public VentasDAL()
        {
            dbconexion = ConfigurationManager.ConnectionStrings["ConectaProductos"].ConnectionString;
        }
        public async Task<Producto> ObtenerProducto(int id)
        {
            Producto Pr = new Producto();
            using (SqlConnection con = new SqlConnection(dbconexion))
            {
                SqlCommand cmd = new SqlCommand("ObtenerProductos", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ProductoId", id);
                try
                {
                    await con.OpenAsync();
                    SqlDataReader sdr = await cmd.ExecuteReaderAsync();
                    if (sdr.HasRows)
                    {
                        while (sdr.Read())
                        {
                            Pr.Nombre = sdr["Nombre"].ToString();
                            Pr.Presentacion = sdr["Presentacion"].ToString();
                            Pr.PMayoreo = Convert.ToDouble(sdr["PMayoreo"]);
                            Pr.PMenudeo = Convert.ToDouble(sdr["PMenudeo"]);
                        }
                        con.Close();
                    }
                    else
                    {
                        Pr = null;
                    }
                }
                catch (Exception)
                {
                    con.Close();
                }
                return (Pr);
            }
        }

       

        public async Task<List<SelectListItem>> ObtenerProductoId()
        {
            List<SelectListItem> ProductoId = new List<SelectListItem>();
            using(SqlConnection con = new SqlConnection(dbconexion))
            {
                SqlCommand cmd = new SqlCommand("ObtenerProductoId", con);
                cmd.CommandType = CommandType.StoredProcedure;
                try
                {
                    await con.OpenAsync();
                    SqlDataReader sdr = await cmd.ExecuteReaderAsync();
                    if (sdr.HasRows)
                    {
                        while (sdr.Read())
                        {
                            ProductoId.Add(new SelectListItem
                            {
                                Text = sdr["ProductoId"].ToString(),
                                Value = sdr["ProductoId"].ToString(),
                            });                            
                        }
                        con.Close();
                    }
                    else
                    {
                        ProductoId = null;
                    }
                }
                catch (Exception)
                {
                    con.Close();
                }
            }
            return (ProductoId);
        }
    }
}