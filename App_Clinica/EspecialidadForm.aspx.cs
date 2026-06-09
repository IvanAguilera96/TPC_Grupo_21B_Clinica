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
    public partial class EspecialidadForm : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    //Si viene ID, es edición
                    if (Request.QueryString["id"] != null)
                    {
                        int idUrl = int.Parse(Request.QueryString["id"]);
                        EspecialidadNegocio negocio = new EspecialidadNegocio();
                        Especialidad seleccionado = negocio.BuscarPorId(idUrl);

                        if (seleccionado != null)
                        {
                            txtDescripcion.Text = seleccionado.Descripcion;
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
            if (string.IsNullOrWhiteSpace(txtDescripcion.Text))
            {
                lblMensaje.ForeColor = System.Drawing.Color.Red;
                lblMensaje.Text = "Debe ingresar una descripción para continuar.";
                return;
            }

            try
            {
                Especialidad nueva = new Especialidad();
                nueva.Descripcion = txtDescripcion.Text;

                //Asigna el id para que viaje al metodo modificar
                if (Request.QueryString["id"] != null)
                {
                    nueva.IdEspecialidad = int.Parse(Request.QueryString["id"]);
                }

                EspecialidadNegocio negocio = new EspecialidadNegocio();

                if (Request.QueryString["id"] != null)
                {
                    negocio.Modificar(nueva);
                    Session["MensajeExito"] = "Especialidad modificada con éxito.";
                }
                else
                {
                    negocio.Agregar(nueva);
                    Session["MensajeExito"] = "Especialidad registrada con éxito.";
                }

                Response.Redirect("EspecialidadPag.aspx", false);
            }
            catch (Exception ex)
            {
                lblMensaje.ForeColor = System.Drawing.Color.Red;
                lblMensaje.Text = "Error: " + ex.Message;
            }
        }
    }
}