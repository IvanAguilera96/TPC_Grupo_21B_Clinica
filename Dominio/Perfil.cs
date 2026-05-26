using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    internal class Perfil
    {
        public int IdPerfil { get; set; }
        public string Descripcion { get; set; } //Médico, Recepcionista, etc. Rol asociado al usuario
    }
}
