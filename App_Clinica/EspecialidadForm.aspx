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

                <div class="mb-4">
                    <div class="p-3 border rounded bg-light d-flex align-items-center justify-content-between">
                        <div>
                            <label class="form-label fw-semibold text-secondary mb-0 d-block">Estado de la Especialidad</label>
                            <small class="text-muted small">Indica si la especialidad se encuentra activa en el sistema</small>
                        </div>

                        <div class="form-check form-switch fs-4 mb-0">
                            <input type="checkbox" id="chkEstado" runat="server" class="form-check-input" role="checkbox" checked="checked" />
                        </div>
                    </div>
                </div>

                <div class="d-grid gap-2">
                     <asp:Button ID="btnGuardar" runat="server" Text="Guardar Especialidad" CssClass="btn btn-primary py-2 fw-bold" OnClick="btnGuardar_Click"/>
                     <a href="EspecialidadPag.aspx" class="btn btn-outline-secondary py-2">Cancelar</a>
                </div>

                <div class="mt-3">
                    <asp:Label ID="lblMensaje" runat="server"></asp:Label>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
