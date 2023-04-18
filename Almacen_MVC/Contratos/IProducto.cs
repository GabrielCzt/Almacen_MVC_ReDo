using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Almacen_MVC.AlmacenMVC.Entities;


namespace Almacen_MVC.Contratos
{
    public interface IProducto
    {
        Task<List<Producto>> ObtenerProductos();
        Task<Producto> ObtenerProducto(int id);
        Task<List<SelectListItem>> ObtenerNombreProducto();
        Task<List<Producto>> ObtenerProductoPorNombre(string nombre);

    }
}
