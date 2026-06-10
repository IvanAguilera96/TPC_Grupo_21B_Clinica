using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class TurnoTrabajo
    { 
        public int IdTurnoTrabajo { get; set; }
        public string Descripcion { get; set; }
        public TimeSpan HoraEntrada { get; set; } 
        public TimeSpan HoraSalida { get; set; }
        public string DiaDeTrabajo { get; set; }
    }
}
