using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    internal class Turno
    {
        public int IdTurno { get; set; } //Ver si es necesario crear un nro de turno o se utiliza este (autonumerado)
        public DateTime Fecha { get; set; }
        public TimeSpan Hora { get; set; }
        public string Observaciones { get; set; } //Motivo del turno
        public string Diagnostico { get; set; } //Resultado de la consulta
    }
}
