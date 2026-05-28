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
        public List<TurnoNegocio> Listar()
        {
            List<TurnoNegocio> lista = new List<TurnoNegocio>();
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
