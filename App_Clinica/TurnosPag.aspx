<%@ Page Title="" Language="C#" MasterPageFile="~/Clinica.Master" AutoEventWireup="true" CodeBehind="TurnosPag.aspx.cs" Inherits="App_Clinica.TurnoPag" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <div class="container mt-4">
        <h2>Gestión de Turnos</h2>
        <hr />

        <!-- Sección de Filtros -->
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

        <!-- Grilla de Turnos -->
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
                        <span class="badge <%# Eval("Estado.Descripcion").ToString() == "Asignado" ? "bg-warning text-dark" : "bg-danger bg-opacity-10 text-danger" %> px-2.5 py-1.5 rounded-2 fw-semibold">
                            <%# Eval("Estado.Descripcion") %>
                        </span>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Acciones" ItemStyle-Width="200px" ItemStyle-CssClass="text-end">
                    <ItemTemplate>
                        <div class="d-inline-flex justify-content-end w-100">
                            
                            <asp:LinkButton ID="btnReprogramar" runat="server"
                                CommandName="ReprogramarTurno"
                                CommandArgument='<%# Eval("IdTurno") %>'
                                CssClass="btn btn-sm btn-link text-primary text-decoration-none fw-bold p-0 me-3"
                                Visible='<%# Eval("Estado.Descripcion").ToString() != "Cancelado" && Eval("Estado.Descripcion").ToString() != "Cerrado" %>'>
                                <i class="bi bi-arrow-repeat me-1"></i>Reprogramar
                            </asp:LinkButton>

                            <asp:LinkButton ID="btnCancelar" runat="server"
                                CommandName="CancelarTurno"
                                CommandArgument='<%# Eval("IdTurno") %>'
                                CssClass="btn btn-sm btn-link text-danger text-decoration-none fw-bold p-0"
                                Visible='<%# Eval("Estado.Descripcion").ToString() != "Cancelado" && Eval("Estado.Descripcion").ToString() != "Cerrado" %>'
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
</asp:Content>
