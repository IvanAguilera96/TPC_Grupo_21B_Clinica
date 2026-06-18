using Dominio;
using Negocio;
using System;
using App_Clinica;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace App_Clinica
{
    public partial class UsuarioForm : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
                try
                {   
                    //Configuración inicial
                    if (!IsPostBack)
                    {
                        PerfilNegocio negocioPerfil = new PerfilNegocio();
                        UsuarioNegocio negocioUsuario = new UsuarioNegocio();

                        ddlPerfil.DataSource = negocioPerfil.Listar();
                        ddlPerfil.DataValueField = "IdPerfil";
                        ddlPerfil.DataTextField = "Descripcion";
                        ddlPerfil.DataBind();

                    //Configuración si recibe ID (modificar)
                    if (Request.QueryString["id"] != null)
                    {
                        int idUrl = int.Parse(Request.QueryString["id"]);

                        Dominio.Usuario seleccionado = negocioUsuario.BuscarPorId(idUrl);

                        if (seleccionado != null)
                        {
                            txtNombre.Text = seleccionado.Nombre;
                            txtContrasenia.Text = seleccionado.Contrasenia;
                            ddlPerfil.SelectedValue = seleccionado.Perfil.IdPerfil.ToString();
                            chkEstado.Checked = seleccionado.Estado;
                        }
                    }
                }

                }
                catch (Exception ex)
                {
                    throw ex;
                }
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNombre.Text) || string.IsNullOrWhiteSpace(txtContrasenia.Text))
            {
                Utils.MostrarAlertaModal(this, "El nombre de usuario y la contraseña son obligatorios.");
                return;
            }

            if (txtContrasenia.Text.Length < 6)
            {
                Utils.MostrarAlertaModal(this, "La contraseña debe tener al menos 6 caracteres.");
                return;
            }

            try
            {
                Dominio.Usuario nuevo = new Dominio.Usuario();
                nuevo.Nombre = txtNombre.Text;
                nuevo.Contrasenia = txtContrasenia.Text;
                nuevo.Estado = chkEstado.Checked;

                nuevo.Perfil = new Perfil();
                nuevo.Perfil.IdPerfil = int.Parse(ddlPerfil.SelectedValue);

                //Asigna el id para que viaje al metodo modificar
                if (Request.QueryString["id"] != null)
                {
                    nuevo.IdUsuario = int.Parse(Request.QueryString["id"]);
                }

                UsuarioNegocio negocio = new UsuarioNegocio();

                //Evalua si es alta o editar
                if (Request.QueryString["id"] != null)
                {
                    negocio.Modificar(nuevo);
                    Session["MensajeExito"] = "Usuario modificado con éxito.";
                }
                else
                {
                    negocio.Agregar(nuevo);
                    Session["MensajeExito"] = "Usuario registrado con éxito.";
                }
                
                Response.Redirect("Usuario.aspx", false);
            }
            catch (Exception ex)
            {

                Utils.MostrarAlertaModal(this, ex.Message);
            }
        }
    }
}