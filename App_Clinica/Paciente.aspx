<%@ Page Title="" Language="C#" MasterPageFile="~/Clinica.Master" AutoEventWireup="true" CodeBehind="Paciente.aspx.cs" Inherits="App_Clinica.Paciente" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    
    <div class="col-md-6">
        <h3>Pacientes registrados</h3>
        <asp:GridView ID="dgvPaciente" runat="server" CssClass="table table-striped table-bordered table-hover"></asp:GridView>
    </div>
    
</asp:Content>
