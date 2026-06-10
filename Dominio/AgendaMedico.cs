using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class AgendaMedico
    {
        public int IdAgendaMedico { get; set; }
        public Medico IdMedico { get; set; }
        public Especialidad Especialidad { get; set; }
        public TurnoTrabajo TurnoTrabajo { get; set; }
    }
}
