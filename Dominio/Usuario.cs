using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class Usuario
    {
        public int IdUsuario { get; set; }
        public string Nombre { get; set; }
        public string Contrasenia { get; set; }
        public Perfil Perfil { get; set; } //Define los permisos del usuario.

        public bool Estado { get; set; } //true=activo, false=inactivo
        //Propiedades
        public string DescripcionPerfil
        {
            get { return Perfil.Descripcion; } //Para cargar la descripción del perfil en la grilla de usuarios.
        }
    }
}
