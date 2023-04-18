using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Almacen_MVC.AlmacenMVC.Entities
{
    public class Venta
    {
        public int VentaId { set; get; }
        public int ProductoId { set; get; }
        public int Cantidad { set; get; }
        public double TotalVenta { set; get; }        
        public DateTime Fecha { set; get; }

    }
}