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
                ControlarAccesoPorRol();
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
            else if (e.CommandName == "VerDetalle")
            {
                try
                {
                    var listado = turnoNegocio.ListarConFiltros(0, 0, "").Where(x => x.IdTurno == IdTurno).ToList();

                    if (listado.Count > 0)
                    {
                        var turnoSeleccionado = listado[0];

                        lblDetallePaciente.Text = turnoSeleccionado.Paciente?.NombreCompleto ?? "N/A";
                        lblDetalleMedico.Text = turnoSeleccionado.Agenda?.Medico?.NombreCompleto ?? "N/A";
                        lblDetalleEspecialidad.Text = turnoSeleccionado.Agenda?.Especialidad?.Descripcion ?? "N/A";
                        lblDetalleFechaHora.Text = $"{turnoSeleccionado.Fecha:dd/MM/yyyy} a las {turnoSeleccionado.Hora:hh\\:mm} hs.";
                        lblDetalleEstado.Text = turnoSeleccionado.Estado?.Descripcion ?? "Sin Estado";

                        lblDetalleEspecialidad.Text = turnoSeleccionado.Agenda?.Especialidad?.Descripcion ?? "N/A";
                        lblDetalleFechaHora.Text = $"{turnoSeleccionado.Fecha:dd/MM/yyyy} a las {turnoSeleccionado.Hora:hh\\:mm} hs.";
                        lblDetalleEstado.Text = turnoSeleccionado.Estado?.Descripcion ?? "Sin Estado";

                        //Mapeo de la Observación (Motivo del turno)
                        if (!string.IsNullOrEmpty(turnoSeleccionado.Observacion))
                        {
                            lblDetalleObservacion.Text = turnoSeleccionado.Observacion;
                            lblDetalleObservacion.CssClass = "text-dark fw-medium small";
                        }
                        else
                        {
                            lblDetalleObservacion.Text = "No se especificó un motivo al agendar el turno.";
                            lblDetalleObservacion.CssClass = "fst-italic text-muted small";
                        }

                        //Mapeo del Diagnóstico (Resultado de la consulta)
                        if (!string.IsNullOrEmpty(turnoSeleccionado.Diagnostico))
                        {
                            lblDetalleDiagnostico.Text = turnoSeleccionado.Diagnostico;
                            lblDetalleDiagnostico.CssClass = "text-dark fw-medium small";
                        }
                        else
                        {
                            lblDetalleDiagnostico.Text = "Pendiente de atención o no registra diagnósticos cargados.";
                            lblDetalleDiagnostico.CssClass = "fst-italic text-muted small";
                        }

                        string descEstado = turnoSeleccionado.Estado?.Descripcion;
                        lblDetalleEstado.CssClass = "badge rounded-2 px-2.5 py-1.5 fw-semibold " +
                            (descEstado == "Asignado" ? "bg-warning text-dark" :
                             descEstado == "Cerrado" ? "bg-success bg-opacity-10 text-success" :
                             descEstado == "Reprogramado" ? "bg-info bg-opacity-10 text-info" :
                             descEstado == "No Asistió" ? "bg-secondary bg-opacity-20 text-dark" : "bg-danger bg-opacity-10 text-danger");

                        if (turnoSeleccionado != null && !string.IsNullOrEmpty(turnoSeleccionado.Diagnostico))
                        {
                            lblDetalleDiagnostico.Text = turnoSeleccionado.Diagnostico;
                            lblDetalleDiagnostico.CssClass = "text-dark";
                        }
                        else
                        {
                            lblDetalleDiagnostico.Text = "No se registraron diagnósticos ni observaciones para este turno todavía.";
                            lblDetalleDiagnostico.CssClass = "fst-italic text-muted small";
                        }

                        ScriptManager.RegisterStartupScript(this, this.GetType(), "PopDetalle", "abrirModalDetalle();", true);
                    }
                }
                catch (Exception ex)
                {
                    Utils.MostrarAlertaModal(this, "Error al cargar el detalle del turno: " + ex.Message);
                }
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

        private void ControlarAccesoPorRol()
        {
            if (Session["UsuarioLogueado"] != null)
            {
                Dominio.Usuario usuarioLogueado = (Dominio.Usuario)Session["UsuarioLogueado"];

                // Si el perfil corresponde al de un Médico
                if (usuarioLogueado.Perfil != null && usuarioLogueado.Perfil.Descripcion == "Medico")
                {
                    if (usuarioLogueado.Medico != null && usuarioLogueado.Medico.IdMedico > 0)
                    {
                        // Preseleccionamos su propio ID en el desplegable de filtros
                        ddlFiltroMedico.SelectedValue = usuarioLogueado.Medico.IdMedico.ToString();

                        // Deshabilitamos el combo para que no pueda cambiarse a otro colega
                        ddlFiltroMedico.Enabled = false;
                    }
                    else
                    {
                        Utils.MostrarAlertaModal(this, "Atención: Su usuario tiene perfil Médico pero no se encuentra vinculado a ningún profesional en el sistema.");
                    }
                }
            }
        }
    }
}