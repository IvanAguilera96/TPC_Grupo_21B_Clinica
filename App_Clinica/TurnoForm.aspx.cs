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
    public partial class TurnoForm : System.Web.UI.Page
    {
        private TurnoNegocio turnoNegocio = new TurnoNegocio();

        protected void Page_Load(object sender, EventArgs e)
        {
            Seguridad.ValidarAcceso(this, "Administrador", "Recepcionista");

            if (!IsPostBack)
            {
                CargarEspecialidades();

                // Si viene un Paciente sugerido desde el Modal de Historial
                if (Request.QueryString["pacienteId"] != null)
                {
                    int idSugerido = int.Parse(Request.QueryString["pacienteId"]);
                    CargarPacientePorIdDirecto(idSugerido);
                }

                // Si viene por Reprogramar
                if (Request.QueryString["reprogramar"] != null)
                {
                    int IdTurnoViejo = int.Parse(Request.QueryString["reprogramar"]);
                    ViewState["IdTurnoAReprogramar"] = IdTurnoViejo;

                    h3TituloTurno.InnerText = "Reprogramar Turno";
                    PrecargarDatosTurnoViejo(IdTurnoViejo);
                }
            }
        }

        // EVENTO PARA BUSCAR AL PACIENTE POR DNI
        protected void btnBuscarPaciente_Click(object sender, EventArgs e)
        {
            string dniElegido = txtDniPaciente.Text.Trim();

            if (string.IsNullOrEmpty(dniElegido))
            {
                Utils.MostrarAlertaModal(this, "Debe ingresar un número de DNI.");
                ResetearControlesPaciente();
                return;
            }

            try
            {
                PacienteNegocio negocio = new PacienteNegocio();
                Paciente paciente = negocio.BuscarPorDni(dniElegido);

                if (paciente != null)
                {
                    lblNombrePaciente.Text = $"✅ {paciente.NombreCompleto}";
                    lblNombrePaciente.CssClass = "fw-bold text-success small";
                    hfIdPaciente.Value = paciente.IdPaciente.ToString();
                }
                else
                {
                    lblNombrePaciente.Text = "❌ Paciente no registrado.";
                    lblNombrePaciente.CssClass = "fw-bold text-danger small";
                    hfIdPaciente.Value = "";
                    Utils.MostrarAlertaModal(this, "No se encontró ningún paciente con el DNI ingresado.");
                }
            }
            catch (Exception ex)
            {
                Utils.MostrarAlertaModal(this, "Error al buscar el paciente: " + ex.Message);
            }
        }

        private void CargarPacientePorIdDirecto(int idPaciente)
        {
            try
            {
                PacienteNegocio negocio = new PacienteNegocio();
                Paciente paciente = negocio.Listar().FirstOrDefault(x => x.IdPaciente == idPaciente);
                if (paciente != null)
                {
                    txtDniPaciente.Text = paciente.Dni;
                    lblNombrePaciente.Text = $"✅ {paciente.NombreCompleto}";
                    lblNombrePaciente.CssClass = "fw-bold text-success small";
                    hfIdPaciente.Value = paciente.IdPaciente.ToString();

                    // Bloqueamos la edición si vino sugerido imperativamente del historial
                    txtDniPaciente.Enabled = false;
                    btnBuscarPaciente.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                Utils.MostrarAlertaModal(this, "Error al cargar paciente sugerido: " + ex.Message);
            }
        }

        // AL CAMBIAR ESPECIALIDAD -> FILTRAMOS MEDICOS
        protected void ddlEspecialidad_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtFecha.Text = null;
            int IdEspecialidad = int.Parse(ddlEspecialidad.SelectedValue);
            if (IdEspecialidad > 0)
            {
                MedicoNegocio negocio = new MedicoNegocio();
                ddlMedico.DataSource = negocio.ListarMedicoPorEspecialidad(IdEspecialidad);
                ddlMedico.DataValueField = "IdMedico";
                ddlMedico.DataTextField = "NombreCompleto";
                ddlMedico.DataBind();
                ddlMedico.Items.Insert(0, new ListItem("Seleccione un médico...", "0"));

                ddlMedico.Enabled = true;
            }
            else
            {
                ResetearControlesMedicos();
            }
            ActualizarAvisoDiasAtencion();
            LimpiarSlots();
        }

        // AL CAMBIAR MEDICO -> REVISAMOS SI YA HABIA UNA FECHA CARGADA
        protected void ddlMedico_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtFecha.Text = null;
            ActualizarAvisoDiasAtencion();

            LimpiarSlots();
            if (ddlMedico.SelectedValue != "0" && !string.IsNullOrEmpty(txtFecha.Text))
            {
                GenerarBloquesHorarios();
            }
        }

        // AL CAMBIAR LA FECHA -> GENERAMOS LOS RECTANGULOS DE TIEMPO
        protected void txtFecha_TextChanged(object sender, EventArgs e)
        {
            hfHoraSeleccionada.Value = "";
            GenerarBloquesHorarios();
        }

        private void GenerarBloquesHorarios()
        {
            int idMedico = int.Parse(ddlMedico.SelectedValue ?? "0");
            int idEspecialidad = int.Parse(ddlEspecialidad.SelectedValue ?? "0");

            if (idMedico == 0 || idEspecialidad == 0 || string.IsNullOrEmpty(txtFecha.Text))
            {
                LimpiarSlots();
                return;
            }

            DateTime fechaElegida = DateTime.Parse(txtFecha.Text);

            if (fechaElegida.Date < DateTime.Today)
            {
                Utils.MostrarAlertaModal(this, "No se pueden asignar turnos para fechas pasadas.");
                LimpiarSlots();
                return;
            }

            AgendaMedicoNegocio agendaNegocio = new AgendaMedicoNegocio();
            var agendas = agendaNegocio.ListarAgendasPorMedico(idMedico, idEspecialidad);

            string diaSemana = TraducirDiaSemana(fechaElegida.DayOfWeek);
            var agendaDelDia = agendas.Find(x => x.TurnoTrabajo.DiaDeTrabajo.ToLower() == diaSemana.ToLower());

            if (agendaDelDia == null)
            {
                Utils.MostrarAlertaModal(this, $"El profesional no atiende los días {diaSemana}.");
                LimpiarSlots();
                return;
            }

            ViewState["IdAgendaMedicoElegida"] = agendaDelDia.IdAgendaMedico;

            List<string> horasOcupadas = turnoNegocio.ObtenerHorasOcupadas(idMedico, fechaElegida.ToString("yyyy-MM-dd"));
            ViewState["HorasOcupadas"] = horasOcupadas;

            List<string> listaSlots = new List<string>();
            TimeSpan inicio = agendaDelDia.TurnoTrabajo.HoraEntrada;
            TimeSpan fin = agendaDelDia.TurnoTrabajo.HoraSalida;
            TimeSpan intervalo = TimeSpan.FromMinutes(30);

            while (inicio < fin)
            {
                listaSlots.Add(inicio.ToString(@"hh\:mm\:ss"));
                inicio = inicio.Add(intervalo);
            }

            ViewState["SlotsGenerados"] = listaSlots;

            repHorarios.DataSource = listaSlots;
            repHorarios.DataBind();
        }

        protected void repHorarios_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "SeleccionarHora")
            {
                hfHoraSeleccionada.Value = e.CommandArgument.ToString();

                var listaSlots = ViewState["SlotsGenerados"] as List<string>;
                repHorarios.DataSource = listaSlots;
                repHorarios.DataBind();
            }
        }

        public bool EsTurnoOcupado(string horaSlot)
        {
            var ocupadas = ViewState["HorasOcupadas"] as List<string> ?? new List<string>();
            return ocupadas.Contains(horaSlot);
        }

        public string ValidarEstiloSlot(string horaSlot)
        {
            if (EsTurnoOcupado(horaSlot))
            {
                return "btn btn-outline-secondary w-100 disabled";
            }

            if (hfHoraSeleccionada.Value == horaSlot)
            {
                return "btn btn-success w-100 fw-bold";
            }

            return "btn btn-outline-primary w-100";
        }

        // CONFIRMAR ALTA / REPROGRAMACIÓN
        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                if (ddlEspecialidad.SelectedValue == "0")
                {
                    Utils.MostrarAlertaModal(this, "Debe seleccionar una Especialidad.");
                    return;
                }

                if (ddlMedico.SelectedValue == "0")
                {
                    Utils.MostrarAlertaModal(this, "Debe seleccionar un Medico.");
                    return;
                }

                // Validacion, Revisar si el hidden de paciente esta vacio
                if (string.IsNullOrEmpty(hfIdPaciente.Value))
                {
                    Utils.MostrarAlertaModal(this, "Debe buscar y seleccionar un Paciente válido ingresando su DNI.");
                    return;
                }

                if (string.IsNullOrEmpty(hfHoraSeleccionada.Value))
                {
                    Utils.MostrarAlertaModal(this, "Debe seleccionar un bloque de horario disponible.");
                    return;
                }

                Turno nuevo = new Turno();
                nuevo.Fecha = DateTime.Parse(txtFecha.Text);
                nuevo.Hora = TimeSpan.Parse(hfHoraSeleccionada.Value);
                nuevo.Observacion = txtObservacion.Text;
                nuevo.Diagnostico = "";

                nuevo.Paciente = new Paciente();
                nuevo.Paciente.IdPaciente = int.Parse(hfIdPaciente.Value); // Leemos el HiddenField cambiado

                nuevo.Agenda = new AgendaMedico();
                nuevo.Agenda.IdAgendaMedico = (int)ViewState["IdAgendaMedicoElegida"];

                int idNuevoTurno = 0;

                if (ViewState["IdTurnoAReprogramar"] != null)
                {
                    int idViejo = (int)ViewState["IdTurnoAReprogramar"];

                    idNuevoTurno = turnoNegocio.Agregar(nuevo);
                    turnoNegocio.CambiarEstado(idViejo, 3);

                    Session["MensajeExito"] = "¡El turno se reprogramó con éxito!";
                }
                else
                {
                    idNuevoTurno = turnoNegocio.Agregar(nuevo);
                    Session["MensajeExito"] = "¡Turno agendado con éxito!";
                }

                try
                {
                    // Cambiado para extraer el nombre desde el Label informativo
                    string nombrePaciente = lblNombrePaciente.Text.Replace("✅ ", "");
                    string nombreMedico = ddlMedico.SelectedItem.Text;

                    PacienteNegocio negocio = new PacienteNegocio();
                    string emailPaciente = "";

                    if (!string.IsNullOrEmpty(emailPaciente))
                    {
                        EmailService emailService = new EmailService();
                        emailService.EnviarConfirmacionTurno(
                                                            emailPaciente,
                                                            nombrePaciente,
                                                            idNuevoTurno.ToString(),
                                                            nuevo.Fecha.ToString("dd/MM/yyyy"),
                                                            nuevo.Hora.ToString(@"hh\:mm"),
                                                            nombreMedico);
                    }
                }
                catch (Exception)
                {
                    // Manejo silencioso de envío de correo electrónico
                }

                Response.Redirect("TurnosPag.aspx", false);
            }
            catch (Exception ex)
            {
                Utils.MostrarAlertaModal(this, "Error al confirmar el turno: " + ex.Message);
            }
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("TurnosPag.aspx");
        }

        private void CargarEspecialidades()
        {
            EspecialidadNegocio negocio = new EspecialidadNegocio();
            ddlEspecialidad.DataSource = negocio.Listar();
            ddlEspecialidad.DataValueField = "IdEspecialidad";
            ddlEspecialidad.DataTextField = "Descripcion";
            ddlEspecialidad.DataBind();
            ddlEspecialidad.Items.Insert(0, new ListItem("Seleccione una especialidad...", "0"));
        }

        private void LimpiarSlots()
        {
            repHorarios.DataSource = null;
            repHorarios.DataBind();
            hfHoraSeleccionada.Value = "";
            ViewState["SlotsGenerados"] = null;
            ViewState["HorasOcupadas"] = null;
        }

        private void ResetearControlesMedicos()
        {
            ddlMedico.Enabled = false;
            ddlMedico.Items.Clear();
            txtFecha.Text = null;
        }

        private void ResetearControlesPaciente()
        {
            lblNombrePaciente.Text = "Ninguno (Ingrese un DNI y busque)";
            lblNombrePaciente.CssClass = "fw-bold text-dark small";
            hfIdPaciente.Value = "";
        }

        private string TraducirDiaSemana(DayOfWeek day)
        {
            switch (day)
            {
                case DayOfWeek.Monday: return "Lunes";
                case DayOfWeek.Tuesday: return "Martes";
                case DayOfWeek.Wednesday: return "Miércoles";
                case DayOfWeek.Thursday: return "Jueves";
                case DayOfWeek.Friday: return "Viernes";
                case DayOfWeek.Saturday: return "Sábado";
                default: return "Domingo";
            }
        }

        private void ActualizarAvisoDiasAtencion()
        {
            if (ddlEspecialidad.SelectedValue == "0" || ddlMedico.SelectedValue == "0")
            {
                lblDiasAtencion.Text = "";
                return;
            }

            int idMedico = int.Parse(ddlMedico.SelectedValue);
            int idEspecialidad = int.Parse(ddlEspecialidad.SelectedValue);

            AgendaMedicoNegocio agendaNegocio = new AgendaMedicoNegocio();
            var agendas = agendaNegocio.ListarAgendasPorMedico(idMedico, idEspecialidad);

            if (agendas != null && agendas.Count > 0)
            {
                var diasYHorarios = agendas.Select(x => $"{x.TurnoTrabajo.DiaDeTrabajo} ({x.TurnoTrabajo.HoraEntrada.ToString(@"hh\:mm")} a {x.TurnoTrabajo.HoraSalida.ToString(@"hh\:mm")} hs)");
                string textoDias = string.Join(", ", diasYHorarios);
                lblDiasAtencion.Text = $"💡 Este profesional atiende los días: {textoDias}.";
            }
            else
            {
                lblDiasAtencion.Text = "⚠️ El profesional no posee horarios configurados para esta especialidad.";
            }
        }

        private void PrecargarDatosTurnoViejo(int idTurno)
        {
            try
            {
                Turno turnoViejo = turnoNegocio.BuscarPorId(idTurno);

                if (turnoViejo != null)
                {
                    ddlEspecialidad.SelectedValue = turnoViejo.Agenda.Especialidad.IdEspecialidad.ToString();
                    ddlEspecialidad_SelectedIndexChanged(null, null);

                    ddlMedico.SelectedValue = turnoViejo.Agenda.Medico.IdMedico.ToString();

                    // PRECARGA DEL PACIENTE MEDIANTE SUS NUEVOS CONTROLES DE DNI
                    if (turnoViejo.Paciente != null)
                    {
                        txtDniPaciente.Text = turnoViejo.Paciente.Dni;
                        lblNombrePaciente.Text = $"✅ {turnoViejo.Paciente.NombreCompleto}";
                        lblNombrePaciente.CssClass = "fw-bold text-success small";
                        hfIdPaciente.Value = turnoViejo.Paciente.IdPaciente.ToString();
                    }

                    txtObservacion.Text = turnoViejo.Observacion;
                    ActualizarAvisoDiasAtencion();
                }
            }
            catch (Exception ex)
            {
                Utils.MostrarAlertaModal(this, "Error al recuperar el turno para reprogramar: " + ex.Message);
            }
        }
    }
}