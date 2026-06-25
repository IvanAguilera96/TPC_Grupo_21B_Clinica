<%@ Page Title="" Language="C#" MasterPageFile="~/Clinica.Master" AutoEventWireup="true" CodeBehind="MedicoAgendaForm.aspx.cs" Inherits="App_Clinica.MedicoAgendaForm" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <div class="row justify-content-center mt-4">
        <div class="col-12 col-md-6 col-lg-5">
            <div class="card bg-white p-4 rounded shadow-sm border">

                <h3 class="mb-3 text-center text-primary fw-bold">Asignar Agenda</h3>
                <hr class="mt-0 mb-4" />

                <div class="mb-3">
                    <label class="form-label fw-semibold text-secondary">Seleccione la Especialidad:</label>
                    <asp:DropDownList ID="ddlEspecialidad" runat="server" CssClass="form-select"></asp:DropDownList>
                </div>

                <div class="mb-3">
                    <label class="form-label">Seleccione el Día de Trabajo:</label>
                    <asp:DropDownList ID="ddlDia" runat="server" CssClass="form-select">
                        <asp:ListItem Text="Lunes" Value="Lunes" />
                        <asp:ListItem Text="Martes" Value="Martes" />
                        <asp:ListItem Text="Miércoles" Value="Miércoles" />
                        <asp:ListItem Text="Jueves" Value="Jueves" />
                        <asp:ListItem Text="Viernes" Value="Viernes" />
                        <asp:ListItem Text="Sábado" Value="Sábado" />
                    </asp:DropDownList>
                </div>

                <div class="row">
                    <div class="col-md-6 mb-3">
                        <label class="form-label">Hora Entrada:</label>
                        <asp:TextBox ID="txtHoraEntrada" runat="server" TextMode="Time" CssClass="form-control"></asp:TextBox>
                    </div>
                    <div class="col-md-6 mb-3">
                        <label class="form-label">Hora Salida:</label>
                        <asp:TextBox ID="txtHoraSalida" runat="server" TextMode="Time" CssClass="form-control"></asp:TextBox>
                    </div>
                </div>

                <div class="d-grid gap-2">
                    <asp:Button ID="btnGuardar" runat="server" Text="Confirmar Asignación" CssClass="btn btn-primary py-2 fw-bold" OnClick="btnGuardar_Click" />
                    <asp:LinkButton ID="btnCancelar" runat="server" CssClass="btn btn-outline-secondary py-2" OnClick="btnCancelar_Click">Cancelar</asp:LinkButton>
                </div>

                <div class="mt-3">
                    <asp:Label ID="lblMensaje" runat="server"></asp:Label>
                </div>
            </div>
        </div>
    </div>

</asp:Content>
