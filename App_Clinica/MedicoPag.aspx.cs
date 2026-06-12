using ConexionBD;
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
    public partial class MedicoPag : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ActualizarGrillaMedico();


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

        protected void dgvMedico_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            // Recupero el ID de la columna seleccionada
            string ID = e.CommandArgument.ToString();

            if(e.CommandName == "Editar")
            {
                //Paso ID de la fila seleccionada
                Response.Redirect("MedicoForm.aspx?id=" + ID);
            
            }
            else if(e.CommandName == "Eliminar")
            {
                try
                {
                    MedicoNegocio negocio = new MedicoNegocio();
                    negocio.Eliminar(int.Parse(ID));
                    lblMensajeGrilla.ForeColor = System.Drawing.Color.Green;
                    lblMensajeGrilla.Text = "Medico eliminado con éxito.";
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
            else if (e.CommandName == "VerHorarios")
            {
                // Recupero el IdMedico que viene en el CommandArgument
                int IdMedico = Convert.ToInt32(e.CommandArgument);

                AgendaMedicoNegocio negocio = new AgendaMedicoNegocio();
                List<AgendaMedico> listaAgenda = negocio.ListarAgendaPorMedico(IdMedico);

                // Cargo 2da grilla y muestro
                dgvAgenda.DataSource = listaAgenda;
                dgvAgenda.DataBind();
                contenedorAgenda.Visible = true;
            }


        }

        protected void dgvMedico_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            dgvMedico.PageIndex = e.NewPageIndex;

            MedicoNegocio negocio = new MedicoNegocio();
            dgvMedico.DataSource = negocio.Listar();
            dgvMedico.DataBind();
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