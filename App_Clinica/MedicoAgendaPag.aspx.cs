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
    public partial class MedicoAgendaPag : System.Web.UI.Page
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
                CargarAgenda();

                if (Session["MensajeExito"] != null)
                {
                    lblMensajeGrilla.Text = Session["MensajeExito"].ToString();
                    lblMensajeGrilla.CssClass = "alert alert-success d-block text-center mb-3";
                    lblMensajeGrilla.Visible = true;

                    Session["MensajeExito"] = null;
                }
            }
        }
    

        private void CargarAgenda()
        {
            try
            {
                int idMedico = int.Parse(Request.QueryString["idmedico"]);

                AgendaMedicoNegocio agendaNegocio = new AgendaMedicoNegocio();
                List<AgendaMedico> lista = agendaNegocio.ListarAgendaPorMedico(idMedico);

                if (lista != null && lista.Count > 0)
                {
                    dgvAgenda.DataSource = lista;
                    dgvAgenda.DataBind();
                }
                else
                {
                    lblMensajeGrilla.CssClass = "alert alert-info d-block text-start w-100 mb-3";
                    lblMensajeGrilla.Text = "El médico seleccionado no posee horarios registrados en su agenda.";
                    lblMensajeGrilla.Visible = true;

                    contenedorAgenda.Visible = false;
                }
            }
            catch (Exception ex)
            {
                lblMensajeGrilla.CssClass = "alert alert-danger d-block text-start w-100 mb-3";
                lblMensajeGrilla.Text = "Error al intentar cargar la agenda: " + ex.Message;
                lblMensajeGrilla.Visible = true;
            }
        }

        protected void btnAsignarHorario_Click(object sender, EventArgs e)
        {
            string idMedico = Request.QueryString["idmedico"];
            Response.Redirect("MedicoAgendaForm.aspx?idmedico=" + idMedico);
        }

        protected void dgvAgenda_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "EliminarAgenda")
            {
                try
                {
                    int index = Convert.ToInt32(e.CommandArgument);
                    // Obtenemos el ID de la AgendaMedico guardado en las DataKeys de la fila correspondiente
                    int idAgendaMedico = Convert.ToInt32(dgvAgenda.DataKeys[index].Value);

                    AgendaMedicoNegocio agendaNegocio = new AgendaMedicoNegocio();
                    agendaNegocio.Eliminar(idAgendaMedico);

                    CargarAgenda();

                    lblMensajeGrilla.ForeColor = System.Drawing.Color.Green;
                    lblMensajeGrilla.Text = "El horario de la agenda se eliminó correctamente.";
                    lblMensajeGrilla.Visible = true;
                }
                catch (Exception ex)
                {
                    lblMensajeGrilla.ForeColor = System.Drawing.Color.Red;
                    lblMensajeGrilla.Text = "Error al intentar eliminar: Existe un turno en el horario seleccionado..";
                    lblMensajeGrilla.Visible = true;
                }
            }
        }


    }
}