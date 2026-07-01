using ConexionBD;
using Dominio;
using Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Utiles;
using static Utiles.Utils;

namespace App_Clinica
{
    public partial class MedicoAgendaForm : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Seguridad.ValidarAcceso(this, "Administrador", "Recepcionista", "Medico");

            if (Request.QueryString["idmedico"] == null)
            {
                Response.Redirect("MedicoPag.aspx");
                return;
            }

            if (!IsPostBack)
            {
                CargarDesplegable();
            }
        }

        private void CargarDesplegable()
        {
            try
            {
                //Carga Especialidades
                EspecialidadNegocio espNegocio = new EspecialidadNegocio();
                ddlEspecialidad.DataSource = espNegocio.Listar();
                ddlEspecialidad.DataValueField = "IdEspecialidad";
                ddlEspecialidad.DataTextField = "Descripcion";
                ddlEspecialidad.DataBind();

            }
            catch (Exception ex)
            {
                Utils.MostrarAlertaModal(this, "Error al cargar los selectores de la agenda: " + ex.Message);
            }
        } //CargarDesplegable

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            string idMedico = Request.QueryString["idmedico"];
            try
            {
                if (string.IsNullOrEmpty(txtHoraEntrada.Text) || string.IsNullOrEmpty(txtHoraSalida.Text))
                {
                    Utils.MostrarAlertaModal(this, "Debe completar el horario de entrada y salida.");
                    return;
                }

                int medicoId = int.Parse(idMedico);
                string diaTrabajo = ddlDia.SelectedValue;
                TimeSpan horaEntrada = TimeSpan.Parse(txtHoraEntrada.Text);
                TimeSpan horaSalida = TimeSpan.Parse(txtHoraSalida.Text);

                if (horaEntrada >= horaSalida)
                {
                    Utils.MostrarAlertaModal(this, "La hora de entrada no puede ser mayor o igual a la hora de salida.");
                    return;
                }

                AgendaMedicoNegocio negocio = new AgendaMedicoNegocio();

                //Evaluamos la superposición de horarios en el mismo día
                if (negocio.ValidarSuperposicionAgenda(medicoId, diaTrabajo, horaEntrada, horaSalida))
                {
                    Utils.MostrarAlertaModal(this, "El profesional ya tiene una agenda asignada en ese día que se superpone con el horario ingresado.");
                    return; 
                }

                // Si pasa la validación, continúa el flujo normal:
                AgendaMedico nuevaAgenda = new AgendaMedico();

                nuevaAgenda.Medico = new Medico();
                nuevaAgenda.Medico.IdMedico = medicoId;

                nuevaAgenda.Especialidad = new Especialidad();
                nuevaAgenda.Especialidad.IdEspecialidad = int.Parse(ddlEspecialidad.SelectedValue);

                nuevaAgenda.TurnoTrabajo = new TurnoTrabajo();
                nuevaAgenda.TurnoTrabajo.DiaDeTrabajo = diaTrabajo;
                nuevaAgenda.TurnoTrabajo.HoraEntrada = horaEntrada;
                nuevaAgenda.TurnoTrabajo.HoraSalida = horaSalida;

                negocio.AgregarConSP(nuevaAgenda);

                Session["MensajeExito"] = "Horario asignado con éxito.";
                Response.Redirect("MedicoAgendaPag.aspx?idmedico=" + idMedico, false);
            }
            catch (Exception ex)
            {
                Utils.MostrarAlertaModal(this, "No se pudo asignar el horario: " + ex.Message);
            }
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("MedicoAgendaPag.aspx?idmedico=" + Request.QueryString["idmedico"]);
        }
    }
}