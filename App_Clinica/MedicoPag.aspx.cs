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