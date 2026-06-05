using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace App_Clinica
{
    public partial class PacienteForm : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Si obtengo ID por la url guardo ID
            if (Request.QueryString["ID"] != null)
            {
                int IdPaciente = int.Parse(Request.QueryString["ID"].ToString());

            }
        }
    }
}