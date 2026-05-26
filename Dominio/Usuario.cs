using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    internal class Usuario
    {
        public int IdUsuario { get; set; }
        public string NombreUsuario { get; set; }
        public string ContraseniaUsiario { get; set; }
        public Perfil Perfil { get; set; } //Define los permisos de cada usuario.
    }
}
