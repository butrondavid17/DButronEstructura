using Negocio;
using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Presentacion.Controllers
{
    public class EmpleadoController : Controller
    {
        // GET: Empleado
        public ActionResult GetAll(Negocio.Empleado empleado)
        {
            if (empleado.Nombre == null)
            {
                empleado.Nombre = "";
            }
            Dictionary<string, object> objeto = Negocio.Empleado.GetAll(empleado);
            bool resultado = (bool)objeto["Resultado"];
            if (resultado)
            {
                empleado = (Empleado)objeto["Empleado"];
                return View(empleado);
            }
            else
            {
                string excepcion = (string)objeto["Excepcion"];
                return View(excepcion);
            }
        }
        public ActionResult Delete(int EmpleadoID)
        {
            Dictionary<string, object> objeto = Negocio.Empleado.Delete(EmpleadoID);
            bool resultado = (bool)objeto["Resultado"];
            if (resultado)
            {
                ViewBag.Mensaje = "El Empleado ha sido eliminado";
                return PartialView("Modal");
            }
            else
            {
                string excepcion = (string)objeto["Excepcion"];
                ViewBag.Mensaje = "Ocurrio un error al intentar eliminar el empleado";
                return PartialView("Modal");
            }
        }
    }
}