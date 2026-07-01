<%@ Page Title="" Language="C#" MasterPageFile="~/Clinica.Master" AutoEventWireup="true" CodeBehind="UsuarioForm.aspx.cs" Inherits="App_Clinica.UsuarioForm" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server"></asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    
    <div class="row justify-content-center mt-4">
        <div class="col-12 col-md-6 col-lg-5">

            <div class="card bg-white p-4 rounded shadow-sm border">

                <h3 id="lblTitulo" runat="server" class="mb-3 text-center text-primary fw-bold">Alta de nuevo usuario</h3>
                <hr class="mt-0 mb-4" />
            
                <div class="mb-3">
                    <label class="form-label fw-semibold text-secondary">Nombre de Usuario:</label>
                    <asp:TextBox ID="txtNombre" runat="server" CssClass="form-control" ></asp:TextBox>
                </div>

                <div class="mb-3">
                    <label class="form-label fw-semibold text-secondary">Contraseña:</label>
                    <asp:TextBox ID="txtContrasenia" runat="server" CssClass="form-control" ></asp:TextBox>
                </div>

                <div class="mb-3">
                    <label class="form-label fw-semibold text-secondary">Perfil:</label>
                    <asp:DropDownList ID="ddlPerfil" runat="server" CssClass="form-select" clientidmode="Static" onchange="evaluarPerfilMedico()"></asp:DropDownList>
                </div>

                <div class="mb-4" id="divMedicoVinculado" style="display: none;">
                    <label class="form-label fw-bold text-primary"><i class="bi bi-person-vcard me-1"></i>Vincular Profesional Médico:</label>
                    <asp:DropDownList ID="ddlMedico" runat="server" CssClass="form-select border-primary shadow-sm"></asp:DropDownList>
                    <small class="text-muted d-block mt-1">Seleccione el legajo del médico que operará esta cuenta.</small>
                </div>

                <div class="mb-4">
                    <div class="p-3 border rounded bg-light d-flex align-items-center justify-content-between">
                        <div>
                            <label class="form-label fw-semibold text-secondary mb-0 d-block">Estado del Usuario</label>
                            <small class="text-muted small">Indica si el usuario se encuentra activo en el sistema</small>
                        </div>
                        <div class="form-check form-switch fs-4 mb-0">
                            <input type="checkbox" id="chkEstado" runat="server" class="form-check-input" role="checkbox" checked="checked" />
                        </div>
                    </div>
                </div>

                <div class="d-grid gap-2">
                     <asp:Button ID="btnGuardar" runat="server" Text="Guardar Usuario" CssClass="btn btn-primary py-2 fw-bold" OnClick="btnGuardar_Click"/>
                     <a href="Usuario.aspx" class="btn btn-outline-secondary py-2">Cancelar</a>
                </div>

                <div class="mt-2">
                    <asp:Label ID="lblMensaje" runat="server"></asp:Label>
                </div>
            </div>
        </div>
    </div>

    <script type="text/javascript">
        function evaluarPerfilMedico() {
            var ddlPerfil = document.getElementById("ddlPerfil");
            var divMedico = document.getElementById("divMedicoVinculado");
            
            // Valor '3' corresponde al IdPerfil de Médico en la base de datos
            if (ddlPerfil.options[ddlPerfil.selectedIndex].value === "3") {
                divMedico.style.display = "block";
            } else {
                divMedico.style.display = "none";
            }
        }

        // Ejecutar al cargar la página para mantener consistencia al modificar registros existente
        window.onload = function () {
            evaluarPerfilMedico();
        };
    </script>
</asp:Content>