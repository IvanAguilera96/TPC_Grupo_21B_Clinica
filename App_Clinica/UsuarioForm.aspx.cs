using App_Clinica;
using Dominio;
using Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Utiles;
using static Utiles.Utils;

namespace App_Clinica
{
    public partial class UsuarioForm : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                Seguridad.ValidarAcceso(this, "Administrador");

                if (!IsPostBack)
                {
                    PerfilNegocio negocioPerfil = new PerfilNegocio();
                    UsuarioNegocio negocioUsuario = new UsuarioNegocio();
                    MedicoNegocio negocioMedico = new MedicoNegocio();

                    // 1. Cargar Perfiles
                    ddlPerfil.DataSource = negocioPerfil.Listar();
                    ddlPerfil.DataValueField = "IdPerfil";
                    ddlPerfil.DataTextField = "Descripcion";
                    ddlPerfil.DataBind();

                    // 2. Cargar Médicos Activos para el campo opcional
                    ddlMedico.DataSource = negocioMedico.Listar().Where(x => x.Estado == true).ToList();
                    ddlMedico.DataValueField = "IdMedico";
                    ddlMedico.DataTextField = "NombreCompleto";
                    ddlMedico.DataBind();
                    ddlMedico.Items.Insert(0, new ListItem("-- Seleccione un Médico --", "0"));

                    // 3. Configuración si recibe ID (Modificar)
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

                            // Si tiene médico asignado lo seleccionamos en el combo
                            if (seleccionado.Medico != null && seleccionado.Medico.IdMedico > 0)
                            {
                                ddlMedico.SelectedValue = seleccionado.Medico.IdMedico.ToString();
                            }
                        }

                        lblTitulo.InnerText = "Modificar Usuario";
                    }
                }
            }
            catch (Exception ex)
            {
                Utils.MostrarAlertaModal(this, "Error al iniciar el formulario: " + ex.Message);
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

            // Validación si seleccionó perfil médico pero no enlazó ningún profesional
            if (ddlPerfil.SelectedValue == "3" && ddlMedico.SelectedValue == "0")
            {
                Utils.MostrarAlertaModal(this, "Debe seleccionar un profesional médico para asignarle este usuario.");
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

                // Lógica de asignación de médico
                if (ddlPerfil.SelectedValue == "3" && ddlMedico.SelectedValue != "0")
                {
                    nuevo.Medico = new Medico();
                    nuevo.Medico.IdMedico = int.Parse(ddlMedico.SelectedValue);
                }
                else
                {
                    nuevo.Medico = null; // No corresponde médico o no se eligió ninguno
                }

                if (Request.QueryString["id"] != null)
                {
                    nuevo.IdUsuario = int.Parse(Request.QueryString["id"]);
                }

                UsuarioNegocio negocio = new UsuarioNegocio();

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