using Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Utiles;

namespace App_Clinica
{
    public partial class TurnoPag : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarFiltros();
                CargarGrilla();
            }
        }

        protected void ddlFiltroMedico_SelectedIndexChanged(object sender, EventArgs e)
        {
            CargarGrilla();
        }

        protected void ddlFiltroEspecialidad_SelectedIndexChanged(object sender, EventArgs e)
        {
            CargarGrilla();
        }

        protected void txtFiltroFecha_TextChanged(object sender, EventArgs e)
        {
            CargarGrilla();
        }

        protected void btnNuevoTurno_Click(object sender, EventArgs e)
        {
            Response.Redirect("TurnoForm.aspx");
        }

        protected void dgvTurnos_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            TurnoNegocio turnoNegocio = new TurnoNegocio();
            int IdTurno = Convert.ToInt32(e.CommandArgument);
            lblMensajeExito.Visible = false;

            if (e.CommandName == "CancelarTurno")
            {
                try
                {
                    int IdEstadoCancelado = 1; // 1 = Cancelado en BD
                    turnoNegocio.CambiarEstado(IdTurno, IdEstadoCancelado);

                    lblMensajeExito.Text = "El turno fue cancelado correctamente.";
                    lblMensajeExito.Visible = true;
                    CargarGrilla();
                }
                catch (Exception ex)
                {
                    Utils.MostrarAlertaModal(this, "No se pudo cancelar el turno: " + ex.Message);
                }
            }
            else if (e.CommandName == "AusenteTurno")
            {
                try
                {
                    int IdEstadoAusente = 5; // 5 = No Asistió en BD
                    turnoNegocio.CambiarEstado(IdTurno, IdEstadoAusente);

                    lblMensajeExito.Text = "Se registró correctamente la inasistencia del paciente.";
                    lblMensajeExito.Visible = true;
                    CargarGrilla();
                }
                catch (Exception ex)
                {
                    Utils.MostrarAlertaModal(this, "No se pudo cambiar el estado: " + ex.Message);
                }
            }
            else if (e.CommandName == "ReprogramarTurno")
            {
                Response.Redirect($"TurnoForm.aspx?reprogramar={IdTurno}");
            }
        }

        protected void btnGuardarCierre_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(hfIdTurnoACerrar.Value)) return;

            int idTurno = int.Parse(hfIdTurnoACerrar.Value);
            string comentarioDiagnostico = txtDiagnostico.Text.Trim();

            if (string.IsNullOrEmpty(comentarioDiagnostico))
            {
                Utils.MostrarAlertaModal(this, "Debe ingresar un diagnóstico para poder cerrar el turno.");
                return;
            }

            try
            {
                lblMensajeExito.Visible = false;
                TurnoNegocio turnoNegocio = new TurnoNegocio();

                int IdEstadoCerrado = 6; // 6 = Cerrado en BD
                turnoNegocio.CambiarEstado(idTurno, IdEstadoCerrado);
                turnoNegocio.ActualizarDiagnostico(idTurno, comentarioDiagnostico);

                lblMensajeExito.Text = "El turno ha sido cerrado y el diagnóstico se registró con éxito.";
                lblMensajeExito.Visible = true;

                CargarGrilla();
            }
            catch (Exception ex)
            {
                Utils.MostrarAlertaModal(this, "Error al cerrar el turno: " + ex.Message);
            }
        }

        private void CargarFiltros()
        {
            MedicoNegocio medNegocio = new MedicoNegocio();
            ddlFiltroMedico.DataSource = medNegocio.Listar();
            ddlFiltroMedico.DataValueField = "IdMedico";
            ddlFiltroMedico.DataTextField = "NombreCompleto";
            ddlFiltroMedico.DataBind();
            ddlFiltroMedico.Items.Insert(0, new ListItem("", "0"));

            EspecialidadNegocio espNegocio = new EspecialidadNegocio();
            ddlFiltroEspecialidad.DataSource = espNegocio.Listar();
            ddlFiltroEspecialidad.DataValueField = "IdEspecialidad";
            ddlFiltroEspecialidad.DataTextField = "Descripcion";
            ddlFiltroEspecialidad.DataBind();
            ddlFiltroEspecialidad.Items.Insert(0, new ListItem("", "0"));
        }

        private void CargarGrilla()
        {
            TurnoNegocio turnoNegocio = new TurnoNegocio();
            int idMedico = int.Parse(ddlFiltroMedico.SelectedValue ?? "0");
            int idEspecialidad = int.Parse(ddlFiltroEspecialidad.SelectedValue ?? "0");
            string fecha = txtFiltroFecha.Text;

            dgvTurnos.DataSource = turnoNegocio.ListarConFiltros(idMedico, idEspecialidad, fecha);
            dgvTurnos.DataBind();
        }
    }
}