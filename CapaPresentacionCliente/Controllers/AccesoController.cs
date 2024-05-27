using CapaEntidad;
using CapaNegocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace CapaPresentacionCliente.Controllers
{
    public class AccesoController : Controller
    {
        // GET: Acceso
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Registrar()
        {
            return View();
        }
        //public ActionResult RegistrarTicket()
        //{
        //    return View();
        //}
        public ActionResult Restablecer()
        {
            return View();
        }
        public ActionResult CambiarClave()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Registrar(Cliente objeto)
        {
            int resultado;
            string mensaje = string.Empty;

            ViewData["Nombre"] = string.IsNullOrEmpty(objeto.Nombre) ? "" : objeto.Nombre;
            ViewData["Apellidos"] = string.IsNullOrEmpty(objeto.Apellidos) ? "" : objeto.Apellidos;
            ViewData["Correo"] = string.IsNullOrEmpty(objeto.Correo) ? "" : objeto.Correo;

            if (objeto.Clave != objeto.ConfirmarClave)
            {

                ViewBag.Error = "Las contraseñas no coinciden";
                return View();
            }


            resultado = new CN_Cliente().Registrar(objeto, out mensaje);

            if (resultado > 0)
            {
                ViewBag.Error = null;
                return RedirectToAction("Index", "Acceso");
            }
            else
            {
                ViewBag.Error = mensaje;
                return View();
            }

        }

        [HttpPost]
        public ActionResult RegistrarTicket(Ticket objeto)
        {
            int resultado;
            string mensaje = string.Empty;

           // ViewData["Prioridad"] = string.IsNullOrEmpty(objeto.Prioridad) ? "" : objeto.Prioridad;
            ViewData["Asunto"] = string.IsNullOrEmpty(objeto.Asunto) ? "" : objeto.Asunto;
            ViewData["Descripcion"] = string.IsNullOrEmpty(objeto.Descripcion) ? "" : objeto.Descripcion;
            ViewData["Comentario"] = string.IsNullOrEmpty(objeto.Comentario) ? "" : objeto.Comentario;

            List<string> prioridades = new List<string>() { "Alta", "Media", "Baja" };
            ViewBag.Prioridades = new SelectList(prioridades);


            resultado = (int)new CN_Ticket().RegistrarTicket(objeto, out mensaje);

            if (resultado > 0)
            {
                ViewBag.Error = null;
                return RedirectToAction("Index", "Acceso");
            }
            else
            {
                ViewBag.Error = mensaje;
                return View();
            }

        }




        [HttpPost]
        public ActionResult Index(string correo, string clave)
        {
            Cliente oCliente = null;

            oCliente = new CN_Cliente().Listar().Where(item => item.Correo == correo && item.Clave == CN_Recursos.ConvertirSha256(clave)).FirstOrDefault();


            if (oCliente == null)
            {
                ViewBag.Error = "Correo o contraseña no son correctas";
                return View();

            }
            else
            {

                if (oCliente.Restablecer)
                {
                    TempData["IdCliente"] = oCliente.IdCliente;
                    return RedirectToAction("CambiarClave", "Acceso");
                }
                else
                {

                    FormsAuthentication.SetAuthCookie(oCliente.Correo, false);

                    Session["Cliente"] = oCliente;

                    ViewBag.Error = null;
                    return RedirectToAction("Index", "Ticket");


                }

            }
        }


        [HttpPost]
        public ActionResult Restablecer(string correo)
        {


            Cliente cliente = new Cliente();

            cliente = new CN_Cliente().Listar().Where(item => item.Correo == correo).FirstOrDefault();

            if (cliente == null)
            {
                ViewBag.Error = "No se encontro un cliente relacionado a ese correo";
                return View();
            }


            string mensaje = string.Empty;
            bool respuesta = new CN_Cliente().RestablecerClave(cliente.IdCliente, correo, out mensaje);

            if (respuesta)
            {

                ViewBag.Error = null;
                return RedirectToAction("Index", "Acceso");

            }
            else
            {

                ViewBag.Error = mensaje;
                return View();
            }


        }

        [HttpPost]
        public ActionResult CambiarClave(string idcliente, string claveactual, string nuevaclave, string confirmaclave)
        {

            Cliente oCliente = new Cliente();

            oCliente = new CN_Cliente().Listar().Where(u => u.IdCliente == int.Parse(idcliente)).FirstOrDefault();

            if (oCliente.Clave != CN_Recursos.ConvertirSha256(claveactual))
            {
                TempData["IdCliente"] = idcliente;
                ViewData["vclave"] = "";
                ViewBag.Error = "La contraseña actual no es correcta";
                return View();
            }
            else if (nuevaclave != confirmaclave)
            {

                TempData["IdCliente"] = idcliente;
                ViewData["vclave"] = claveactual;
                ViewBag.Error = "Las contraseñas no coinciden";
                return View();
            }
            ViewData["vclave"] = "";


            nuevaclave = CN_Recursos.ConvertirSha256(nuevaclave);


            string mensaje = string.Empty;

            bool respuesta = new CN_Cliente().CambiarClave(int.Parse(idcliente), nuevaclave, out mensaje);

            if (respuesta)
            {

                return RedirectToAction("Index");
            }
            else
            {

                TempData["IdCliente"] = idcliente;

                ViewBag.Error = mensaje;
                return View();
            }

        }



        public ActionResult CerrarSesion()
        {
            Session["Cliente"] = null;
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Acceso");

        }
    }
}