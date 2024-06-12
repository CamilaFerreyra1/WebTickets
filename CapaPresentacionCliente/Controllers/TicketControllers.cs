using System;
using System.Collections.Generic;
using System.Web.Mvc;
using CapaEntidad;
using CapaNegocio;

namespace CapaPresentacionCliente.Controllers
{
    [Authorize]
    public class TicketController : Controller
    {
        // GET: Ticket


        public ActionResult RegistrarTicket()
        {
            return View();
        }

        public ActionResult Index()
        {
            return View();
        }



        [HttpGet]
        public JsonResult ListarTickets()
        {


           List<Ticket> listaTickets = new List<Ticket>();

            listaTickets = new CN_Ticket().ListarTickets();

            return Json(new { data = listaTickets }, JsonRequestBehavior.AllowGet);


        }


        //[HttpGet]
        //public JsonResult ListarTickets(DateTime? fechainicio, DateTime? fechafin, int? idtransaccion)
        //{
        //    try
        //    {
        //        List<Ticket> listaTickets = new CN_Ticket().ListarTickets(); // Puedes agregar filtros aquí según los parámetros
        //        return Json(new { data = listaTickets }, JsonRequestBehavior.AllowGet);
        //    }
        //    catch (Exception ex)
        //    {
        //        return Json(new { success = false, message = ex.Message }, JsonRequestBehavior.AllowGet);
        //    }
        //}


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

        //    [HttpPost]
        //    public JsonResult GuardarTicket(Ticket objeto)
        //    {
        //        try
        //        {
        //            object resultado;
        //            string mensaje = string.Empty;

        //            if (objeto.IdTicket == 0)
        //            {
        //                resultado = new CN_Ticket().RegistrarTicket(objeto, out mensaje);

        //            }
        //            else
        //            {
        //                resultado = new CN_Ticket().EditarTicket(objeto, out mensaje);
        //            }

        //            return Json(new { success = true, resultado = resultado, mensaje = mensaje }, JsonRequestBehavior.AllowGet);
        //        }
        //        //catch (Exception ex)
        //        //{
        //        //    return Json(new { success = false, message = ex.Message }, JsonRequestBehavior.AllowGet);
        //        //}
        //    }


        //}

        [HttpPost]
        public JsonResult GuardarTicket(Ticket objeto)
        {
            object resultado;
            string mensaje = string.Empty;

            System.Diagnostics.Debug.WriteLine("IdTicket: " + objeto.IdTicket);

            if (objeto.IdTicket == 0)
            {
                resultado = new CN_Ticket().RegistrarTicket(objeto, out mensaje);
            }
            else
            {
                resultado = new CN_Ticket().EditarTicket(objeto, out mensaje);
            }

            return Json(new { resultado = resultado, mensaje = mensaje }, JsonRequestBehavior.AllowGet);
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

