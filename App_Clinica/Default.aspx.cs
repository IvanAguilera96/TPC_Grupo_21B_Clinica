using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace App_Clinica
{
    public partial class Default1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //Lógica para recuperar el usuario logueado

                //Evaluar según la descripción del Perfil
                string userLogueado = "Administrador";

                switch (userLogueado)
                {
                    case "Administrador":
                        CargarDashboardAdmin();
                        break;

                    case "Recepcionista":
                        CargarDashboardRecepcion();
                        break;

                    case "Medico":
                        CargarDashboardMedico();
                        break;

                    default:
                        break;
                }
            }
        }
        private void CargarDashboardAdmin()
        {
            pnlAdmin.Visible = true;
        }

        private void CargarDashboardRecepcion()
        {
            pnlRecepcion.Visible = true;        
        }

        private void CargarDashboardMedico()
        {
            pnlMedico.Visible = true;
        }
    }   
}