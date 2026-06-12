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
            }
        }

        protected void dgvMedico_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if(e.CommandName == "VerHorarios")
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