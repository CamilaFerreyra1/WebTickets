using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidad
{
    public class Ticket
    {
        public int IdTicket { get; set; }
        public int IdCliente { get; set; }
        public string Asunto { get; set; }
        public string Descripcion { get; set; }
        public string Estado { get; set; }
        public string FechaCreacion { get; set; }
        public string FechaCierre { get; set; }
        public string  Prioridad { get; set; }
        public string Comentario { get; set; }

    }
}
