using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Almacen_MVC.AlmacenMVC.Entities;
using System.Web.Mvc;

namespace Almacen_MVC.Contratos
{
    public interface IVentas
    {
        Task<List<SelectListItem>> ObtenerProductoId();
        Task<Producto> ObtenerProducto(int id);

       

    }
}
