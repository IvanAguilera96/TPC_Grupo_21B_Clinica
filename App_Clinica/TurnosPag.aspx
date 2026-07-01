<%@ Page Title="" Language="C#" MasterPageFile="~/Clinica.Master" AutoEventWireup="true" CodeBehind="TurnosPag.aspx.cs" Inherits="App_Clinica.TurnoPag" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server"></asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <asp:HiddenField ID="hfIdTurnoACerrar" runat="server" />
       <!-- El ScriptManager es obligatorio para que funcione el UpdatePanel -->
   <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

    <div class="row justify-content-center">
        <div class="col-12 col-md-11 col-lg-10">

            <div class="d-flex justify-content-between align-items-center mb-3">
                <h4 class="text-dark fw-bold m-0">
                    <i class="bi bi-calendar-check-fill text-primary me-1"></i>Gestión de Turnos
                </h4>
                <asp:Button ID="btnNuevoTurno" runat="server" Text="Nuevo Turno ➕"
                    CssClass="btn border-0 fw-bold px-4 py-2 shadow-sm rounded-3 btn-sm"
                    Style="background-color: #D0E4F7; color: #1C3345;"
                    OnClick="btnNuevoTurno_Click" />
            </div>


            <asp:UpdatePanel ID="UpdatePanelCombos" runat="server">
                <ContentTemplate>
                    <div class="row g-3 mb-3 align-items-end">
                        <!-- Filtrar por Médico -->
                        <div class="col-12 col-md-4">
                            <label class="form-label fw-semibold text-secondary mb-1" style="font-size: 0.85rem;">Filtrar por Médico:</label>
                            <asp:DropDownList ID="ddlFiltroMedico" runat="server" CssClass="form-select"
                                AutoPostBack="true" OnSelectedIndexChanged="ddlFiltroMedico_SelectedIndexChanged">
                            </asp:DropDownList>
                        </div>

                        <!-- Filtrar por Especialidad -->
                        <div class="col-12 col-md-4">
                            <label class="form-label fw-semibold text-secondary mb-1" style="font-size: 0.85rem;">Filtrar por Especialidad:</label>
                            <asp:DropDownList ID="ddlFiltroEspecialidad" runat="server" CssClass="form-select"
                                AutoPostBack="true" OnSelectedIndexChanged="ddlFiltroEspecialidad_SelectedIndexChanged">
                            </asp:DropDownList>
                        </div>

                        <!-- Filtrar por Fecha -->
                        <div class="col-12 col-md-4">
                            <label class="form-label fw-semibold text-secondary mb-1" style="font-size: 0.85rem;">Filtrar por Fecha:</label>
                            <asp:TextBox ID="txtFiltroFecha" runat="server" TextMode="Date" CssClass="form-control"
                                AutoPostBack="true" onkeydown="return false;" OnTextChanged="txtFiltroFecha_TextChanged">
                            </asp:TextBox>
                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
            <div class="mt-2 mb-2">
                <asp:Label ID="lblMensajeExito" runat="server" CssClass="alert alert-success d-block fw-semibold small py-2 px-3 shadow-sm rounded-3" Visible="false">
                        <i class="bi bi-check-circle-fill me-2"></i>
                </asp:Label>
            </div>


            <div class="card bg-white p-3 rounded-3 shadow-sm border-0 mb-4">
                <div class="table-responsive" style="max-height: 500px; overflow-y: auto;">
                    <asp:GridView ID="dgvTurnos" runat="server" CssClass="table table-hover align-middle border-0 small mb-0"
                        AutoGenerateColumns="false" GridLines="None" DataKeyNames="IdTurno" OnRowCommand="dgvTurnos_RowCommand">
                        <Columns>

                            <asp:BoundField HeaderText="Fecha" DataField="Fecha" DataFormatString="{0:dd/MM/yyyy}" ItemStyle-Width="110px" />
                            <asp:BoundField HeaderText="Hora" DataField="Hora" DataFormatString="{0:hh\:mm}" ItemStyle-CssClass="fw-bold text-dark" ItemStyle-Width="90px" />

                            <asp:BoundField HeaderText="Paciente" DataField="Paciente.NombreCompleto" />
                            <asp:BoundField HeaderText="Médico" DataField="Agenda.Medico.NombreCompleto" />
                            <asp:BoundField HeaderText="Especialidad" DataField="Agenda.Especialidad.Descripcion" />

                            <asp:TemplateField HeaderText="Estado" ItemStyle-Width="120px">
                                <ItemTemplate>
                                    <span class="badge <%# Eval("Estado.Descripcion").ToString() == "Asignado" ? "bg-warning text-dark" : 
                                                       Eval("Estado.Descripcion").ToString() == "Cerrado" ? "bg-success bg-opacity-10 text-success" :
                                                       Eval("Estado.Descripcion").ToString() == "Reprogramado" ? "bg-info bg-opacity-10 text-info" :
                                                       Eval("Estado.Descripcion").ToString() == "No Asistió" ? "bg-secondary bg-opacity-20 text-dark" : "bg-danger bg-opacity-10 text-danger" %> px-2.5 py-1.5 rounded-2 fw-semibold">
                                        <%# Eval("Estado.Descripcion") %>
                                    </span>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Acciones" ItemStyle-Width="140px" ItemStyle-CssClass="text-end">
                                <ItemTemplate>
                                    <div class="d-inline-flex justify-content-end w-100 gap-1">

                                        <asp:LinkButton ID="btnVerDetalle" runat="server"
                                            CommandName="VerDetalle" CommandArgument='<%# Eval("IdTurno") %>'
                                            CssClass="btn btn-sm p-1 text-primary fs-5" title="Ver Detalle del Turno">
                                                <i class="bi bi-eye-fill"></i>
                                        </asp:LinkButton>

                                        <button type="button" class="btn btn-sm p-1 text-success fs-5" title="Cerrar Turno"
                                            style='<%# Eval("Estado.Descripcion").ToString() == "Cancelado" || Eval("Estado.Descripcion").ToString() == "Cerrado" || Eval("Estado.Descripcion").ToString() == "No Asistió" || Eval("Estado.Descripcion").ToString() == "Reprogramado" ? "display:none;": "" %>'
                                            onclick="abrirModalDiagnostico('<%# Eval("IdTurno") %>')">
                                            <i class="bi bi-check-circle-fill"></i>
                                        </button>

                                        <asp:LinkButton ID="btnAusente" runat="server"
                                            CommandName="AusenteTurno" CommandArgument='<%# Eval("IdTurno") %>'
                                            CssClass="btn btn-sm p-1 text-dark fs-5" title="Marcar como No Asistió"
                                            OnClientClick="return confirmarAccionTurno(this, 'Ausente');"
                                            Visible='<%# Eval("Estado.Descripcion").ToString() != "Cancelado" && Eval("Estado.Descripcion").ToString() != "Cerrado" && Eval("Estado.Descripcion").ToString() != "No Asistió" && Eval("Estado.Descripcion").ToString() != "Reprogramado" %>'>
                                                <i class="bi bi-person-x-fill"></i>
                                        </asp:LinkButton>

                                        <asp:LinkButton ID="btnReprogramar" runat="server"
                                            CommandName="ReprogramarTurno" CommandArgument='<%# Eval("IdTurno") %>'
                                            CssClass="btn btn-sm p-1 text-info fs-5" title="Reprogramar Turno"
                                            Visible='<%# Eval("Estado.Descripcion").ToString() != "Cancelado" && Eval("Estado.Descripcion").ToString() != "Cerrado" && Eval("Estado.Descripcion").ToString() != "No Asistió" && Eval("Estado.Descripcion").ToString() != "Reprogramado" %>'>
                                                <i class="bi bi-arrow-repeat"></i>
                                        </asp:LinkButton>

                                        <asp:LinkButton ID="btnCancelar" runat="server"
                                            CommandName="CancelarTurno" CommandArgument='<%# Eval("IdTurno") %>'
                                            CssClass="btn btn-sm p-1 text-danger fs-5" title="Cancelar Turno"
                                            OnClientClick="return confirmarAccionTurno(this, 'Cancelar');"
                                            Visible='<%# Eval("Estado.Descripcion").ToString() != "Cancelado" && Eval("Estado.Descripcion").ToString() != "Cerrado" && Eval("Estado.Descripcion").ToString() != "No Asistió" && Eval("Estado.Descripcion").ToString() != "Reprogramado" %>'>
                                                <i class="bi bi-x-circle-fill"></i>
                                        </asp:LinkButton>

                                    </div>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
            </div>

        </div>
    </div>

    <!-- MODAL DIAGNÓSTICO -->
    <div class="modal fade" id="modalDiagnostico" tabindex="-1" aria-hidden="true">
        <div class="modal-dialog modal-dialog-centered">
            <div class="modal-content border-0 shadow" style="background-color: #f0f8ff;">
                <div class="modal-header border-0" style="background-color: #d0e7ff; color: #2c3e50;">
                    <h5 class="modal-title fw-bold"><i class="bi bi-heart-pulse-fill me-2"></i>Cierre de Turno y Diagnóstico</h5>
                    <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                </div>
                <div class="modal-body p-4">
                    <div class="form-group">
                        <label class="form-label fw-semibold text-secondary mb-2">Diagnóstico / Observaciones Médicas</label>
                        <asp:TextBox ID="txtDiagnostico" runat="server" TextMode="MultiLine" Rows="4"
                            CssClass="form-control border-0 shadow-sm bg-white"
                            Style="resize: none;" placeholder="Escriba el diagnóstico aquí..."></asp:TextBox>
                    </div>
                </div>
                <div class="modal-footer border-0 justify-content-end p-3" style="background-color: #e6f2ff;">
                    <button type="button" class="btn btn-light border px-3" data-bs-dismiss="modal">Cerrar</button>
                    <asp:Button ID="btnGuardarCierre" runat="server" Text="Guardar y Finalizar"
                        CssClass="btn btn-primary px-4 text-white fw-semibold shadow-sm"
                        OnClick="btnGuardarCierre_Click" />
                </div>
            </div>
        </div>
    </div>

    <!-- MODAL DETALLE -->
    <div class="modal fade" id="modalDetalleTurno" tabindex="-1" aria-hidden="true">
        <div class="modal-dialog modal-dialog-centered">
            <div class="modal-content border-0 shadow" style="background-color: #f0f8ff;">
                <div class="modal-header border-0" style="background-color: #d0e7ff; color: #2c3e50;">
                    <h5 class="modal-title fw-bold"><i class="bi bi-info-circle-fill me-2" style="color: #4a90e2;"></i>Detalle del Turno</h5>
                    <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                </div>
                <div class="modal-body p-4 text-dark">
                    <div class="row g-3 small">
                        <div class="col-6">
                            <span class="text-muted d-block fw-medium">Paciente:</span>
                            <asp:Label ID="lblDetallePaciente" runat="server" CssClass="fw-bold text-dark fs-6"></asp:Label>
                        </div>
                        <div class="col-6 text-end">
                            <span class="text-muted d-block fw-medium text-end">Estado:</span>
                            <asp:Label ID="lblDetalleEstado" runat="server" CssClass="fw-bold"></asp:Label>
                        </div>
                        <div class="col-6">
                            <span class="text-muted d-block fw-medium">Médico:</span>
                            <asp:Label ID="lblDetalleMedico" runat="server" CssClass="fw-semibold text-dark"></asp:Label>
                        </div>
                        <div class="col-6">
                            <span class="text-muted d-block fw-medium">Fecha y Hora:</span>
                            <asp:Label ID="lblDetalleFechaHora" runat="server" CssClass="fw-semibold text-dark"></asp:Label>
                        </div>
                        <div class="col-12">
                            <span class="text-muted d-block fw-medium">Especialidad:</span>
                            <asp:Label ID="lblDetalleEspecialidad" runat="server" CssClass="fw-semibold text-dark"></asp:Label>
                        </div>

                        <hr class="text-muted opacity-25 my-3" />

                        <div class="col-12">
                            <label class="form-label fw-bold text-secondary mb-1"><i class="bi bi-chat-left-text me-1"></i>Motivo de la Consulta (Observación):</label>
                            <div class="p-3 bg-white rounded-3 border-0 shadow-sm text-dark text-start" style="min-height: 50px; white-space: pre-wrap;">
                                <asp:Label ID="lblDetalleObservacion" runat="server"></asp:Label>
                            </div>
                        </div>

                        <div class="col-12 mt-3">
                            <label class="form-label fw-bold text-secondary mb-1"><i class="bi bi-heart-pulse me-1"></i>Diagnóstico / Evolución Médica:</label>
                            <div class="p-3 bg-white rounded-3 border-0 shadow-sm text-dark text-start" style="min-height: 70px; white-space: pre-wrap;">
                                <asp:Label ID="lblDetalleDiagnostico" runat="server"></asp:Label>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="modal-footer border-0 p-3" style="background-color: #e6f2ff;">
                    <button type="button" class="btn btn-light border px-4 fw-semibold rounded-3" data-bs-dismiss="modal">Cerrar</button>
                </div>
            </div>
        </div>
    </div>

    <!-- SCRIPTS -->
    <script type="text/javascript">
        function abrirModalDiagnostico(idTurno) {
            document.getElementById('<%= hfIdTurnoACerrar.ClientID %>').value = idTurno;
            document.getElementById('<%= txtDiagnostico.ClientID %>').value = '';
            var modal = new bootstrap.Modal(document.getElementById('modalDiagnostico'));
            modal.show();
        }

        function confirmarAccionTurno(boton, tipo) {
            var titulo = document.getElementById("modalConfirmarLabel");
            var cuerpo = document.querySelector("#modalConfirmarEliminar .modal-body p");
            var btnConfirmar = document.getElementById("btnMdlConfirmarBorrado");

            if (tipo === 'Ausente') {
                titulo.innerHTML = '<i class="bi bi-x-circle-fill me-2" style="color: #C85A53;"></i>Registrar Inasistencia';
                cuerpo.innerText = "¿Está seguro de que desea registrar la inasistencia del paciente?";
                btnConfirmar.innerText = "Confirmar";
                btnConfirmar.className = "btn btn-danger flex-grow-1 fw-bold rounded-3";
            } else if (tipo === 'Cancelar') {
                titulo.innerHTML = '<i class="bi bi-x-circle-fill me-2" style="color: #C85A53;"></i>Cancelar Turno';
                cuerpo.innerText = "¿Está seguro de que desea cancelar este turno?";
                btnConfirmar.innerText = "Cancelar Turno";
                btnConfirmar.className = "btn btn-danger flex-grow-1 fw-bold rounded-3";
            }

            return confirmarEliminar(boton);
        }

        function abrirModalDetalle() {
            var modal = new bootstrap.Modal(document.getElementById('modalDetalleTurno'));
            modal.show();
        }
    </script>
</asp:Content>
