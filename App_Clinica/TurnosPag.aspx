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
        <asp:GridView ID="dgvTurnos" runat="server" CssClass="table table-striped table-hover table-bordered" 
            AutoGenerateColumns="false" DataKeyNames="IdTurno" OnRowCommand="dgvTurnos_RowCommand">
            <Columns>
                <asp:BoundField HeaderText="ID" DataField="IdTurno" ItemStyle-Width="50px" />
                <asp:BoundField HeaderText="Fecha" DataField="Fecha" DataFormatString="{0:dd/MM/yyyy}" />
                <asp:BoundField HeaderText="Hora" DataField="Hora" DataFormatString="{0:hh\:mm}" />
                
                <asp:BoundField HeaderText="Paciente" DataField="Paciente.NombreCompleto" /> 
                <asp:BoundField HeaderText="Médico" DataField="Agenda.Medico.NombreCompleto" />
                <asp:BoundField HeaderText="Especialidad" DataField="Agenda.Especialidad.Descripcion" />
                
                <%-- Estado con Badge de color dinámico --%>
                <asp:TemplateField HeaderText="Estado">
                    <ItemTemplate>
                        <span class="badge <%# Eval("Estado.Descripcion").ToString() == "Asignado" ? "bg-warning text-dark" : "bg-danger" %>">
                            <%# Eval("Estado.Descripcion") %>
                        </span>
                    </ItemTemplate>
                </asp:TemplateField>

                <%-- Acciones --%>
                <asp:TemplateField HeaderText="Acciones">
                    <ItemTemplate>
                        <asp:LinkButton ID="btnCancelar" runat="server" 
                            CommandName="CancelarTurno" 
                            CommandArgument='<%# Eval("IdTurno") %>' CssClass="btn btn-sm btn-outline-danger"
                            Visible='<%# Eval("Estado.Descripcion").ToString() == "Asignado" %>'>
                            Cancelar
                        </asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </div>
</asp:Content>
