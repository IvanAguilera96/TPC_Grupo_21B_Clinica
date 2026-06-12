using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Dominio;
using Negocio;

namespace App_Clinica
{
    public partial class MedicoForm : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Si obtengo ID por la url guardo ID (Quiere modificar)
                if (Request.QueryString["ID"] != null)
                {
                    int IdMedico = int.Parse(Request.QueryString["ID"]);
                    MedicoNegocio negocio = new MedicoNegocio();
                    Medico medico = new Medico();

                    medico = negocio.BuscarMedico(IdMedico);
                    if (medico != null)
                    {
                        txtDni.Text = medico.Dni;
                        txtApellido.Text = medico.Apellido;
                        txtNombre.Text = medico.Nombre;
                        txtMatricula.Text = medico.Matricula.ToString();
                        chkEstado.Checked = medico.Estado;
                    }
                }
            }
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            MedicoNegocio negocio = new MedicoNegocio();
            Medico aux = new Medico();

            try
            {
                aux.Dni = txtDni.Text;
                aux.Nombre = txtNombre.Text;
                aux.Apellido = txtApellido.Text;
                aux.Matricula = int.Parse(txtMatricula.Text);
                aux.Estado = chkEstado.Checked;

                if (Request.QueryString["ID"] != null)
                {
                    aux.IdMedico = Convert.ToInt32(Request.QueryString["ID"]);
                    negocio.Modificar(aux);
                    Session["MensajeExito"] = "Medico modificado con éxito.";
                }
                else
                {
                    negocio.Agregar(aux);
                    Session["MensajeExito"] = "Medico registrado con éxito.";
                }
                
                Response.Redirect("MedicoPag.aspx", false);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }
}