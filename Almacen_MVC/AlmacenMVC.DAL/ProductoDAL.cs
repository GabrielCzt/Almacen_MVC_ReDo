using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using Almacen_MVC.AlmacenMVC.Entities;
using Almacen_MVC.Contratos;


namespace Almacen_MVC.AlmacenMVC.DAL
{
    public class ProductoDAL : IProducto
    {
        string dbconexion;

        public ProductoDAL()
        {
            dbconexion = ConfigurationManager.ConnectionStrings["ConectaProductos"].ConnectionString;
        }
        public async Task<List<SelectListItem>> ObtenerNombreProducto()
        {
            List<SelectListItem> NombreProducto = new List<SelectListItem>();
            using (SqlConnection con = new SqlConnection(dbconexion))
            {
                SqlCommand cmd = new SqlCommand("NombreProducto", con);
                cmd.CommandType = CommandType.StoredProcedure;
                try
                {
                    await con.OpenAsync();
                    SqlDataReader sdr = await cmd.ExecuteReaderAsync();
                    if (sdr.HasRows)
                    {
                        while (sdr.Read())
                        {
                            NombreProducto.Add(new SelectListItem
                            {
                                Text = sdr["Nombre"].ToString(),
                                Value = sdr["Nombre"].ToString(),

                            });
                        }
                        con.Close();
                    }
                    else
                    {
                        NombreProducto = null;
                    }
                }
                catch (Exception)
                {
                    con.Close();
                }
            }
            return (NombreProducto);
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
                            Pr.ProductoId = Convert.ToInt16(sdr["ProductoId"]);
                            Pr.Nombre = sdr["Nombre"].ToString();
                            Pr.Presentacion = sdr["Presentacion"].ToString();
                            Pr.CostoUnitario = Convert.ToDouble(sdr["CostoUnitario"]);
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
        public async Task<List<Producto>> ObtenerProductoPorNombre(string nombre)
        {
            List<Producto> ListProductos = new List<Producto>();
            using(SqlConnection con = new SqlConnection(dbconexion))
            {
                SqlCommand cmd = new SqlCommand("ObtenerProductoPorNombre", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@NombreProducto", nombre);
                try
                {
                    await con.OpenAsync();
                    SqlDataReader sdr = await cmd.ExecuteReaderAsync();
                    if (sdr.HasRows)
                    {
                        while (sdr.Read())
                        {
                            ListProductos.Add(new Producto
                            {
                                Nombre = sdr["Nombre"].ToString(),
                                Presentacion = sdr["Presentacion"].ToString(),
                                PMayoreo = Convert.ToDouble(sdr["PMayoreo"]),
                                PMenudeo = Convert.ToDouble(sdr["PMenudeo"]),
                                Existencia = Convert.ToInt16(sdr["Existencia"]),
                                CostoUnitario = Convert.ToDouble(sdr["CostoUnitario"]),


                            });

                        }
                        con.Close();
                    }
                    else
                    {
                        ListProductos = null;
                    }
                }
                catch (Exception)
                {
                    con.Close();
                }
                return (ListProductos);
            }
        }

            public async Task<List<Producto>> ObtenerProductos()
            {
                List<Producto> ListProductos = new List<Producto>();
                using (SqlConnection con = new SqlConnection(dbconexion))
                {
                    SqlCommand cmd = new SqlCommand("ObtenerProductos", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    try
                    {
                        await con.OpenAsync();
                        SqlDataReader sdr = await cmd.ExecuteReaderAsync();
                        if (sdr.HasRows)
                        {
                            while (sdr.Read())
                            {
                                ListProductos.Add(new Producto
                                {
                                    Nombre = sdr["Nombre"].ToString(),
                                    Presentacion = sdr["Presentacion"].ToString(),
                                    CostoUnitario = Convert.ToDouble(sdr["CostoUnitario"]),
                                    ImagenPath = sdr["ImagenPath"].ToString(),
                                });

                            }
                            con.Close();
                        }
                        else
                        {
                            ListProductos = null;
                        }
                    }
                    catch (Exception)
                    {
                        con.Close();
                    }
                    return (ListProductos);
                }
            }
    }
}
