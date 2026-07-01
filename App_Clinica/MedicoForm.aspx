<%@ Page Title="" Language="C#" MasterPageFile="~/Clinica.Master" AutoEventWireup="true" CodeBehind="MedicoForm.aspx.cs" Inherits="App_Clinica.MedicoForm" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <div class="row justify-content-center mt-4">
    <div class="col-12 col-md-6 col-lg-5">

        <div class="card bg-white p-4 rounded shadow-sm border">

            <h3 id="lblTitulo" runat="server" class="mb-3 text-center text-primary fw-bold">Alta de nuevo Médico
        </h3>
            <hr class="mt-0 mb-4" />

            <div class="mb-3">
                <label class="form-label fw-semibold text-secondary">Nombre</label>
                <asp:TextBox ID="txtNombre" runat="server" CssClass="form-control"></asp:TextBox>
            </div>

            <div class="mb-3">
                <label class="form-label fw-semibold text-secondary">Apellido</label>
                <asp:TextBox ID="txtApellido" runat="server" CssClass="form-control"></asp:TextBox>
            </div>

            <div class="mb-3">
                <label class="form-label fw-semibold text-secondary">DNI</label>
                <asp:TextBox ID="txtDni" runat="server" CssClass="form-control"></asp:TextBox>
            </div>

            <div class="mb-3">
                <label class="form-label fw-semibold text-secondary">Matricula</label>
                <asp:TextBox ID="txtMatricula" runat="server" CssClass="form-control"> </asp:TextBox>
            </div>

            <div class="mb-4">
                <div class="p-3 border rounded bg-light d-flex align-items-center justify-content-between">
                    <div>
                        <label class="form-label fw-semibold text-secondary mb-0 d-block">Estado del Medico</label>
                        <small class="text-muted small">Indica si el medico se encuentra activo en el sistema</small>
                    </div>

                    <div class="form-check form-switch fs-4 mb-0">
                        <input type="checkbox" id="chkEstado" runat="server" class="form-check-input" role="checkbox" checked="checked" />
                    </div>
                </div>
            </div>

            <div class="d-grid gap-2">
                <asp:Button ID="btnGuardar" runat="server" Text="Guardar Medico" CssClass="btn btn-primary py-2 fw-bold" OnClick="btnGuardar_Click" />
                <a href="MedicoPag.aspx" class="btn btn-outline-secondary py-2">Cancelar</a>
            </div>

            <div>
                <asp:Label ID="lblMensaje" runat="server"></asp:Label>
            </div>
        </div>

    </div>
</div>
</asp:Content>
