<%@ Page Title="" Language="C#" MasterPageFile="~/Clinica.Master" AutoEventWireup="true" CodeBehind="UsuarioForm.aspx.cs" Inherits="App_Clinica.UsuarioForm" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

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
                    <asp:TextBox ID="txtContrasenia" runat="server" TextMode="Password" CssClass="form-control" ></asp:TextBox> <%-- TextMode Password oculta caracteres al escribir --%>
                </div>

                <div class="mb-4">
                    <label class="form-label fw-semibold text-secondary">Perfil:</label>
                    <asp:DropDownList ID="ddlPerfil" runat="server" CssClass="form-select" >
                        <asp:ListItem Text="Administrador" Value="1" />
                        <asp:ListItem Text="Recepcionista" Value="2" />
                        <asp:ListItem Text="Médico" Value="3" />
                    </asp:DropDownList>
                </div>

            <div class="d-grid gap-2">
                 <asp:Button ID="btnGuardar" runat="server" Text="Aceptar" CssClass="btn btn-primary py-2 fw-bold" OnClick="btnGuardar_Click"/>
                 <a href="Usuario.aspx" class="btn btn-outline-secondary py-2">Cancelar</a>
            </div>

            <div>
                <asp:Label ID="lblMensaje" runat="server"></asp:Label>
            </div>
         </div>
        </div>
</div>
</asp:Content>
