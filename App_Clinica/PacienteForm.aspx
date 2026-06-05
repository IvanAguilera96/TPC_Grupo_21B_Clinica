<%@ Page Title="" Language="C#" MasterPageFile="~/Clinica.Master" AutoEventWireup="true" CodeBehind="PacienteForm.aspx.cs" Inherits="App_Clinica.PacienteForm" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <div class="row justify-content-center mt-4">
        <div class="col-12 col-md-6 col-lg-5">

            <div class="card bg-white p-4 rounded shadow-sm border">

                <h3 id="lblTitulo" runat="server" class="mb-3 text-center text-primary fw-bold">Alta de nuevo paciente
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
                    <label class="form-label fw-semibold text-secondary">Email</label>
                    <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" TextMode="Email"></asp:TextBox>
                </div>

                <div class="mb-4">
                    <label class="form-label fw-semibold text-secondary">Teléfono</label>
                    <asp:TextBox ID="txtTelefono" runat="server" CssClass="form-control" TextMode="Phone"></asp:TextBox>
                </div>

                <div class="d-grid gap-2">
                    <asp:Button ID="btnGuardar" runat="server" Text="Guardar Paciente" CssClass="btn btn-primary py-2 fw-bold" />
                    <a href="Default.aspx" class="btn btn-outline-secondary py-2">Cancelar</a>
                </div>

            </div>

        </div>
</div>
</asp:Content>
