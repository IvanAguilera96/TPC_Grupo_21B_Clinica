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
				datos.setearConsulta("Select IdPaciente, Dni, Nombre, Apellido, Email, Telefono From Paciente");
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

	} // PacienteNegocio
}

