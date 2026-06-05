using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio;
using ConexionBD;

namespace Negocio
{
    public class UsuarioNegocio
    {
        public List<Usuario> Listar()
        {
            List<Usuario> lista = new List<Usuario>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("SELECT U.IdUsuario, U.Nombre, U.Contrasenia, P.IdPerfil, P.Descripcion FROM Usuario U INNER JOIN Perfil P ON U.IdPerfil = P.IdPerfil WHERE U.Estado = 1");
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Usuario aux = new Usuario();
                    aux.IdUsuario = (int)datos.Lector["IdUsuario"];
                    aux.Nombre = (string)datos.Lector["Nombre"];
                    aux.Contrasenia = (string)datos.Lector["Contrasenia"];

                    aux.Perfil = new Perfil();
                    aux.Perfil.IdPerfil = (int)datos.Lector["IdPerfil"];
                    aux.Perfil.Descripcion = (string)datos.Lector["Descripcion"];

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

        public void Agregar(Usuario nuevo)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("INSERT INTO Usuario (Nombre, Contrasenia, IdPerfil) VALUES (@usuario, @contrasenia, @idPerfil)");

                datos.setearParametros("@usuario", nuevo.Nombre);
                datos.setearParametros("@contrasenia", nuevo.Contrasenia);
                datos.setearParametros("@idPerfil", nuevo.Perfil.IdPerfil);

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
        }

        public void ELiminar(int IdEliminar)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("UPDATE USUARIO SET Estado = 0 WHERE IdUsuario = @id");
                datos.setearParametros("@id", IdEliminar);
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
        }
    }
}
