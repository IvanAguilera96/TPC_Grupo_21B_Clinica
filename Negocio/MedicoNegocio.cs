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

    }
}
