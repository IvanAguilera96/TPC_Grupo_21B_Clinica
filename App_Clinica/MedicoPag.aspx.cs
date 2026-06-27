using Dominio;
using Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using static Utiles.Utils;

namespace App_Clinica
{
    public partial class MedicoPag : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Seguridad.ValidarAcceso(this, "Administrador", "Recepcionista");

            if (!IsPostBack)
            {
                ActualizarGrillaMedico();

                if (Session["MensajeExito"] != null)
                {
                    lblMensajeGrilla.Text = Session["MensajeExito"].ToString();
                    lblMensajeGrilla.CssClass = "alert alert-success d-block text-center mb-3";
                    lblMensajeGrilla.Visible = true;

                    Session["MensajeExito"] = null;
                }
            }
        }

        protected void dgvMedico_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int idMedico = Convert.ToInt32(e.CommandArgument);

            if (e.CommandName == "Editar")
            {
                Response.Redirect("MedicoForm.aspx?id=" + idMedico);
            }
            else if (e.CommandName == "VerHorarios")
            {
                Response.Redirect("MedicoAgendaPag.aspx?idmedico=" + idMedico);
            }
            else if (e.CommandName == "Eliminar")
            {
                try
                {
                    MedicoNegocio negocio = new MedicoNegocio();
                    negocio.Eliminar(idMedico);

                    lblMensajeGrilla.ForeColor = System.Drawing.Color.Green;
                    lblMensajeGrilla.Text = "Médico eliminado con éxito.";
                    lblMensajeGrilla.Visible = true;

                    ActualizarGrillaMedico();
                }
                catch (Exception ex)
                {
                    lblMensajeGrilla.ForeColor = System.Drawing.Color.Red;
                    lblMensajeGrilla.Text = "Error al intentar eliminar: " + ex.Message;
                    lblMensajeGrilla.Visible = true;
                }
            }
        }

        protected void dgvMedico_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {

        }
        public void ActualizarGrillaMedico()
        {
            try
            {
                MedicoNegocio negocio = new MedicoNegocio();
                dgvMedico.DataSource = negocio.Listar();
                dgvMedico.DataBind();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}