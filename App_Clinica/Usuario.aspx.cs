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
            //Si presionan Editar:
            if (e.CommandName == "Editar")
            {
                //Captura el ID para enviar al formulario
                string id = e.CommandArgument.ToString();

                Response.Redirect("UsuarioForm.aspx?id=" + id);
            }
            else if (e.CommandName == "Eliminar")
            {
                try
                {
                    int IdEliminar = int.Parse(e.CommandArgument.ToString());

                    UsuarioNegocio negocio = new UsuarioNegocio();

                    negocio.ELiminar(IdEliminar);

                    lblMensajeGrilla.ForeColor = System.Drawing.Color.Green;
                    lblMensajeGrilla.Text = "Usuario eliminado con éxito.";
                    lblMensajeGrilla.Visible = true;

                    ActualizarGrillaUsuarios();
                }
                catch (Exception ex)
                {
                    lblMensajeGrilla.ForeColor = System.Drawing.Color.Red;
                    lblMensajeGrilla.Text = "Error eliminando el usuario.";
                    lblMensajeGrilla.Visible = true;
                }
            }
        }
    }
}