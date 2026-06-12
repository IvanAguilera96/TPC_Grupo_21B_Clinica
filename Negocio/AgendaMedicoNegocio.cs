using Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio;
using ConexionBD;

namespace Negocio
{
    public class AgendaMedicoNegocio
    {
        public List<AgendaMedico> ListarAgendaPorMedico(int IdMedico)
        {
            List<AgendaMedico> lista = new List<AgendaMedico>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("SELECT A.IdAgendaMedico, A.IdMedico, T.IdTurnoTrabajo, T.Descripcion AS NombreTurno, T.HoraEntrada, T.HoraSalida, T.DiaDeTrabajo, E.IdEspecialidad, E.Descripcion AS NombreEspecialidad FROM AgendaMedico A INNER JOIN TurnoTrabajo T ON A.IdTurnoTrabajo = T.IdTurnoTrabajo INNER JOIN especialidad E ON T.IdEspecialidad = E.IdEspecialidad WHERE A.IdMedico = @IdMedico");
                datos.setearParametros("@IdMedico", IdMedico);
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    AgendaMedico Agenda = new AgendaMedico();
                    Agenda.IdAgendaMedico = (int)datos.Lector["IdAgendaMedico"];

                    Agenda.Medico = new Medico();
                    Agenda.Medico.IdMedico = (int)datos.Lector["IdMedico"];

                    Agenda.TurnoTrabajo = new TurnoTrabajo();
                    Agenda.TurnoTrabajo.IdTurnoTrabajo = (int)datos.Lector["IdTurnoTrabajo"];
                    Agenda.TurnoTrabajo.Descripcion = (string)datos.Lector["NombreTurno"];
                    Agenda.TurnoTrabajo.HoraEntrada = (TimeSpan)datos.Lector["HoraEntrada"];
                    Agenda.TurnoTrabajo.HoraSalida = (TimeSpan)datos.Lector["HoraSalida"];
                    Agenda.TurnoTrabajo.DiaDeTrabajo = (string)datos.Lector["DiaDeTrabajo"];

                    Agenda.Especialidad = new Especialidad();
                    Agenda.Especialidad.IdEspecialidad = (int)datos.Lector["IdEspecialidad"];
                    Agenda.Especialidad.Descripcion = (string)datos.Lector["NombreEspecialidad"];

                    lista.Add(Agenda);
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

        } // ListarAgendaPorMedico
    }
}
