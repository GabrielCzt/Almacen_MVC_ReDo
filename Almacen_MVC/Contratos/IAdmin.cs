using Almacen_MVC.AlmacenMVC.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Almacen_MVC.Contratos
{
    public interface IAdmin
    {
        Task<List<Venta>> ObtenerProductoFecha(DateTime fecha);
        Task<bool> AgregarProducto(Producto prd);
    }
}
