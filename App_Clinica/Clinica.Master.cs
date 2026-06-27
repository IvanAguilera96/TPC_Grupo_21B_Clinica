using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace App_Clinica
{
    public partial class MasterPage : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UsuarioLogueado"] == null)
            {
                Response.Redirect("Login.aspx", false);
                return;
            }

            if (!IsPostBack)
            {
                Dominio.Usuario user = (Dominio.Usuario)Session["UsuarioLogueado"];

                lblUserNombre.Text = user.Nombre;

                // CONTROL DE ACCESOS POR ROL (Ocultar botones del menú lateral)
                string rol = user.Perfil.Descripcion;

                if (rol == "Medico")
                {
                    // El médico solo ve el Inicio (donde está su agenda) y sus Turnos
                    menuPacientes.Visible = false;
                    menuMedicos.Visible = false;
                    menuEspecialidad.Visible = false;
                    menuUsuario.Visible = false;
                }
                else if (rol == "Recepcionista")
                {
                    // La recepcionista ve Pacientes y Turnos. No ve médicos, especialidades ni usuarios
                    menuEspecialidad.Visible = false;
                    menuUsuario.Visible = false;
                }
                // Si es "Administrador", no entra a ningún IF y ve todo el menú
            }
        }

        protected void btnSalir_Click(object sender, EventArgs e)
        {
            Session.Clear();
            Session.Abandon();
            Response.Redirect("Login.aspx", false);
        }
    }
}