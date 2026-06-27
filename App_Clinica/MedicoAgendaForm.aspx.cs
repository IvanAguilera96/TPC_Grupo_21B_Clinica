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
                if(string.IsNullOrEmpty(txtHoraEntrada.Text) || string.IsNullOrEmpty(txtHoraSalida.Text))
                {
                    Utils.MostrarAlertaModal(this, "Debe completar el horario de entrada y salida.");
                    return;
                }

                AgendaMedico nuevaAgenda = new AgendaMedico();

                nuevaAgenda.Medico = new Medico();
                nuevaAgenda.Medico.IdMedico = int.Parse(idMedico);

                nuevaAgenda.Especialidad = new Especialidad();
                nuevaAgenda.Especialidad.IdEspecialidad = int.Parse(ddlEspecialidad.SelectedValue);

                nuevaAgenda.TurnoTrabajo = new TurnoTrabajo();
                nuevaAgenda.TurnoTrabajo.DiaDeTrabajo = ddlDia.SelectedValue;
                nuevaAgenda.TurnoTrabajo.HoraEntrada = TimeSpan.Parse(txtHoraEntrada.Text);
                nuevaAgenda.TurnoTrabajo.HoraSalida = TimeSpan.Parse(txtHoraSalida.Text);

                AgendaMedicoNegocio negocio = new AgendaMedicoNegocio();
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