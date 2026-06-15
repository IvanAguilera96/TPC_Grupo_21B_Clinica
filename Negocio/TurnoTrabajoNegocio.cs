using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio;
using ConexionBD;

namespace Negocio
{
    public class TurnoTrabajoNegocio
    {
        public List<TurnoTrabajo> Listar()
        {
            AccesoDatos datos = new AccesoDatos();
            List<TurnoTrabajo> lista = new List<TurnoTrabajo>();
            try
            {
                datos.setearConsulta("SELECT IdTurnoTrabajo,  DiaDeTrabajo + ' ' + CAST(HoraEntrada AS varchar(5)) + ' a ' + CAST(HoraSalida AS varchar(5)) AS Descripcion FROM TurnoTrabajo");
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    TurnoTrabajo aux = new TurnoTrabajo();
                    aux.IdTurnoTrabajo = (int)datos.Lector["IdTurnoTrabajo"];
                    aux.Descripcion = (string)datos.Lector["Descripcion"];
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
        }
    }
}
