using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Almacen_MVC.AlmacenMVC.Entities;
using Almacen_MVC.Contratos;
using System.Threading.Tasks;

namespace Almacen_MVC.Controllers
{
    public class ConsultasController : Controller
    {
        // GET: Consultas
        private readonly IProducto _producto;

        public ConsultasController(IProducto producto)
        {
            _producto = producto;
        }

        [Authorize(Roles = "RecHumanos")]
        public async Task<ActionResult> Listar()
        {
            List<Producto> ListProductos = new List<Producto>();
            ListProductos = await _producto.ObtenerProductos();
            return View(ListProductos);
        }

        [Authorize]
        public async Task<ActionResult> ListarPorNombre()
        {
            List<SelectListItem> NombreProd = new List<SelectListItem>();
            NombreProd = await _producto.ObtenerNombreProducto();
            ViewData["NombreProductos"] = NombreProd;
            return View();

        }

        [Authorize(Users ="user3@gmail.com")]
        public ActionResult ListarPorId()
        {         
            return View();
        }


        [HttpPost]

        public async Task<ActionResult> ListarPorNombre(string Nombre)
        {
            List<SelectListItem> NombreProd = new List<SelectListItem>();
            List<Producto> ListProductos = new List<Producto>();
            NombreProd = await _producto.ObtenerNombreProducto();
            ViewData["NombreProductos"] = NombreProd;
            ListProductos = await _producto.ObtenerProductoPorNombre(Nombre);
            return View(ListProductos);
        }
        [HttpPost]
        public async Task<ActionResult> ListarPorId(string id)
        {
            int _id;
            
            if (id != "" )
            {
                if(int.TryParse(id, out _id))
                {
                    _id = int.Parse(id);
                    if (_id > 0)
                    {
                        
                        Producto _Producto = new Producto();                        
                        _Producto = await _producto.ObtenerProducto(_id);
                        if(_Producto==null)
                        {
                            ModelState.AddModelError("error", "No existen productos para ese ID");
                            return View();
                        }
                        else
                        {
                            return View(_Producto);
                        }
                        
                    }
                    else
                    {
                        ModelState.AddModelError("error", "El id debe ser un número positivo");
                        return View();
                    }
                }
                else
                {
                    ModelState.AddModelError("error", "El id debe ser un número entero");
                    return View();
                }
                
                
            }
            else
            {
                ModelState.AddModelError("error", "Debe ingresar un id");
                return View();
            }
            
            
        }

        public ActionResult Error401()
        {
            return View();
        }
    }
}