using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Almacen_MVC.AlmacenMVC.Entities
{
    public class Producto
    {
        public int ProductoId { set; get; }
        public string Nombre { set; get; }
        public string Presentacion { set; get; }
        public double PMayoreo { set; get; }
        public double PMenudeo { set; get; }
        public int Existencia { set; get; }
        public double CostoUnitario { set; get; }
        public string ImagenPath { set; get; }
    }
}