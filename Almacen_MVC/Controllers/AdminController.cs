using Almacen_MVC.AlmacenMVC.Entities;
using Almacen_MVC.Contratos;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace Almacen_MVC.Controllers
{
    public class AdminController : Controller
    {
        private readonly IAdmin _lstVentas;
        Producto producto;
        private readonly IAdmin _aprod;

        public AdminController(IAdmin lstVentas, IAdmin aprod)
        {
            _lstVentas = lstVentas;
            _aprod = aprod;
        }
        // GET: Admin
        [Authorize(Users = "user2@gmail.com", Roles ="Admin")]
        public ActionResult ListarVentas()
        {
            
            return View();

        }
        [Authorize(Roles ="Admin")]
        
        public ActionResult AgregarProducto()
        {
            return View();
        }

        public string JavaScriptModal()
        {
            StringBuilder CadenaToast = new StringBuilder();
            CadenaToast.Append("<script type=text/javascript>");
            CadenaToast.Append("let VModal = document.getElementById('PreguntaModal');");
            CadenaToast.Append("let bsVModal = new bootstrap.Modal(VModal, {focus:true});");
            CadenaToast.Append("bsVModal.show();");
            CadenaToast.Append("</script>");
            return CadenaToast.ToString();
        }

        [HttpPost]

        public async Task<ActionResult> AgregarProducto(string Nombre, string Presentacion, 
            string PMayoreo, string PMenudeo, string Existencia, string CostoUnitario, 
            string ImagenPath)
        {
            double _PMayoreo = Convert.ToDouble(PMayoreo);
            double _PMenudeo = Convert.ToDouble(PMenudeo);
            int _Existencia = int.Parse(Existencia);
            double _CostoUnitario = Convert.ToDouble(CostoUnitario);

            producto = new Producto();
            producto.Nombre = Nombre;
            producto.Presentacion = Presentacion;
            producto.PMayoreo = _PMayoreo;
            producto.PMenudeo = _PMenudeo;
            producto.Existencia = _Existencia;
            producto.CostoUnitario = _CostoUnitario;
            producto.ImagenPath = ImagenPath;
            if(await _aprod.AgregarProducto(producto)){
                ViewData["Error"] = "El producto se agregó correctamente";
            }
            else
            {
                ViewData["Error"] = "Algo salió mal";
            }
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> ListarVentas( DateTime? fecha)
        {
             if(!fecha.HasValue)
             {
                ModelState.AddModelError("error", "Debe ingresar una fecha");
             }
            if (ModelState.IsValid)
            {
                DateTime date;
                List<Venta> ListarVentas = new List<Venta>();
                date = (DateTime)fecha;
                ListarVentas = await _lstVentas.ObtenerProductoFecha(date);
                return View(ListarVentas);
            }
            else
            {
                return View();
            }
            

                     
        }
    }
}