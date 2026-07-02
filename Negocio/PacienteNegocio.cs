using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio;
using ConexionBD;
using System.Data;

namespace Negocio
{
	public class PacienteNegocio
	{
        public List<Paciente> Listar(string filtroDni = "", string filtroNombre = "")
        {
            List<Paciente> lista = new List<Paciente>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                string consulta = "SELECT IdPaciente, Dni, Nombre, Apellido, FechaNacimiento, Email, Telefono, Estado FROM Paciente WHERE 1 = 1";

                if (!string.IsNullOrWhiteSpace(filtroDni))
                {
                    consulta += " AND Dni LIKE @Dni";
                    datos.setearParametros("@Dni", filtroDni + "%"); 
                }

                if (!string.IsNullOrWhiteSpace(filtroNombre))
                {
                    consulta += " AND (Nombre LIKE @Nombre OR Apellido LIKE @Nombre)";
                    datos.setearParametros("@Nombre", "%" + filtroNombre + "%"); 
                }

                datos.setearConsulta(consulta);
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Paciente aux = new Paciente();
                    aux.IdPaciente = (int)datos.Lector["IdPaciente"];
                    aux.Dni = (string)datos.Lector["Dni"];
                    aux.Nombre = (string)datos.Lector["Nombre"];
                    aux.Apellido = (string)datos.Lector["Apellido"];
                    aux.FechaNacimiento = (DateTime)datos.Lector["FechaNacimiento"];
                    aux.Email = (string)datos.Lector["Email"];
                    aux.Telefono = (string)datos.Lector["Telefono"];
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
        } //Listar

        public void Agregar(Paciente nuevo)
		{
			AccesoDatos datos = new AccesoDatos();

            if (ExisteDni(nuevo.Dni, 0))
            {
                throw new Exception("Ya existe un médico registrado con el DNI ingresado.");
            }

            try
			{
				datos.setearConsulta("INSERT INTO Paciente (Dni, Nombre, Apellido, FechaNacimiento,  Email, Telefono, Estado) VALUES (@Dni, @Nombre, @Apellido, @FechaNacimiento, @Email, @Telefono, @Estado)");
				datos.setearParametros("@Dni", nuevo.Dni);
                datos.setearParametros("@Nombre", nuevo.Nombre);
                datos.setearParametros("@Apellido", nuevo.Apellido);
                datos.setearParametros("@FechaNacimiento", nuevo.FechaNacimiento);
                datos.setearParametros("@Email", nuevo.Email);
                datos.setearParametros("@Telefono", nuevo.Telefono);
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

		public void Eliminar(int ID)
		{
			AccesoDatos datos = new AccesoDatos();

			try
			{
				datos.setearConsulta("UPDATE Paciente SET Estado = 0 WHERE IdPaciente = @ID");
				datos.setearParametros("@ID", ID);
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

		public Paciente buscoPaciente(int ID)
		{
			AccesoDatos datos = new AccesoDatos();
			try
			{
				datos.setearConsulta("SELECT Dni, Nombre, Apellido, FechaNacimiento, Email, Telefono, Estado FROM Paciente WHERE IdPaciente = @ID");
				datos.setearParametros("@ID", ID);
				datos.ejecutarLectura();

				if (datos.Lector.Read())
				{
					Paciente aux = new Paciente();
					aux.Dni = (string)datos.Lector["Dni"];
					aux.Nombre = (string)datos.Lector["Nombre"];
					aux.Apellido = (string)datos.Lector["Apellido"];
                    aux.FechaNacimiento = (DateTime)datos.Lector["FechaNacimiento"];
                    aux.Email = (string)datos.Lector["Email"];
					aux.Telefono = (string)datos.Lector["Telefono"];
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
        } // buscoPaciente

		public void Modificar(Paciente paciente)
		{
			AccesoDatos datos = new AccesoDatos();

            if (ExisteDni(paciente.Dni, 0))
            {
                throw new Exception("Ya existe un médico registrado con el DNI ingresado.");
            }

            try
			{
				datos.setearConsulta("UPDATE Paciente SET Dni = @Dni, Nombre = @Nombre, Apellido = @Apellido, FechaNacimiento = @fechaNacimiento, Email = @Email, Telefono = @Telefono, Estado = @Estado WHERE IdPaciente = @ID");
				datos.setearParametros("@ID", paciente.IdPaciente);
				datos.setearParametros("@Dni", paciente.Dni);
                datos.setearParametros("@Nombre", paciente.Nombre);
                datos.setearParametros("@Apellido", paciente.Apellido);
                datos.setearParametros("@fechaNacimiento", paciente.FechaNacimiento);
                datos.setearParametros("@Email", paciente.Email);
                datos.setearParametros("@Telefono", paciente.Telefono);
                datos.setearParametros("@Estado", paciente.Estado);
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
        }// Modificar

        public string ObtenerEmailPorId(int idPaciente)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("SELECT Email FROM Paciente WHERE IdPaciente = @IdPaciente");
                datos.setearParametros("@IdPaciente", idPaciente);

                datos.ejecutarLectura();

                if (datos.Lector.Read())
                {
                    // Validamos que el campo no sea nulo en la base de datos
                    if (!(datos.Lector["Email"] is DBNull))
                    {
                        return datos.Lector["Email"].ToString();
                    }
                }

                return "";
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

        private bool ExisteDni(string dni, int idPaciente = 0)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("SELECT COUNT(*) FROM Paciente WHERE Dni = @Dni AND IdPaciente <> @IdPaciente");
                datos.setearParametros("@Dni", dni);
                datos.setearParametros("@IdPaciente", idPaciente);
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
    } 

}

