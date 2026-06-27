using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class Medico
    {
        public int IdMedico { get; set; }
        public string Dni { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public int Matricula { get; set; }
        public bool Estado { get; set; } //true=activo, false=inactivo
        public List<AgendaMedico> ListaAgendaMedico { get; set; }
        public string NombreCompleto
        {
            get { return Nombre + " " + Apellido; }
        }
        //Agregar relacion con usuario.
    }
}
