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
    public partial class EspecialidadPag : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Seguridad.ValidarAcceso(this, "Administrador");

            if (!IsPostBack)
            {
                ActualizarGrilla();

                if (Session["MensajeExito"] != null)
                {
                    lblMensajeGrilla.Visible = true;
                    lblMensajeGrilla.Text = Session["MensajeExito"].ToString();
                    Session["MensajeExito"] = null;
                }
            }
        }

        private void ActualizarGrilla()
        {
            try
            {
                EspecialidadNegocio negocio = new EspecialidadNegocio();
                dgvEspecialidades.DataSource = negocio.Listar();
                dgvEspecialidades.DataBind();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        protected void dgvEspecialidades_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            string id = e.CommandArgument.ToString();

            if (e.CommandName == "Editar")
            {
                Response.Redirect("EspecialidadForm.aspx?id=" + id);
            }
            else if (e.CommandName == "Eliminar")
            {
                try
                {
                    EspecialidadNegocio negocio = new EspecialidadNegocio();
                    negocio.Eliminar(int.Parse(id));

                    lblMensajeGrilla.CssClass = "alert alert-success fade show d-block text-start w-100 mb-3";
                    lblMensajeGrilla.Text = "Especialidad eliminada con éxito.";
                    lblMensajeGrilla.Visible = true;

                    ActualizarGrilla();
                }
                catch (Exception ex)
                {
                    lblMensajeGrilla.CssClass = "alert alert-danger d-block text-start w-100 mb-3";
                    lblMensajeGrilla.Text = "Error al eliminar: " + ex.Message;
                    lblMensajeGrilla.Visible = true;
                }
            }
        }
    }
}