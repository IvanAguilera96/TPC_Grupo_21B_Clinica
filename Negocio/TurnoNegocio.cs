using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio;
using ConexionBD;

namespace Negocio
{
    public class TurnoNegocio
    {
        public List<Turno> ListarConFiltros(int IdMedico, int IdEspecialidad, string Fecha)
        {
            List<Turno> lista = new List<Turno>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                // Query base con todos los INNER JOIN correspondientes
                string Query = @"
                    SELECT 
                        T.IdTurno, T.Fecha, T.Hora, T.Observacion, T.Diagnostico,
                        P.IdPaciente, P.Nombre AS NombrePaciente, P.Apellido AS ApellidoPaciente,
                        E.IdEspecialidad, E.Descripcion AS NombreEspecialidad,
                        M.IdMedico, M.Nombre AS NombreMedico, M.Apellido AS ApellidoMedico,
                        EST.IdEstadoTurno, EST.Descripcion AS NombreEstado,
                        A.IdAgendaMedico
                    FROM Turno T
                    INNER JOIN Paciente P ON T.IdPaciente = P.IdPaciente
                    INNER JOIN AgendaMedico A ON T.IdAgendaMedico = A.IdAgendaMedico
                    INNER JOIN Medico M ON A.IdMedico = M.IdMedico
                    INNER JOIN Especialidad E ON A.IdEspecialidad = E.IdEspecialidad
                    INNER JOIN EstadoTurno EST ON T.IdEstadoTurno = EST.IdEstadoTurno";

                // Revisamos los filtros
                if (IdMedico > 0)
                {
                    Query += " AND M.IdMedico = @IdMedico";
                    datos.setearParametros("@IdMedico", IdMedico);
                }

                if (IdEspecialidad > 0)
                {
                    Query += " AND E.IdEspecialidad = @IdEspecialidad";
                    datos.setearParametros("@IdEspecialidad", IdEspecialidad);
                }

                if (!string.IsNullOrEmpty(Fecha))
                {
                    Query += " AND T.Fecha = @Fecha";
                    datos.setearParametros("@Fecha", Fecha);
                }

                // Ordenamos para que muestre primero los turnos mas recientes
                Query += " ORDER BY T.Fecha DESC, T.Hora ASC";

                datos.setearConsulta(Query);
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Turno aux = new Turno();

                    // Datos del Turno
                    aux.IdTurno = (int)datos.Lector["IdTurno"];
                    aux.Fecha = (DateTime)datos.Lector["Fecha"];
                    aux.Hora = (TimeSpan)datos.Lector["Hora"];

                    // Manejo de nulos para observaciones y diagnosticos
                    aux.Observacion = datos.Lector["Observacion"] != DBNull.Value ? (string)datos.Lector["Observacion"] : "";
                    aux.Diagnostico = datos.Lector["Diagnostico"] != DBNull.Value ? (string)datos.Lector["Diagnostico"] : "";

                    aux.Paciente = new Paciente();
                    aux.Paciente.IdPaciente = (int)datos.Lector["IdPaciente"];
                    aux.Paciente.Nombre = (string)datos.Lector["NombrePaciente"];
                    aux.Paciente.Apellido = (string)datos.Lector["ApellidoPaciente"];

                    aux.Estado = new EstadoTurno();
                    aux.Estado.IdEstado = (int)datos.Lector["IdEstadoTurno"];
                    aux.Estado.Descripcion = (string)datos.Lector["NombreEstado"];

                    aux.Agenda = new AgendaMedico();
                    aux.Agenda.IdAgendaMedico = (int)datos.Lector["IdAgendaMedico"];

                    aux.Agenda.Medico = new Medico();
                    aux.Agenda.Medico.IdMedico = (int)datos.Lector["IdMedico"];
                    aux.Agenda.Medico.Nombre = (string)datos.Lector["NombreMedico"];
                    aux.Agenda.Medico.Apellido = (string)datos.Lector["ApellidoMedico"];

                    aux.Agenda.Especialidad = new Especialidad();
                    aux.Agenda.Especialidad.IdEspecialidad = (int)datos.Lector["IdEspecialidad"];
                    aux.Agenda.Especialidad.Descripcion = (string)datos.Lector["NombreEspecialidad"];

                    lista.Add(aux);
                }

