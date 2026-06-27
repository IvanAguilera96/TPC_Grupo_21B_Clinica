using Dominio;
using Negocio;
using System;
using System.Web.UI;
using Utiles;
using static Utiles.Utils;

namespace App_Clinica
{
    public partial class Default1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Seguridad.ValidarAcceso(this, "Administrador", "Recepcionista", "Medico");

            if (!IsPostBack)
            {
                // 1. Recuperamos de forma segura el usuario logueado en la sesión global
                if (Session["UsuarioLogueado"] != null)
                {
                    Dominio.Usuario usuarioLogueado = (Dominio.Usuario)Session["UsuarioLogueado"];

                    lblNombreUsuario.Text = usuarioLogueado.Nombre;

                    string rolDesc = usuarioLogueado.Perfil.Descripcion;

                    switch (rolDesc)
                    {
                        case "Administrador":
                            CargarDashboardAdmin();
                            break;

                        case "Recepcionista":
                            CargarDashboardRecepcion();
                            break;

                        case "Medico":
                            CargarDashboardMedico();
                            break;

                        default:
                            Utils.MostrarAlertaModal(this, "Su usuario no cuenta con un panel configurado para este sistema.");
                            break;
                    }
                }
                else
                {
                    Response.Redirect("Login.aspx", false);
                }
            }
        }

        private void CargarDashboardAdmin()
        {
            pnlAdmin.Visible = true;

            try
            {
                MedicoNegocio medNegocio = new MedicoNegocio();
                int totalMedicos = medNegocio.Listar().Count;
                lblCantMedicos.Text = totalMedicos.ToString();

                PacienteNegocio pacNegocio = new PacienteNegocio();
                int totalPacientes = pacNegocio.Listar().Count;
                lblCantPacientes.Text = totalPacientes.ToString();

                //listar turnos del mes (se completará al terminar Turnos)
            }
            catch (Exception ex)
            {
                Utils.MostrarAlertaModal(this, "Error al cargar las estadísticas de administración: " + ex.Message);
            }
        }

        private void CargarDashboardRecepcion()
        {
            pnlRecepcion.Visible = true;
        }

        private void CargarDashboardMedico()
        {
            pnlMedico.Visible = true;

            try
            {
                //recuperar el ID del médico asociado al usuario para filtrar y mostrar su grilla
                Dominio.Usuario usuarioLogueado = (Dominio.Usuario)Session["UsuarioLogueado"];
            }
            catch (Exception ex)
            {
                Utils.MostrarAlertaModal(this, "Error al cargar la agenda médica: " + ex.Message);
            }
        }
    }
}