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
                datos.setearConsulta("SELECT U.IdUsuario, U.Nombre, U.Estado, P.IdPerfil, P.Descripcion FROM Usuario U INNER JOIN Perfil P ON U.IdPerfil = P.IdPerfil");
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Usuario aux = new Usuario();
                    aux.IdUsuario = (int)datos.Lector["IdUsuario"];
                    aux.Nombre = (string)datos.Lector["Nombre"];
                    aux.Estado = (bool)datos.Lector["Estado"];
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

        public Dominio.Usuario BuscarPorId(int id)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("SELECT U.IdUsuario, U.Nombre, U.Contrasenia, U.Estado, P.IdPerfil, P.Descripcion FROM Usuario U INNER JOIN Perfil P ON U.IdPerfil = P.IdPerfil WHERE U.IdUsuario = @id");
                datos.setearParametros("@id", id);
                datos.ejecutarLectura();

                if (datos.Lector.Read())
                {
                    Dominio.Usuario user = new Usuario();
                    user.IdUsuario = (int)datos.Lector["IdUsuario"];
                    user.Nombre = datos.Lector["Nombre"].ToString();
                    user.Contrasenia = datos.Lector["Contrasenia"].ToString();
                    user.Estado = (bool)datos.Lector["Estado"];
                    user.Perfil = new Perfil();
                    user.Perfil.IdPerfil = (int)datos.Lector["IdPerfil"];
                    user.Perfil.Descripcion = datos.Lector["Descripcion"].ToString();

                    return user;
                }

                return null;
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
            if (ExisteUsuario(nuevo.Nombre, 0))
            {
                throw new Exception("El nombre de usuario ya existe.");
            }

            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("INSERT INTO Usuario (Nombre, Contrasenia, IdPerfil, Estado) VALUES (@usuario, @contrasenia, @idPerfil, 1)");
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

        public void Modificar(Usuario user)
        {
            if (ExisteUsuario(user.Nombre, user.IdUsuario))
            {
                throw new Exception("El nombre de usuario ya existe.");
            }

            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("UPDATE Usuario SET Nombre = @nombre, Contrasenia = @contrasenia, Estado = @estado, IdPerfil = @idPerfil WHERE IdUsuario = @id");
                datos.setearParametros("@nombre", user.Nombre);
                datos.setearParametros("@contrasenia", user.Contrasenia);
                datos.setearParametros("@estado", user.Estado);
                datos.setearParametros("@idPerfil", user.Perfil.IdPerfil);
                datos.setearParametros("@id", user.IdUsuario);
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
        public void Eliminar(int IdEliminar)
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

        private bool ExisteUsuario(string nombreUsuario, int IdUsuarioActual = 0)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("SELECT COUNT(*) FROM Usuario WHERE Nombre = @nombre AND IdUsuario <> @id");
                datos.setearParametros("@nombre", nombreUsuario);
                datos.setearParametros("@id", IdUsuarioActual);
                datos.ejecutarLectura();

                if (datos.Lector.Read())
                {
                    int cantidad = (int)datos.Lector[0];
                    return cantidad > 0;
                }

                return false;
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

        public Usuario ValidarLogin(string nombre, string contrasenia)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                //Recupera usuario y perfil
                string consulta = @"SELECT U.IdUsuario, U.Nombre, U.IdPerfil, U.Estado, P.Descripcion as Perfil 
                                    FROM Usuario U 
                                    INNER JOIN Perfil P ON U.IdPerfil = P.IdPerfil 
                                    WHERE U.Nombre = @nombre AND U.Contrasenia = @contrasenia";

                datos.setearConsulta(consulta);
                datos.setearParametros("@nombre", nombre);
                datos.setearParametros("@contrasenia", contrasenia);

                datos.ejecutarLectura();

                if (datos.Lector.Read())
                {
                    Usuario usuarioLogueado = new Usuario();
                    usuarioLogueado.IdUsuario = (int)datos.Lector["IdUsuario"];
                    usuarioLogueado.Nombre = (string)datos.Lector["Nombre"];
                    usuarioLogueado.Estado = (bool)datos.Lector["Estado"];

                    usuarioLogueado.Perfil = new Perfil();
                    usuarioLogueado.Perfil.IdPerfil = (int)datos.Lector["IdPerfil"];
                    usuarioLogueado.Perfil.Descripcion = (string)datos.Lector["Perfil"];

                    return usuarioLogueado; //Credenciales correctas, devolvemos el usuario
                }

                return null; //las credenciales no existen o el usuario está de baja
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
