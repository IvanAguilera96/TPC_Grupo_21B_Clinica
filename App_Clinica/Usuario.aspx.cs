using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Negocio;

namespace App_Clinica
{
    public partial class Usuario : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    UsuarioNegocio negocio = new UsuarioNegocio();

                    dgvUsuario.DataSource = negocio.Listar();
                    dgvUsuario.DataBind();
                }
                catch (Exception ex )
                {

                    throw ex;
                }
            }
        }
    }
}