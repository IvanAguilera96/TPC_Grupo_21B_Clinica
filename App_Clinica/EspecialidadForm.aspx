<%@ Page Title="" Language="C#" MasterPageFile="~/Clinica.Master" AutoEventWireup="true" CodeBehind="EspecialidadForm.aspx.cs" Inherits="App_Clinica.EspecialidadForm" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <div class="row justify-content-center mt-4">
        <div class="col-12 col-md-6 col-lg-5">
            <div class="card bg-white p-4 rounded shadow-sm border">

                <h3 id="lblTitulo" runat="server" class="mb-3 text-center text-primary fw-bold">Nueva Especialidad</h3>
                <hr class="mt-0 mb-4" />
            
                <div class="mb-4">
                    <label class="form-label fw-semibold text-secondary">Nombre de la Especialidad:</label>
                    <asp:TextBox ID="txtDescripcion" runat="server" CssClass="form-control"></asp:TextBox>
                </div>

                <div class="d-grid gap-2">
                     <asp:Button ID="btnGuardar" runat="server" Text="Aceptar" CssClass="btn btn-primary py-2 fw-bold" OnClick="btnGuardar_Click"/>
                     <a href="EspecialidadPag.aspx" class="btn btn-outline-secondary py-2">Cancelar</a>
                </div>

                <div class="mt-3">
                    <asp:Label ID="lblMensaje" runat="server"></asp:Label>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
