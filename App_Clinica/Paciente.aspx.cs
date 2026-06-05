using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Dominio;
using ConexionBD;
using Negocio;

namespace App_Clinica
{
    public partial class Paciente : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                PacienteNegocio pacienteNeg = new PacienteNegocio();
                dgvPaciente.DataSource = pacienteNeg.Listar();
                dgvPaciente.DataBind();
            }
          
        }

        protected void dgvPaciente_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Recupero el ID de la columna seleccionada
            var ID = dgvPaciente.SelectedDataKey.Value.ToString();
        }
    }
}