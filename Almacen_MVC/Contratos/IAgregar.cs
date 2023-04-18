using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Almacen_MVC.AlmacenMVC.Entities;

namespace Almacen_MVC.Contratos
{
    public interface IAgregar
    {
        Task<bool> RegistrarVenta(Venta Vnta);
    }
}
