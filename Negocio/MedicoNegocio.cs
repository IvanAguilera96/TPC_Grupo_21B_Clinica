using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio;
using ConexionBD;

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
            if (ExisteDni(nuevo.Dni, 0))
            {
                throw new Exception("Ya existe un médico registrado con el DNI ingresado.");
            }
            if (ExisteMatricula(nuevo.Matricula, 0))
            {
                throw new Exception("Ya existe un médico registrado con la Matrícula ingresada.");
            }

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
        }

        public Medico BuscarPorId(int id)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("SELECT IdMedico, Dni, Nombre, Apellido, Matricula, Estado FROM Medico WHERE IdMedico = @id");
                datos.setearParametros("@id", id);
                datos.ejecutarLectura();

                if (datos.Lector.Read())
                {
                    Medico aux = new Medico();
                    aux.IdMedico = (int)datos.Lector["IdMedico"];
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
        } // BuscarPorId

        public void Modificar(Medico modificar)
        {
            if (ExisteDni(modificar.Dni, modificar.IdMedico))
            {
                throw new Exception("Ya existe otro médico registrado con el DNI ingresado.");
            }
            if (ExisteMatricula(modificar.Matricula, modificar.IdMedico))
            {
                throw new Exception("Ya existe otro médico registrado con la Matrícula ingresada.");
            }

            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("UPDATE Medico SET Dni = @Dni, Nombre = @Nombre, Apellido = @Apellido, Matricula = @Matricula, Estado = @Estado WHERE IdMedico = @id");
                datos.setearParametros("@Dni", modificar.Dni);
                datos.setearParametros("@Nombre", modificar.Nombre);
                datos.setearParametros("@Apellido", modificar.Apellido);
                datos.setearParametros("@Matricula", modificar.Matricula);
                datos.setearParametros("@Estado", modificar.Estado);
                datos.setearParametros("@id", modificar.IdMedico);

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

        public void Eliminar(int idEliminar)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("UPDATE Medico SET Estado = 0 WHERE IdMedico = @id");
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
        } // Eliminar

        public List<Medico> ListarMedicoPorEspecialidad(int IdEspecialidad)
        {
            List<Medico> lista = new List<Medico>();
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("SELECT DISTINCT M.IdMedico, M.Nombre, M.Apellido FROM Medico M INNER JOIN AgendaMedico A ON M.IdMedico = A.IdMedico WHERE A.IdEspecialidad = @IdEspecialidad");
                datos.setearParametros("@IdEspecialidad", IdEspecialidad);
                        
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Medico aux = new Medico();
                    aux.IdMedico = (int)datos.Lector["IdMedico"];
                    aux.Nombre = (string)datos.Lector["Nombre"];
                    aux.Apellido = (string)datos.Lector["Apellido"];

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

        }// ListarMedicoPorEspecialidad

        // Metodos Privados para usar dentro de la clase
        private bool ExisteDni(string dni, int idMedicoActual = 0)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("SELECT COUNT(*) FROM Medico WHERE Dni = @Dni AND IdMedico <> @IdMedico");
                datos.setearParametros("@Dni", dni);
                datos.setearParametros("@IdMedico", idMedicoActual);
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
        } //ExisteDni
        private bool ExisteMatricula(int matricula, int idMedicoActual = 0)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("SELECT COUNT(*) FROM Medico WHERE Matricula = @Matricula AND IdMedico <> @IdMedico");
                datos.setearParametros("@Matricula", matricula);
                datos.setearParametros("@IdMedico", idMedicoActual);
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
        } //ExisteMatricula

    }
}
