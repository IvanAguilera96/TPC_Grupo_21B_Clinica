using ConexionBD;
using Dominio;
using Negocio;
using Utiles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace App_Clinica
{
    public partial class MedicoAgendaForm : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["idmedico"] == null)
            {
                Response.Redirect("MedicoPag.aspx");
                return;
            }

            if (!IsPostBack)
            {
                CargarDesplegables();
            }
        }

        private void CargarDesplegables()
        {
            try
            {
                //Carga Especialidades
                EspecialidadNegocio espNegocio = new EspecialidadNegocio();
                ddlEspecialidad.DataSource = espNegocio.Listar();
                ddlEspecialidad.DataValueField = "IdEspecialidad";
                ddlEspecialidad.DataTextField = "Descripcion";
                ddlEspecialidad.DataBind();

                // Carga TurnoTrabajo
                TurnoTrabajoNegocio ttNegocio = new TurnoTrabajoNegocio();
                ddlTurnoTrabajo.DataSource = ttNegocio.Listar();
                ddlTurnoTrabajo.DataValueField = "IdTurnoTrabajo";
                ddlTurnoTrabajo.DataTextField = "Descripcion";
                ddlTurnoTrabajo.DataBind();
            }
            catch (Exception ex)
            {
                Utils.MostrarAlertaModal(this, "Error al cargar los selectores de la agenda: " + ex.Message);
            }
        } //CargarDesplegables

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            string idMedico = Request.QueryString["idmedico"];
            try
            {
                AgendaMedico nuevaAgenda = new AgendaMedico();

                nuevaAgenda.Medico = new Medico();
                nuevaAgenda.Medico.IdMedico = int.Parse(idMedico);

                nuevaAgenda.Especialidad = new Especialidad();
                nuevaAgenda.Especialidad.IdEspecialidad = int.Parse(ddlEspecialidad.SelectedValue);

                nuevaAgenda.TurnoTrabajo = new TurnoTrabajo();
                nuevaAgenda.TurnoTrabajo.IdTurnoTrabajo = int.Parse(ddlTurnoTrabajo.SelectedValue);

                AgendaMedicoNegocio negocio = new AgendaMedicoNegocio();
                negocio.Agregar(nuevaAgenda);

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