using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using CapaEntidad;
using CapaNegocio;
using Newtonsoft.Json;



namespace CapaPresentacionAdmin.Controllers
{
    //   [Authorize(Roles = "Administrador")]
    [Authorize]
    public class MantenedorController : Controller
    {
        // GET: Mantenedor/Tickets
        public ActionResult Tickets()
        {
            return View();
        }

        [HttpGet]
        public JsonResult ListarTickets()
        {
            List<Ticket> listaTickets = new List<Ticket>();

            listaTickets = new CN_Ticket().ListarTicket();

            return Json(new { data = listaTickets }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
               public JsonResult GuardarTicket(Ticket objeto)
               {
                   object resultado;
                   string mensaje = string.Empty;

            System.Diagnostics.Debug.WriteLine("IdTicket: " + objeto.IdTicket);

            if (objeto.IdTicket == 0)
                   {
        // Lógica para guardar un nuevo ticket
                       resultado = new CN_Ticket().RegistrarTicket(objeto, out mensaje);
                   }
                   else
                   {
        // Lógica para editar un ticket existente
                       resultado = new CN_Ticket().EditarTicket(objeto, out mensaje);
                   }

                   return Json(new { resultado = resultado, mensaje = mensaje }, JsonRequestBehavior.AllowGet);
              }
    

    [HttpPost]
        public JsonResult EliminarTicket(int id)
        {
            bool respuesta = false;
            string mensaje = string.Empty;

            respuesta = new CN_Ticket().EliminarTicket(id, out mensaje);

            return Json(new { resultado = respuesta, mensaje = mensaje }, JsonRequestBehavior.AllowGet);
        }
    }
}


