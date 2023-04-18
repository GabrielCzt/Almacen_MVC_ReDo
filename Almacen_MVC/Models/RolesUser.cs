using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Almacen_MVC.Models
{
    public class RolesUser
    {
        readonly string dbconexion;
        private string Error = "";
        SqlConnection Cn;
        public RolesUser()
        {
            dbconexion = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

        }
        public DataTable ObtenerRoles(string id)
        {
            DataTable Dt = new DataTable();
            Cn = new SqlConnection(dbconexion);
            string consulta = "select ClaimValue from AspNetUserClaims where Userid='" + id + "'";
            SqlCommand dbcommand = new SqlCommand();
            dbcommand.CommandText = consulta;
            dbcommand.Connection = Cn;
            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = dbcommand;
            try
            {
                adapter.Fill(Dt);
            }
            catch (Exception e)
            {
                Error = e.Message;
            }
            dbcommand.Connection.Close();
            dbcommand.Dispose();
            adapter.Dispose();
            return Dt;
        }
    }
}