using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio;
using ConexionBD;

namespace Negocio
{
    public class EstadoTurno
    {
        public List<EstadoTurno> Listar()
        {
            List<EstadoTurno> lista = new List<EstadoTurno>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                return lista;
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {

            }
        }
    }
}
