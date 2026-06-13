using Dominio;
using Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace App_Clinica
{
    public partial class MedicoAgendaPag : System.Web.UI.Page
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
                CargarAgenda();
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


    }
}