                return lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                datos.cerrarConexion();
            }
        } // Listar Con Filtro

        public void CambiarEstado(int IdTurno, int IdEstadoCancelado)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("UPDATE turno SET IdEstadoTurno = @IdEstadoCancelado WHERE IdTurno = @IdTurno");
                datos.setearParametros("@IdEstadoCancelado", IdEstadoCancelado);
                datos.setearParametros("@IdTurno", IdTurno);
                datos.ejecutarAccion();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                datos.cerrarConexion();
            }
        } // Cambiar Estado

        public void Agregar(Turno nuevo)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta(@"
                                        INSERT INTO Turno (Fecha, Hora, IdAgendaMedico, IdPaciente, IdEstadoTurno, Observacion) 
                                        VALUES (@Fecha, @Hora, @IdAgenda, @IdPaciente, @IdEstado, @Observacion)");

                datos.setearParametros("@Fecha", nuevo.Fecha);
                datos.setearParametros("@Hora", nuevo.Hora);
                datos.setearParametros("@IdAgenda", nuevo.Agenda.IdAgendaMedico);
                datos.setearParametros("@IdPaciente", nuevo.Paciente.IdPaciente);

                // Al ser un alta, por defecto pasamos ID correspondiente a "Asignado" 2
                datos.setearParametros("@IdEstado", 2);

                datos.setearParametros("@Observacion", nuevo.Observacion ?? (object)DBNull.Value);

                datos.ejecutarAccion();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                datos.cerrarConexion();
            }
        } // Agregar

        public List<string> ObtenerHorasOcupadas(int idMedico, string fecha)
        {
            List<string> ocupadas = new List<string>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                // Buscamos las horas de los turnos de este médico que NO estén cancelados
                datos.setearConsulta(@"
                        SELECT T.Hora 
                        FROM Turno T
                            INNER JOIN AgendaMedico A ON T.IdAgendaMedico = A.IdAgendaMedico
                        WHERE A.IdMedico = @IdMedico 
                            AND T.Fecha = @Fecha
                            AND T.IdEstadoTurno <> 1 AND T.IdEstadoTurno <> 4 "); // Supongamos que 1 es "Cancelado". Si está cancelado, la hora se libera.

                datos.setearParametros("@IdMedico", idMedico);
                datos.setearParametros("@Fecha", fecha);
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    // Convertimos el TimeSpan de la BD a string estructurado "hh:mm:ss" para mapear directo con el slot
                    TimeSpan hora = (TimeSpan)datos.Lector["Hora"];
                    ocupadas.Add(hora.ToString(@"hh\:mm\:ss"));
                }

                return ocupadas;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                datos.cerrarConexion();
            }
        } // ObtenerHorasOcupadas

        public Turno BuscarPorId(int id)
        {
            AccesoDatos datos = new AccesoDatos();
            Turno aux = null;

            try
            {
                //  Query unificada: Traemos Turno, Paciente, Estado, Agenda y Especialidad de una
                string query = @"
            SELECT 
                T.IdTurno, T.Fecha, T.Hora, T.Observacion, T.Diagnostico,
                T.IdPaciente, P.Nombre AS NombrePaciente, P.Apellido AS ApellidoPaciente,
                T.IdEstadoTurno, ET.Descripcion AS NombreEstado,
                T.IdAgendaMedico,
                AM.IdMedico, AM.IdEspecialidad,
                ESP.Descripcion AS NombreEspecialidad
            FROM Turno T
            INNER JOIN Paciente P ON T.IdPaciente = P.IdPaciente
            INNER JOIN EstadoTurno ET ON T.IdEstadoTurno = ET.IdEstadoTurno
            INNER JOIN AgendaMedico AM ON T.IdAgendaMedico = AM.IdAgendaMedico
            INNER JOIN Especialidad ESP ON AM.IdEspecialidad = ESP.IdEspecialidad
            WHERE T.IdTurno = @id";

                datos.setearConsulta(query);
                datos.setearParametros("@id", id);
                datos.ejecutarLectura();

                if (datos.Lector.Read())
                {
                    aux = new Turno();
                    aux.IdTurno = (int)datos.Lector["IdTurno"];
                    aux.Fecha = (DateTime)datos.Lector["Fecha"];
                    aux.Hora = (TimeSpan)datos.Lector["Hora"];

                    aux.Observacion = datos.Lector["Observacion"] != DBNull.Value ? (string)datos.Lector["Observacion"] : "";
                    aux.Diagnostico = datos.Lector["Diagnostico"] != DBNull.Value ? (string)datos.Lector["Diagnostico"] : "";

                    // Mapeamos el Paciente
                    aux.Paciente = new Paciente();
                    aux.Paciente.IdPaciente = (int)datos.Lector["IdPaciente"];
                    aux.Paciente.Nombre = (string)datos.Lector["NombrePaciente"];
                    aux.Paciente.Apellido = (string)datos.Lector["ApellidoPaciente"];

                    // Mapeamos el Estado
                    aux.Estado = new EstadoTurno();
                    aux.Estado.IdEstado = (int)datos.Lector["IdEstadoTurno"];
                    aux.Estado.Descripcion = (string)datos.Lector["NombreEstado"];

                    // Mapeamos la Agenda COMPLETA con su Especialidad adentro
                    aux.Agenda = new AgendaMedico();
                    aux.Agenda.IdAgendaMedico = (int)datos.Lector["IdAgendaMedico"];

                    aux.Agenda.Medico = new Medico();
                    aux.Agenda.Medico.IdMedico = (int)datos.Lector["IdMedico"];

                    // Creamos el objeto Especialidad dentro de la Agenda para que no de NullReference
                    aux.Agenda.Especialidad = new Especialidad();
                    aux.Agenda.Especialidad.IdEspecialidad = (int)datos.Lector["IdEspecialidad"];
                    aux.Agenda.Especialidad.Descripcion = (string)datos.Lector["NombreEspecialidad"];
                }

                return aux;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                datos.cerrarConexion();
            }
        } // BuscarPorId

        public void ActualizarDiagnostico(int IdTurno, string Diagnostico)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("UPDATE Turno SET Diagnostico = @Diagnostico WHERE IdTurno = @IdTurno");
                datos.setearParametros("@Diagnostico", Diagnostico);
                datos.setearParametros("@IdTurno", IdTurno);
                datos.ejecutarAccion();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                datos.cerrarConexion();
            }
        } // Actualizar Diagnostico
    }
}
