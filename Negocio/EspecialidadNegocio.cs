using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio;
using ConexionBD;

namespace Negocio
{
    public class EspecialidadNegocio
    {
        public List<Especialidad> Listar()
        {
            List<Especialidad> lista = new List<Especialidad>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("SELECT IdEspecialidad, Descripcion, Estado FROM Especialidad");
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Especialidad aux = new Especialidad();
                    aux.IdEspecialidad = (int)datos.Lector["IdEspecialidad"];
                    aux.Descripcion = (string)datos.Lector["Descripcion"];
                    aux.Estado = (bool)datos.Lector["Estado"];
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

        public Especialidad BuscarPorId(int id)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("SELECT IdEspecialidad, Descripcion, Estado FROM Especialidad WHERE IdEspecialidad = @id");
                datos.setearParametros("@id", id);
                datos.ejecutarLectura();

                if (datos.Lector.Read())
                {
                    Especialidad esp = new Especialidad();
                    esp.IdEspecialidad = (int)datos.Lector["IdEspecialidad"];
                    esp.Descripcion = datos.Lector["Descripcion"].ToString();
                    esp.Estado = (bool)datos.Lector["Estado"];
                    return esp;
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

        public void Agregar(Especialidad nueva)
        {
            if (ExisteEspecialidad(nueva.Descripcion, 0))
                throw new Exception("La especialidad ya se encuentra registrada.");

            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("INSERT INTO Especialidad (Descripcion, Estado) VALUES (@descripcion, 1)");
                datos.setearParametros("@descripcion", nueva.Descripcion);
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

        public void Modificar(Especialidad esp)
        {
            if (ExisteEspecialidad(esp.Descripcion, esp.IdEspecialidad))
                throw new Exception("Ya existe una especialidad con ese nombre.");

            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("UPDATE Especialidad SET Descripcion = @descripcion WHERE IdEspecialidad = @id");
                datos.setearParametros("@descripcion", esp.Descripcion);
                datos.setearParametros("@id", esp.IdEspecialidad);
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

        public void Eliminar(int idEliminar)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("UPDATE Especialidad SET Estado = 0 WHERE IdEspecialidad = @id");
                datos.setearParametros("@id", idEliminar);
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

        private bool ExisteEspecialidad(string descripcion, int idEspecialidad = 0)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("SELECT COUNT(*) FROM Especialidad WHERE Descripcion = @descripcion AND Estado = 1 AND IdEspecialidad <> @id");
                datos.setearParametros("@descripcion", descripcion);
                datos.setearParametros("@id", idEspecialidad);
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
    }
}
