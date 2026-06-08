using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio;
using ConexionBD;

namespace Negocio
{
	public class PacienteNegocio
	{
		public List<Paciente> Listar()
		{
            List<Paciente> lista = new List<Paciente>();
            AccesoDatos datos = new AccesoDatos();

			try
			{
				datos.setearConsulta("Select IdPaciente, Dni, Nombre, Apellido, Email, Telefono, Estado From Paciente");
				datos.ejecutarLectura();

				while (datos.Lector.Read())
				{
					Paciente aux = new Paciente();
					aux.IdPaciente = (int)datos.Lector["IdPaciente"];
					aux.Dni = (string)datos.Lector["Dni"];
					aux.Nombre = (string)datos.Lector["Nombre"];
					aux.Apellido = (string)datos.Lector["Apellido"];
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
        } // Listar

		public void Agregar(Paciente nuevo)
		{
			AccesoDatos datos = new AccesoDatos();

			try
			{
				datos.setearConsulta("INSERT INTO Paciente (Dni, Nombre, Apellido, Email, Telefono, Estado) VALUES (@Dni, @Nombre, @Apellido, @Email, @Telefono, 1)");
				datos.setearParametros("@Dni", nuevo.Dni);
                datos.setearParametros("@Nombre", nuevo.Nombre);
                datos.setearParametros("@Apellido", nuevo.Apellido);
                datos.setearParametros("@Email", nuevo.Email);
                datos.setearParametros("@Telefono", nuevo.Telefono);
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
				datos.setearConsulta("SELECT Dni, Nombre, Apellido, Email, Telefono, Estado FROM Paciente WHERE IdPaciente = @ID");
				datos.setearParametros("@ID", ID);
				datos.ejecutarLectura();

				if (datos.Lector.Read())
				{
					Paciente aux = new Paciente();
					aux.Dni = (string)datos.Lector["Dni"];
					aux.Nombre = (string)datos.Lector["Nombre"];
					aux.Apellido = (string)datos.Lector["Apellido"];
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

    } // PacienteNegocio
}

