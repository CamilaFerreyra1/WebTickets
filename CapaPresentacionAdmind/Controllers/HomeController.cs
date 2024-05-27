using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;



using CapaEntidad;
using CapaNegocio;
using ClosedXML.Excel;

namespace CapaPresentacionAdmin.Controllers
{

    [Authorize]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }


        public ActionResult Usuarios()
        {
            return View();
        }



        [HttpGet]
        public JsonResult ListarUsuarios()
        {


            List<Usuario> oLista = new List<Usuario>();

            oLista = new CN_Usuarios().Listar();


            return Json(new { data = oLista }, JsonRequestBehavior.AllowGet);

        }

        //[HttpGet]
        //public JsonResult ListarTickets()
        //{


        //    List<Ticket> oLista = new List<Ticket>();

        //    oLista = new CN_Ticket().ListarTicket();


        //    return Json(new { data = oLista }, JsonRequestBehavior.AllowGet);

        //}
        //---------------------------------------------------
        //       [HttpPost]
        //       public JsonResult GuardarTicket(Ticket objeto)
        //       {
        //           object resultado;
        //           string mensaje = string.Empty;

        //           if (objeto.IdTicket == 0)
        //           {
        // Lógica para guardar un nuevo ticket
        //               resultado = new CN_Ticket().RegistrarTicket(objeto, out mensaje);
        //           }
        //           else
        //           {
        // Lógica para editar un ticket existente
        //               resultado = new CN_Ticket().EditarTicket(objeto, out mensaje);
        //           }

        //           return Json(new { resultado = resultado, mensaje = mensaje }, JsonRequestBehavior.AllowGet);
        //       }

        //--------------------------------------------------------------------------
        [HttpPost]
        public JsonResult GuardarUsuario(Usuario objeto)
        {
            object resultado;
            string mensaje = string.Empty;

            if (objeto.IdUsuario == 0)
            {

                resultado = new CN_Usuarios().Registrar(objeto, out mensaje);
            }
            else
            {
                resultado = new CN_Usuarios().Editar(objeto, out mensaje);

            }

            return Json(new { resultado = resultado, mensaje = mensaje }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult EliminarUsuario(int id)
        {
            bool respuesta = false;
            string mensaje = string.Empty;

            respuesta = new CN_Usuarios().Eliminar(id, out mensaje);

            return Json(new { resultado = respuesta, mensaje = mensaje }, JsonRequestBehavior.AllowGet);
        }


    }
}