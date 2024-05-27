using System;
using System.Collections.Generic;
using System.Web.Mvc;
using CapaEntidad;
using CapaNegocio;

namespace CapaPresentacionCliente.Controllers
{
    public class TicketController : Controller
    {
        // GET: Ticket


        public ActionResult RegistrarTicket()
        {
            return View();
        }

        public ActionResult Index()
        {
            //  var model = new Ticket();
            //  return View(model);
            return View();
        }

        ////[HttpGet]
        ////public JsonResult ListarTicket()
        ////{
        ////    try
        ////    {
        ////        // Obtener todos los tickets disponibles
        ////        List<Ticket> listaTickets = new CN_Ticket().ListarTicket();

        ////        return Json(new { data = listaTickets }, JsonRequestBehavior.AllowGet);
        ////    }
        ////    catch (Exception ex)
        ////    {
        ////        // Manejo de errores
        ////        return Json(new { success = false, message = ex.Message }, JsonRequestBehavior.AllowGet);
        ////    }
        ////}
        ///


        [HttpGet]
        public JsonResult ListarTicket()
        {


            List<Ticket> oLista = new List<Ticket>();

            oLista = new CN_Ticket().ListarTicket();


            return Json(new { data = oLista }, JsonRequestBehavior.AllowGet);

        }


        //[HttpGet]
        //public JsonResult ListarTicket()
        //{
        //    try
        //    {
        //        List<Ticket> listaTickets = new CN_Ticket().ListarTicket();
        //        return Json(new { data = listaTickets }, JsonRequestBehavior.AllowGet);
        //    }
        //    catch (Exception ex)
        //    {
        //        return Json(new { success = false, message = ex.Message }, JsonRequestBehavior.AllowGet);
        //    }
        //}

        [HttpPost]
        public JsonResult GuardarTicket(Ticket objeto)
        {
            try
            {
                object resultado;
                string mensaje = string.Empty;

                if (objeto.IdTicket == 0)
                {
                    resultado = new CN_Ticket().RegistrarTicket(objeto, out mensaje);
                   
                }
                else
                {
                    resultado = new CN_Ticket().EditarTicket(objeto, out mensaje);
                }

                return Json(new { success = true, resultado = resultado, mensaje = mensaje }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

       
    }


    namespace Models
    {
        public class TicketModel
        {
            public int IdCliente { get; set; }
            public string Asunto { get; set; }
            public string Descripcion { get; set; }
            public string Prioridad { get; set; }
            public string Comentario { get; set; }
        }
    }

}

