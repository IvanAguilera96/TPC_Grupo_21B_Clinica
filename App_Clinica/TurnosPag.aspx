<%@ Page Title="" Language="C#" MasterPageFile="~/Clinica.Master" AutoEventWireup="true" CodeBehind="TurnosPag.aspx.cs" Inherits="App_Clinica.TurnoPag" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server"></asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <asp:HiddenField ID="hfIdTurnoACerrar" runat="server" />

    <div class="container mt-4">
        <h2>Gestión de Turnos</h2>
        <hr />

        <div class="row mb-4 align-items-end">
            <div class="col-md-3">
                <label class="form-label">Filtrar por Médico:</label>
                <asp:DropDownList ID="ddlFiltroMedico" runat="server" CssClass="form-select" AutoPostBack="true" OnSelectedIndexChanged="ddlFiltroMedico_SelectedIndexChanged"></asp:DropDownList>
            </div>
            <div class="col-md-3">
                <label class="form-label">Filtrar por Especialidad:</label>
                <asp:DropDownList ID="ddlFiltroEspecialidad" runat="server" CssClass="form-select" AutoPostBack="true" OnSelectedIndexChanged="ddlFiltroEspecialidad_SelectedIndexChanged"></asp:DropDownList>
            </div>
            <div class="col-md-3">
                <label class="form-label">Filtrar por Fecha:</label>
                <asp:TextBox ID="txtFiltroFecha" runat="server" TextMode="Date" CssClass="form-control" AutoPostBack="true" onkeydown="return false;" OnTextChanged="txtFiltroFecha_TextChanged"></asp:TextBox>
            </div>
            <div class="col-md-3 text-end">
                <asp:Button ID="btnNuevoTurno" runat="server" Text="Nuevo Turno ➕" CssClass="btn btn-primary w-100" OnClick="btnNuevoTurno_Click" />
            </div>
        </div>

        <div class="card bg-white p-4 rounded-3 shadow-sm border-0 mb-4 mt-2">
            <div class="table-responsive" style="max-height: 450px; overflow-y: auto;">
                <asp:GridView ID="dgvTurnos" runat="server" CssClass="table table-hover align-middle border-0 small"
                    AutoGenerateColumns="false" GridLines="None" DataKeyNames="IdTurno" OnRowCommand="dgvTurnos_RowCommand">
                    <Columns>
                        <asp:BoundField HeaderText="ID" DataField="IdTurno" ItemStyle-CssClass="text-muted fw-semibold" ItemStyle-Width="60px" />
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

                        <asp:TemplateField HeaderText="Acciones" ItemStyle-Width="320px" ItemStyle-CssClass="text-end">
                            <ItemTemplate>
                                <div class="d-inline-flex justify-content-end w-100 gap-2">
                                    
                                    <button type="button" class="btn btn-sm btn-link text-success text-decoration-none fw-bold p-0 me-2"
                                        style='<%# Eval("Estado.Descripcion").ToString() == "Cancelado" || Eval("Estado.Descripcion").ToString() == "Cerrado" || Eval("Estado.Descripcion").ToString() == "No Asistió" || Eval("Estado.Descripcion").ToString() == "Reprogramado" ? "display:none;" : "" %>'
                                        onclick="abrirModalDiagnostico('<%# Eval("IdTurno") %>')">
                                        <i class="bi bi-check-circle me-1"></i>Cerrar
                                    </button>

                                    <asp:LinkButton ID="btnNoAsistio" runat="server"
                                        CommandName="AusenteTurno" CommandArgument='<%# Eval("IdTurno") %>'
                                        CssClass="btn btn-sm btn-link text-dark text-decoration-none fw-bold p-0 me-2"
                                        Visible='<%# Eval("Estado.Descripcion").ToString() != "Cancelado" && Eval("Estado.Descripcion").ToString() != "Cerrado" && Eval("Estado.Descripcion").ToString() != "No Asistió" && Eval("Estado.Descripcion").ToString() != "Reprogramado" %>'>
                                        <i class="bi bi-person-x me-1"></i>No Asistió
                                    </asp:LinkButton>

                                    <asp:LinkButton ID="btnReprogramar" runat="server"
                                        CommandName="ReprogramarTurno" CommandArgument='<%# Eval("IdTurno") %>'
                                        CssClass="btn btn-sm btn-link text-primary text-decoration-none fw-bold p-0 me-2"
                                        Visible='<%# Eval("Estado.Descripcion").ToString() != "Cancelado" && Eval("Estado.Descripcion").ToString() != "Cerrado" && Eval("Estado.Descripcion").ToString() != "No Asistió" && Eval("Estado.Descripcion").ToString() != "Reprogramado" %>'>
                                        <i class="bi bi-arrow-repeat me-1"></i>Reprogramar
                                    </asp:LinkButton>

                                    <asp:LinkButton ID="btnCancelar" runat="server"
                                        CommandName="CancelarTurno" CommandArgument='<%# Eval("IdTurno") %>'
                                        CssClass="btn btn-sm btn-link text-danger text-decoration-none fw-bold p-0"
                                        Visible='<%# Eval("Estado.Descripcion").ToString() != "Cancelado" && Eval("Estado.Descripcion").ToString() != "Cerrado" && Eval("Estado.Descripcion").ToString() != "No Asistió" && Eval("Estado.Descripcion").ToString() != "Reprogramado" %>'
                                        OnClientClick="return confirmarEliminar(this);">
                                        <i class="bi bi-x-circle me-1"></i>Cancelar
                                    </asp:LinkButton>
                                    
                                </div>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
        </div>
    </div>

    <div class="modal fade" id="modalDiagnostico" tabindex="-1" aria-hidden="true" data-bs-backdrop="static">
        <div class="modal-dialog modal-dialog-centered">
            <div class="modal-content border-0 shadow">
                <div class="modal-header bg-info bg-opacity-25 text-dark border-0">
                    <h5 class="modal-title fw-bold">Finalizar Consulta y Cerrar Turno</h5>
                    <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                </div>
                <div class="modal-body text-start">
                    <div class="mb-3">
                        <label class="form-label fw-bold text-secondary">Escriba el Diagnóstico / Observaciones Clínicas:</label>
                        <asp:TextBox ID="txtDiagnostico" runat="server" TextMode="MultiLine" Rows="4" CssClass="form-control" placeholder="Ingrese los detalles médicos aquí..."></asp:TextBox>
                    </div>
                </div>
                <div class="modal-footer border-0">
                    <button type="button" class="btn btn-light fw-semibold text-secondary" data-bs-backdrop="false" data-bs-dismiss="modal">Cancelar</button>
                    <asp:Button ID="btnGuardarCierre" runat="server" Text="Guardar y Cerrar Turno" CssClass="btn btn-info text-dark fw-bold" OnClick="btnGuardarCierre_Click" />
                </div>
            </div>
        </div>
    </div>

    <script type="text/javascript">
        function abrirModalDiagnostico(idTurno) {
            document.getElementById('<%= hfIdTurnoACerrar.ClientID %>').value = idTurno;
            document.getElementById('<%= txtDiagnostico.ClientID %>').value = '';
            var modal = new bootstrap.Modal(document.getElementById('modalDiagnostico'));
            modal.show();
        }
    </script>
</asp:Content>