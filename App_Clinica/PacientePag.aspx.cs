using ConexionBD;
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
    public partial class PacientePag : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Seguridad.ValidarAcceso(this, "Administrador", "Recepcionista");

            if (!IsPostBack)
            {
                ActualizarGrillaPaciente();

                if (Session["MensajeExito"] != null)
                {
                    lblMensajeGrilla.Visible = true;
                    lblMensajeGrilla.Text = Session["MensajeExito"].ToString();
                    lblMensajeGrilla.ForeColor = System.Drawing.Color.Green;
                    Session["MensajeExito"] = null;
                }
            }
        }

        protected void dgvPaciente_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Page") return; // Evita interferencias con la paginación nativa

            string ID = e.CommandArgument.ToString();

            if (e.CommandName == "Editar")
            {
                Response.Redirect("PacienteForm.aspx?id=" + ID);
            }
            else if (e.CommandName == "Eliminar")
            {
                try
                {
                    PacienteNegocio negocio = new PacienteNegocio();
                    negocio.Eliminar(int.Parse(ID));
                    lblMensajeGrilla.ForeColor = System.Drawing.Color.Green;
                    lblMensajeGrilla.Text = "Paciente eliminado con éxito.";
                    lblMensajeGrilla.Visible = true;

                    ActualizarGrillaPaciente();
                }
                catch (Exception ex)
                {
                    lblMensajeGrilla.ForeColor = System.Drawing.Color.Red;
                    lblMensajeGrilla.Text = "Error al intentar eliminar: " + ex.Message;
                    lblMensajeGrilla.Visible = true;
                }
            }
        }

        public void ActualizarGrillaPaciente()
        {
            try
            {
                PacienteNegocio negocio = new PacienteNegocio();

                string dni = txtFiltroDni.Text.Trim();
                string nombreApellido = txtFiltroNombre.Text.Trim();

                // Le pasamos los filtros directamente al método Listar
                dgvPaciente.DataSource = negocio.Listar(dni, nombreApellido);
                dgvPaciente.DataBind();
            }
            catch (Exception ex)
            {
                lblMensajeGrilla.ForeColor = System.Drawing.Color.Red;
                lblMensajeGrilla.Text = "Error al cargar datos: " + ex.Message;
                lblMensajeGrilla.Visible = true;
            }
        }

        protected void txtFiltro_TextChanged(object sender, EventArgs e)
        {
            dgvPaciente.PageIndex = 0; // Reinicia a la primera página al filtrar
            ActualizarGrillaPaciente();
        }

        protected void btnLimpiarFiltros_Click(object sender, EventArgs e)
        {
            txtFiltroDni.Text = string.Empty;
            txtFiltroNombre.Text = string.Empty;
            dgvPaciente.PageIndex = 0;
            ActualizarGrillaPaciente();
        }

        protected void dgvPaciente_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            dgvPaciente.PageIndex = e.NewPageIndex;
            ActualizarGrillaPaciente();
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            dgvPaciente.PageIndex = 0; 
            ActualizarGrillaPaciente();
        }
    }
}