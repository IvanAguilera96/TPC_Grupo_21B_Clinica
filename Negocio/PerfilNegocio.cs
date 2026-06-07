using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio;
using ConexionBD;

namespace Negocio
{
    public class PerfilNegocio
    {
        public List<Perfil> Listar()
        {
            List<Perfil> lista = new List<Perfil>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("SELECT IdPerfil, Descripcion FROM Perfil");
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Perfil aux = new Perfil();
                    aux.IdPerfil = (int)datos.Lector["IdPerfil"];
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
