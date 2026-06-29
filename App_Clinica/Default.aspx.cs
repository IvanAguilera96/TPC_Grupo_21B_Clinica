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
            // 🔒 El cerrojo unificado protege la página completa ante cualquier ingreso ilegal por URL
            Seguridad.ValidarAcceso(this, "Administrador", "Recepcionista", "Medico");

            if (!IsPostBack)
            {
                if (Session["UsuarioLogueado"] != null)
                {
                    Dominio.Usuario usuarioLogueado = (Dominio.Usuario)Session["UsuarioLogueado"];

                    // Asignamos la bienvenida dinámica al Label
                    lblNombreUsuario.Text = usuarioLogueado.Nombre;

                    string rolDesc = usuarioLogueado.Perfil.Descripcion;

                    // Encendemos y cargamos el panel correspondiente al rol
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

                // Inicializado en 0 por ahora hasta mapear la capa de Turnos
                lblCantTurnosMes.Text = "0";
            }
            catch (Exception ex)
            {
                Utils.MostrarAlertaModal(this, "Error al cargar las estadísticas de administración: " + ex.Message);
            }
        }

        private void CargarDashboardRecepcion()
        {
            pnlRecepcion.Visible = true;

            try
            {
                TurnoNegocio turnoNegocio = new TurnoNegocio();
                string fechaHoy = DateTime.Today.ToString("yyyy-MM-dd");

                var turnosDeHoy = turnoNegocio.ListarConFiltros(0, 0, fechaHoy);

                dgvProximosTurnos.DataSource = turnosDeHoy;
                dgvProximosTurnos.DataBind();
            }
            catch (Exception ex)
            {
                Utils.MostrarAlertaModal(this, "Error al cargar el monitor de recepción: " + ex.Message);
            }
        }

        private void CargarDashboardMedico()
        {
            pnlMedico.Visible = true;

            try
            {
                Dominio.Usuario usuarioLogueado = (Dominio.Usuario)Session["UsuarioLogueado"];
                int idMedicoLogueado = usuarioLogueado.IdUsuario;

                TurnoNegocio turnoNegocio = new TurnoNegocio();
                string fechaHoy = DateTime.Today.ToString("yyyy-MM-dd");

                // Filtramos: IdMedico de la sesión, IdEspecialidad = 0 (Todas), Fecha = Hoy
                var agendaDelDia = turnoNegocio.ListarConFiltros(idMedicoLogueado, 0, fechaHoy);

                dgvTurnosDelDia.DataSource = agendaDelDia;
                dgvTurnosDelDia.DataBind();
            }
            catch (Exception ex)
            {
                Utils.MostrarAlertaModal(this, "Error al cargar la agenda médica: " + ex.Message);
            }
        }

    }
}