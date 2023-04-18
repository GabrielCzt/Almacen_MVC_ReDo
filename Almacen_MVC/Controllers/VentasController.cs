using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Xml.Schema;
using Almacen_MVC.AlmacenMVC.Entities;
using Almacen_MVC.Contratos;

namespace Almacen_MVC.Controllers
{
    public class VentasController : Controller
    {
        Producto producto;
        Venta venta;
        static double total;
        private readonly IVentas _ventas;
        private readonly IAgregar _agregar;
        public VentasController(IVentas ventas, IAgregar agregar)
        {
            _ventas = ventas;
            _agregar = agregar;
        }

        [Authorize(Roles ="Planeación, Vinculación")]
        public async Task<ActionResult> Ventas()
        {
            List<SelectListItem> ListIds = new List<SelectListItem>();
            ListIds = await _ventas.ObtenerProductoId();
            ViewData["Idsproductos"] = ListIds;
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

        public async Task<ActionResult> Ventas(string IdProducto, string Cantidad, string MayoreoMenudeo, bool Descuento, string Operacion)
        {

            producto = new Producto();
            venta = new Venta();


            int cantidadProd;
            List<SelectListItem> ListIds = new List<SelectListItem>();
            ListIds = await _ventas.ObtenerProductoId();
            ViewData["IdsProductos"] = ListIds;
            if (Operacion == "Calcular")
            {
                if(IdProducto != "")
                {
                    producto = await _ventas.ObtenerProducto(Convert.ToInt16(IdProducto));
                    ViewData["Descripcion"] = "Descripcion" + producto.Nombre + " " + producto.Presentacion;

                    try
                    {
                        cantidadProd = Convert.ToInt16(Cantidad);
                        if (cantidadProd < 0) throw new ArithmeticException();
                        else
                        {
                            if (string.IsNullOrEmpty(MayoreoMenudeo))
                            {
                                ViewData["Error"] = "Seleccione Precio de Mayoreo o Menudeo";
                            }
                            else
                            {
                                if(MayoreoMenudeo == "Mayoreo")
                                {
                                    total = producto.PMayoreo * cantidadProd;
                                    if (Descuento)
                                    {
                                        total = total - (total * 0.20);
                                    }

                                }
                                if(MayoreoMenudeo == "Menudeo")
                                {
                                    total = producto.PMenudeo * cantidadProd;
                                    if (Descuento)
                                    {
                                        total = total - (total * 0.20);
                                    }
                                }
                                
                            }
                        }
                        ViewData["TotalPago"] = total.ToString();
                        ViewBag.RegistrarVenta = true;

                    }
                    catch (FormatException)
                    {
                        ViewData["Error"] = "Capture una cantidad";
                    }
                    catch (ArithmeticException)
                    {
                        ViewData["Error"] = "Ingrese Cantidades Positivas";
                    }
                }
                else
                {
                    ViewData["Error"] = "Seleccione Id del producto";
                }
            }
            if(Operacion == "Registrar_Venta")
            {
                ViewBag.Modal = JavaScriptModal();
            }
            if(Operacion == "Registrar")
            {
                venta.ProductoId = Convert.ToInt16(IdProducto);
                venta.Cantidad = Convert.ToInt16(Cantidad);
                venta.TotalVenta = total;
                venta.Fecha = DateTime.Now;
                if(await _agregar.RegistrarVenta(venta))
                {
                    ViewData["TotalPago"] = total.ToString();
                    ViewData["Error"] = "La Venta se Registró exitosamente";
                }
                else
                {
                    ViewData["Error"] = "Ocurrió un Error al Registrar la Venta";
                }
            }
            return View();
            
        }
    }
}