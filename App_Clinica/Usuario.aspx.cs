using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Negocio;
using Dominio;

namespace App_Clinica
{
    public partial class Usuario : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ActualizarGrillaUsuarios();
            }
        }

        private void ActualizarGrillaUsuarios()
        {
            try
            {
                UsuarioNegocio negocio = new UsuarioNegocio();

                dgvUsuario.DataSource = negocio.Listar();
                dgvUsuario.DataBind();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            if(string.IsNullOrWhiteSpace(txtNombre.Text) || string.IsNullOrWhiteSpace(txtContrasenia.Text))
            {
                lblMensaje.ForeColor = System.Drawing.Color.Red;
                lblMensaje.Text = "Debe completar todos los campos para continuar.";
            }

            try
            {

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}