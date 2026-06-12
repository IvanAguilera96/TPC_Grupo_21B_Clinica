using ConexionBD;
using Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class MedicoNegocio
    {
        public List<Medico> Listar() {

            List<Medico> lista = new List<Medico>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("SELECT IdMedico, Dni, Nombre, Apellido, Matricula, Estado FROM Medico");
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Medico aux = new Medico();
                    aux.IdMedico = (int)datos.Lector["IdMedico"];
                    aux.Dni = (string)datos.Lector["Dni"];
                    aux.Nombre = (string)datos.Lector["Nombre"];
                    aux.Apellido = (string)datos.Lector["Apellido"];
                    aux.Matricula = (int)datos.Lector["Matricula"];
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

        }// Listar

        public void Agregar(Medico nuevo)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("INSERT INTO Medico (Dni, Nombre, Apellido, Matricula, Estado) VALUES (@Dni, @Nombre, @Apellido, @Matricula, @Estado)");
                datos.setearParametros("@Dni", nuevo.Dni);
                datos.setearParametros("@Nombre", nuevo.Nombre);
                datos.setearParametros("@Apellido", nuevo.Apellido);
                datos.setearParametros("@Matricula", nuevo.Matricula);
                datos.setearParametros("@Estado", nuevo.Estado);
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

        public Medico BuscarMedico(int IdMedico)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("SELECT Dni, Nombre, Apellido, Matricula, Estado FROM Medico WHERE IdMedico = @ID");
                datos.setearParametros("@ID", IdMedico);
                datos.ejecutarLectura();

                if (datos.Lector.Read())
                {
                    Medico aux = new Medico();
                    aux.Dni = (string)datos.Lector["Dni"];
                    aux.Nombre = (string)datos.Lector["Nombre"];
                    aux.Apellido = (string)datos.Lector["Apellido"];
                    aux.Matricula = (int)datos.Lector["Matricula"];
                    aux.Estado = (bool)datos.Lector["Estado"];

                    return aux;
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
        }// BuscarMedico

        public void Modificar(Medico medico)
        {
            AccesoDatos datos = new AccesoDatos ();

            try
            {
                datos.setearConsulta("UPDATE Medico SET Dni = @Dni, Nombre = @Nombre, Apellido = @Apellido, Matricula = @Matricula, Estado = @Estado WHERE IdMedico = @ID");
                datos.setearParametros("@ID", medico.IdMedico);
                datos.setearParametros("@Dni", medico.Dni);
                datos.setearParametros("@Nombre", medico.Nombre);
                datos.setearParametros("@Apellido", medico.Apellido);
                datos.setearParametros("@Matricula", medico.Matricula);
                datos.setearParametros("@Estado", medico.Estado);
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

        } // Modificar

        public void Eliminar(int IdMedico)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("UPDATE Medico SET Estado = 0 WHERE IdMedico = @IdMedico");
                datos.setearParametros("@IdMedico", IdMedico);
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
