using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class Turno
    {
        public int IdTurno { get; set; } //Ver si es necesario crear un nro de turno o se utiliza este (autonumerado)
        public DateTime Fecha { get; set; }
        public TimeSpan Hora { get; set; }

        //Relaciones de composición
        public AgendaMedico Agenda { get; set; } //Contien Médico, Especialidad y Turno de Trabajo
        public Paciente Paciente { get; set; }
        public EstadoTurno Estado { get; set; } //"Disponible", "Asignado", "Cancelado", "Reprogramado", "Atendido"

        public string Observacion { get; set; } //Motivo del turno
        public string Diagnostico { get; set; } //Resultado de la consulta
    }
}
