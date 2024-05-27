using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CapaDatos;
using CapaEntidad;

namespace CapaNegocio
{
    public class CN_Ticket
    {

        private CD_Ticket objCapaDato = new CD_Ticket();

        public List<Ticket> ListarTicket()
        {
            return objCapaDato.ListarTickets();
        }

        public object RegistrarTicket(Ticket obj, out string mensaje)
        {
            mensaje = string.Empty;

            // Validar los datos del ticket
            if (string.IsNullOrEmpty(obj.Asunto) || string.IsNullOrWhiteSpace(obj.Asunto))
            {
                mensaje = "El asunto debe completarse";
                return null;
            }
            else if (string.IsNullOrEmpty(obj.Descripcion) || string.IsNullOrWhiteSpace(obj.Descripcion))
            {
                mensaje = "La descripción debe completarse";
                return null;
            }

            else if (string.IsNullOrEmpty(obj.Prioridad) || string.IsNullOrWhiteSpace(obj.Prioridad))
            {
                mensaje = "La Prioridad debe completarse";
                return null;
            }

            if (string.IsNullOrEmpty(mensaje))
            {
              //  obj.Clave = CN_Recursos.ConvertirSha256(obj.Clave);
                return objCapaDato.RegistrarTicket(obj, out mensaje);

            }
            else
            {

                return 0;
            }
            //   return "Envio Exitoso";
        }






        public bool EditarTicket(Ticket obj, out string Mensaje)
        {

            Mensaje = string.Empty;

             if (string.IsNullOrEmpty(obj.Estado) || string.IsNullOrWhiteSpace(obj.Estado))
           // if (!string.IsNullOrEmpty(obj.Estado))
            {
                Mensaje = "El Estado debe completarse";
            }
             else if (string.IsNullOrEmpty(obj.Comentario) || string.IsNullOrWhiteSpace(obj.Comentario))
            //if (!string.IsNullOrEmpty(obj Comentario))
            {
                Mensaje = "El comentario no puede estar vacio";
            }

            if (string.IsNullOrEmpty(Mensaje))
            {

                return objCapaDato.EditarTicket(obj, out Mensaje);
            }
            else
            {
                return false;
            }
        }

          public bool EliminarTicket(int id, out string Mensaje)
        {

            return objCapaDato.EliminarTicket(id, out Mensaje);
        }
    }
}