<%@ Page Title="" Language="C#" MasterPageFile="~/Clinica.Master" AutoEventWireup="true" CodeBehind="Paciente.aspx.cs" Inherits="App_Clinica.Paciente" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <div class="row justify-content-center">
        <div lass="col-md-6 d-flex flex-column align-items-center">
            
            <asp:Label ID="lblMensajeGrilla" runat="server" CssClass="alert alert-success d-block text-center mb-3" Visible="false"></asp:Label>
            
            <h3 class="mb-3">Pacientes registrados</h3>
            <asp:GridView ID="dgvPaciente" DataKeyNames="IdPaciente" OnSelectedIndexChanged="dgvPaciente_SelectedIndexChanged" runat="server" CssClass="table table-striped table-bordered table-hover" AutoGenerateColumns="false">
                <Columns>
                    <asp:BoundField HeaderText ="DNI" DataField="DNI"/>
                    <asp:BoundField HeaderText ="Nombre" DataField="Nombre"/> 
                    <asp:BoundField HeaderText ="Apellido" DataField="Apellido"/> 
                    <asp:BoundField HeaderText ="Email" DataField="Email"/> 
                    <asp:BoundField HeaderText ="Telefono" DataField="Telefono"/> 
                    <asp:CheckBoxField HeaderText ="Estado" DataField="Estado" />
                    <asp:CommandField ShowSelectButton="true" SelectText="Seleccionar" HeaderText="Accion"/>
                </Columns>
            </asp:GridView>
        </div>
    </div>
   
    
</asp:Content>
