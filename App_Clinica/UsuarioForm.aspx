<%@ Page Title="" Language="C#" MasterPageFile="~/Clinica.Master" AutoEventWireup="true" CodeBehind="UsuarioForm.aspx.cs" Inherits="App_Clinica.UsuarioForm" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    
    <div class="crow justify-content-center mt-5">
        <div>
            <h3 id="lblTitulo" runat="server" class="mb-4 text-center">Alta de nuevo usuario</h3>
            <hr class="mb-4"/>
            
                <div class="mb-3">
                    <label>Nombre de Usuario:</label>
                    <asp:TextBox ID="txtNombre" runat="server" CssClass="form-control" ></asp:TextBox>
                </div>

                <div class="mb-3">
                    <label>Contraseña:</label>
                    <asp:TextBox ID="txtContrasenia" runat="server" TextMode="Password" CssClass="form-control" ></asp:TextBox> <%-- TextMode Password oculta caracteres al escribir --%>
                </div>

                <div class="mb-4">
                    <label>Perfil:</label>
                    <asp:DropDownList ID="ddlPerfil" runat="server" CssClass="form-select" >
                        <asp:ListItem Text="Administrador" Value="1" />
                        <asp:ListItem Text="Recepcionista" Value="2" />
                        <asp:ListItem Text="Médico" Value="3" />
                    </asp:DropDownList>
                </div>

            <div class="row g-2">
                <div class="col-6">
                    <asp:Button ID="btnGuardar" runat="server" Text="Aceptar" CssClass="btn btn-primary w-100 py-2" OnClick="btnGuardar_Click"/>
                </div>
                <div class="col-6">
                    <a href="Usuario.aspx" class="btn btn-outline-secondary w-100 py-2">Cancelar</a>
                </div>
            </div>

            <div>
                <asp:Label ID="lblMensaje" runat="server"></asp:Label>
            </div>

        </div>
</div>
</asp:Content>
