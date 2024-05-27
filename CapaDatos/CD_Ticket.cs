using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CapaEntidad;

using System.Data.SqlClient;
using System.Data;

namespace CapaDatos
{
    public class CD_Ticket
    {

        public List<Ticket> ListarTickets()
        {
            List<Ticket> lista = new List<Ticket>();

            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cn))
                {
                    string query = "SELECT IdTicket, IdCliente, Asunto, Descripcion, Estado, FechaCreacion, FechaCierre, Prioridad, Comentario FROM Ticket";

                    SqlCommand cmd = new SqlCommand(query, oconexion);
                    cmd.CommandType = CommandType.Text;

                    oconexion.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(
                                new Ticket()
                                {
                                    IdTicket = Convert.ToInt32(dr["IdTicket"]),
                                    IdCliente = Convert.ToInt32(dr["IdCliente"]),
                                    Asunto = dr["Asunto"].ToString(),
                                    Descripcion = dr["Descripcion"].ToString(),
                                    Estado = dr["Estado"].ToString(),
                                    FechaCreacion = dr["FechaCreacion"].ToString(),
                                    FechaCierre = dr["FechaCierre"].ToString(),
                                    Prioridad = dr["Prioridad"].ToString(),
                                    Comentario = dr["Comentario"].ToString()
                                }
                            );
                        }
                    }
                }
            }
            catch
            {
                lista = new List<Ticket>();
            }

            return lista;
        }


        public int RegistrarTicket(Ticket obj, out string mensaje)
        {
            int idTicketGenerado = 0;
            mensaje = string.Empty;
           
            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cn))
                {
                    SqlCommand cmd = new SqlCommand("sp_RegistrarTicket", oconexion);
                    cmd.Parameters.AddWithValue("IdTicket", obj.IdCliente);
                    cmd.Parameters.AddWithValue("IdCliente", obj.IdCliente);
                    cmd.Parameters.AddWithValue("Asunto", obj.Asunto);
                    cmd.Parameters.AddWithValue("Descripcion", obj.Descripcion);
                    cmd.Parameters.AddWithValue("Estado", obj.Estado);
                    cmd.Parameters.AddWithValue("Estado", "Pendiente");
                    cmd.Parameters.AddWithValue("FechaCreacion", obj.FechaCreacion);
                    cmd.Parameters.AddWithValue("FechaCreacion", DateTime.Now);
                    cmd.Parameters.AddWithValue("Prioridad", obj.Prioridad);
                    cmd.Parameters.AddWithValue("Comentario",obj.Comentario); // Nuevo parámetro
                    cmd.Parameters.Add("IdTicket", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("Mensaje", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.StoredProcedure;

                    oconexion.Open();

                    cmd.ExecuteNonQuery();

                    idTicketGenerado = Convert.ToInt32(cmd.Parameters["IdTicket"].Value);
                    mensaje = cmd.Parameters["Mensaje"].Value.ToString();
                }
            }
            catch (Exception ex)
            {
                idTicketGenerado = 0;
                mensaje = ex.Message;
            }

            return idTicketGenerado;
        }



        public bool EditarTicket(Ticket obj, out string Mensaje)
        {
            bool resultado = false;
            Mensaje = string.Empty;
            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cn))
                {
                    SqlCommand cmd = new SqlCommand("sp_EditarTicket", oconexion);
                    cmd.Parameters.AddWithValue("IdTicket", obj.IdTicket);
                    if (obj.FechaCierre != null)
                    {
                        cmd.Parameters.AddWithValue("FechaCierre", obj.FechaCierre);
                    }
                    //  cmd.Parameters.AddWithValue("IdCliente", obj.Prioridad);
                    cmd.Parameters.AddWithValue("Prioridad", obj.Prioridad);
                    cmd.Parameters.AddWithValue("Descripcion", obj.Descripcion);
                    cmd.Parameters.AddWithValue("Estado", obj.Estado);
                    cmd.Parameters.AddWithValue("Asunto", obj.Asunto);
                  //  cmd.Parameters.AddWithValue("FechaCierre", obj.FechaCierre);
                    cmd.Parameters.AddWithValue("Comentario", obj.Comentario);

                   // cmd.Parameters.Add("Resultado", SqlDbType.Bit).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("Mensaje", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.StoredProcedure;

                    oconexion.Open();

                    cmd.ExecuteNonQuery();

                   // resultado = Convert.ToBoolean(cmd.Parameters["Resultado"].Value);
                    Mensaje = cmd.Parameters["Mensaje"].Value.ToString();
                }

            }
            catch (Exception ex)
            {
                resultado = false;
                Mensaje = ex.Message;
            }
            return resultado;
        }

          public bool EliminarTicket(int id, out string Mensaje)
        {
            bool resultado = false;
            Mensaje = string.Empty;
            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cn))
                {
                    SqlCommand cmd = new SqlCommand("delete top (1) from Ticket where IdTicket = @id", oconexion);
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.CommandType = CommandType.Text;
                    oconexion.Open();
                    resultado = cmd.ExecuteNonQuery() > 0 ? true : false;
                }
            }
            catch (Exception ex)
            {
                resultado = false;
                Mensaje = ex.Message;
            }
            return resultado;
        }
    }
}
