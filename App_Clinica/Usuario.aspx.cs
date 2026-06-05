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

        protected void dgvUsuario_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            string idUsuario = e.CommandArgument.ToString();

            if (e.CommandName == "Editar")
            {
                Response.Redirect("UsuarioForm.aspx?id=" + idUsuario);
            }
            else if (e.CommandName == "Eliminar")
            {
                try
                {
                    UsuarioNegocio negocio = new UsuarioNegocio();
                    negocio.Eliminar(int.Parse(idUsuario));

                    lblMensajeGrilla.ForeColor = System.Drawing.Color.Green;
                    lblMensajeGrilla.Text = "Usuario eliminado con éxito.";
                    lblMensajeGrilla.Visible = true;

                    ActualizarGrillaUsuarios();
                }
                catch (Exception ex)
                {
                    lblMensajeGrilla.ForeColor = System.Drawing.Color.Red;
                    lblMensajeGrilla.Text = "Error al intentar eliminar: " + ex.Message;
                    lblMensajeGrilla.Visible = true;
                }
            }
        }
    }
}