using Dominio;
using Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Utiles;

namespace App_Clinica
{
    public partial class TurnoForm : System.Web.UI.Page
    {
        private TurnoNegocio turnoNegocio = new TurnoNegocio();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarEspecialidades();
                CargarPacientes();

                // Si viene por Reprogramnar
                if (Request.QueryString["reprogramar"] != null)
                {
                    int IdTurnoViejo = int.Parse(Request.QueryString["reprogramar"]);
                    // Guardamos el ID en el ViewState para tenerlo a mano al guardar
                    ViewState["IdTurnoAReprogramar"] = IdTurnoViejo;

                    // Cambiamos el título de la tarjeta para avisar al usuario

                    h3TituloTurno.InnerText = "Reprogramar Turno";
                    PrecargarDatosTurnoViejo(IdTurnoViejo);
                }
            }
        }

        // AL CAMBIAR ESPECIALIDAD -> FILTRAMOS MEDICOS
        protected void ddlEspecialidad_SelectedIndexChanged(object sender, EventArgs e)
        {
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
            // Si cambian la especialidad, limpiamos los horarios anteriores
            LimpiarSlots();
        }

        // AL CAMBIAR MEDICO -> RE-EVALUAMOS SI YA HABIA UNA FECHA CARGADA
        protected void ddlMedico_SelectedIndexChanged(object sender, EventArgs e)
        {
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
            hfHoraSeleccionada.Value = ""; // Si cambia la fecha, reseteamos la hora elegida anterior
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

            // Validamos que no busquen una fecha pasada
            if (fechaElegida.Date < DateTime.Today)
            {
                Utils.MostrarAlertaModal(this, "No se pueden asignar turnos para fechas pasadas.");
                LimpiarSlots();
                return;
            }

            // Buscamos las agendas asignadas al médico para esa especialidad
            AgendaMedicoNegocio agendaNegocio = new AgendaMedicoNegocio();
            var agendas = agendaNegocio.ListarAgendasPorMedico(idMedico, idEspecialidad);

            // Mapeamos el día de la semana al string correspondiente
            string diaSemana = TraducirDiaSemana(fechaElegida.DayOfWeek);
            var agendaDelDia = agendas.Find(x => x.TurnoTrabajo.DiaDeTrabajo.ToLower() == diaSemana.ToLower());

            if (agendaDelDia == null)
            {
                Utils.MostrarAlertaModal(this, $"El profesional no atiende los días {diaSemana}.");
                LimpiarSlots();
                return;
            }

            // Guardamos el ID de la agenda para recuperarlo al guardar el turno
            ViewState["IdAgendaMedicoElegida"] = agendaDelDia.IdAgendaMedico;

            // Buscamos los turnos ya ocupados en la base de datos para esa fecha
            List<string> horasOcupadas = turnoNegocio.ObtenerHorasOcupadas(idMedico, fechaElegida.ToString("yyyy-MM-dd"));
            ViewState["HorasOcupadas"] = horasOcupadas;

            // Generamos los intervalos de 30 minutos secuenciales
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

            // Enlazamos al Repeater para pintar los botones
            repHorarios.DataSource = listaSlots;
            repHorarios.DataBind();
        }

        // MANEJO DE CLICKS EN LOS RECTANGULOS DEL REPEATER
        protected void repHorarios_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "SeleccionarHora")
            {
                // Guardamos la hora en el HiddenField
                hfHoraSeleccionada.Value = e.CommandArgument.ToString();

                // Refrescamos el repeater para aplicar el color verde al botón seleccionado
                var listaSlots = ViewState["SlotsGenerados"] as List<string>;
                repHorarios.DataSource = listaSlots;
                repHorarios.DataBind();
            }
        }

        // METODOS AUXILIARES QUE UTILIZA EL REPEATER DESDE EL HTML
        public bool EsTurnoOcupado(string horaSlot)
        {
            var ocupadas = ViewState["HorasOcupadas"] as List<string> ?? new List<string>();
            return ocupadas.Contains(horaSlot);
        }

        public string ValidarEstiloSlot(string horaSlot)
        {
            if (EsTurnoOcupado(horaSlot))
            {
                return "btn btn-outline-secondary w-100 disabled"; // Gris/Blanco si está ocupado
            }

            if (hfHoraSeleccionada.Value == horaSlot)
            {
                return "btn btn-success w-100 fw-bold"; // Verde llamativo si se seleccionó
            }

            return "btn btn-outline-primary w-100"; // Azul estándar para disponibles
        }

        //(CONFIRMAR ALTA)
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

                if (ddlPaciente.SelectedValue == "0")
                {
                    Utils.MostrarAlertaModal(this, "Debe seleccionar un Paciente.");
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
                nuevo.Paciente.IdPaciente = int.Parse(ddlPaciente.SelectedValue);

                nuevo.Agenda = new AgendaMedico();
                nuevo.Agenda.IdAgendaMedico = (int)ViewState["IdAgendaMedicoElegida"];

                int idNuevoTurno = 0;

                if (ViewState["IdTurnoAReprogramar"] != null)
                {
                    int idViejo = (int)ViewState["IdTurnoAReprogramar"];

                    // Insertamos el nuevo, y al viejo le hacemos un UPDATE cambiando el estado a "Reprogramado"
                    idNuevoTurno = turnoNegocio.Agregar(nuevo); 
                    turnoNegocio.CambiarEstado(idViejo, 4); // 4 es "Reprogramado" en la tabla EstadoTurno

                    Session["MensajeExito"] = "¡El turno se reprogramó con éxito!";
                }
                else
                {
                    // Flujo normal de un alta de turnos
                    idNuevoTurno = turnoNegocio.Agregar(nuevo);
                    Session["MensajeExito"] = "¡Turno agendado con éxito!";
                }

                try
                {

                    string nombrePaciente = ddlPaciente.SelectedItem.Text;
                    string nombreMedico = ddlMedico.SelectedItem.Text;

                    PacienteNegocio negocio = new PacienteNegocio();
                    //string emailPaciente = negocio.ObtenerEmailPorId(nuevo.Paciente.IdPaciente);
                    string emailPaciente = "invokerk868@gmail.com";

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
                    // Importante: Dejamos el catch vacío o registramos el error de manera silenciosa.
                    // Si el mail falla (por falta de internet temporal, etc.), NO queremos interrumpir el flujo.
                    // El turno ya se guardó en la base de datos, por lo que el programa debe continuar a la siguiente línea.
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

        private void CargarPacientes()
        {
            PacienteNegocio negocio = new PacienteNegocio();
            ddlPaciente.DataSource = negocio.Listar();
            ddlPaciente.DataValueField = "IdPaciente";
            ddlPaciente.DataTextField = "NombreCompleto";
            ddlPaciente.DataBind();
            ddlPaciente.Items.Insert(0, new ListItem("Seleccione un paciente...", "0"));
        }

        // METODOS DE LIMPIEZA INTERNA
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
            // Validamos que se haya seleccionado especialidad y médico
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
                // Agrupamos y extraemos los dias con sus horarios de entrada y salida
                var diasYHorarios = agendas.Select(x => $"{x.TurnoTrabajo.DiaDeTrabajo} ({x.TurnoTrabajo.HoraEntrada.ToString(@"hh\:mm")} a {x.TurnoTrabajo.HoraSalida.ToString(@"hh\:mm")} hs)");

                // Unimos todo en una sola cadena separada por comas
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
                // Buscamos el turno completo con sus relaciones en la base de datos
                Turno turnoViejo = new Turno();
                turnoViejo = turnoNegocio.BuscarPorId(idTurno);

                if (turnoViejo != null)
                {
                    // seleccionamos Especialidad y disparamos su evento manualmente para llenar los medicos
                    ddlEspecialidad.SelectedValue = turnoViejo.Agenda.Especialidad.IdEspecialidad.ToString();
                    ddlEspecialidad_SelectedIndexChanged(null, null);

                    // seleccionamos el Medico
                    ddlMedico.SelectedValue = turnoViejo.Agenda.Medico.IdMedico.ToString();

                    // seleccionamos el Paciente
                    ddlPaciente.SelectedValue = turnoViejo.Paciente.IdPaciente.ToString();

                    // Cargamos la observacion vieja si quieren mantenerla de referencia
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