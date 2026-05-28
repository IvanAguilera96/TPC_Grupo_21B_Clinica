using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class EstadoTurno
    {
        public int IdEstado { get; set; }
        public string Descripcion { get; set; } //Cancelado, Asignado, etc.
    }
}
