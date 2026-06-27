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

                //Valida si debe mostrar mensaje guardado en Session
                if (Session["MensajeExito"] != null)
                {
                    lblMensajeGrilla.Visible = true;

                    //Asigna mensaje almacenado en Session
                    lblMensajeGrilla.Text = Session["MensajeExito"].ToString();
                    lblMensajeGrilla.ForeColor = System.Drawing.Color.Green;

                    //Limpia el mensaje
                    Session["MensajeExito"] = null;
                }
            }
        }

        protected void dgvPaciente_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            // Recupero el ID de la columna seleccionada
            string ID = e.CommandArgument.ToString();

            if(e.CommandName == "Editar")
            {
                //Paso ID de la fila seleccionada
                Response.Redirect("PacienteForm.aspx?id=" + ID);
            }
            else if(e.CommandName == "Eliminar")
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
                dgvPaciente.DataSource = negocio.Listar();
                dgvPaciente.DataBind();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void dgvPaciente_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            dgvPaciente.PageIndex = e.NewPageIndex;

            PacienteNegocio negocio = new PacienteNegocio();
            dgvPaciente.DataSource = negocio.Listar();
            dgvPaciente.DataBind();
        }
    }
}