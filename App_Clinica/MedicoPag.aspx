<%@ Page Title="" Language="C#" MasterPageFile="~/Clinica.Master" AutoEventWireup="true" CodeBehind="MedicoPag.aspx.cs" Inherits="App_Clinica.MedicoPag" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <div class="row justify-content-center">
        <div class="col-md-6 d-flex flex-column align-items-center">

            <asp:Label ID="lblMensajeGrilla" runat="server" CssClass="alert alert-success d-block text-center mb-3" Visible="false"></asp:Label>

            <h3 class="mb-3">Medicos registrados</h3>
            <asp:GridView ID="dgvMedico" DataKeyNames="IdMedico" CssClass="table table-striped table-bordered table-hover"
                OnRowCommand="dgvMedico_RowCommand" runat="server" AutoGenerateColumns="false"
                AllowPaging="true" PageSize="10" OnPageIndexChanging="dgvMedico_PageIndexChanging">
                <Columns>
                    <asp:BoundField HeaderText="DNI" DataField="DNI" />
                    <asp:BoundField HeaderText="Nombre" DataField="Nombre" />
                    <asp:BoundField HeaderText="Apellido" DataField="Apellido" />
                    <asp:BoundField HeaderText="Matricula" DataField="Matricula" />
                    <asp:CheckBoxField HeaderText="Estado" DataField="Estado" />

                    <asp:TemplateField HeaderText="Acciones">
                        <ItemTemplate>
                            <asp:LinkButton ID="btnEditar" runat="server" CssClass="btn btn-sm btn-outline-primary"
                                CommandName="Editar"
                                CommandArgument='<%# Eval("IdMedico")%>'>
                                <i class="bi bi-pencil-square"></i>
                            </asp:LinkButton>

                            <asp:LinkButton ID="btnEliminar" runat="server" CssClass="btn btn-sm btn-outline-danger"
                                CommandName="Eliminar"
                                CommandArgument='<%# Eval("IdMedico")%>'
                                OnClientClick="return confirmarEliminar(this);">
                                <i class="bi bi-trash"></i>
                            </asp:LinkButton>

                            <asp:LinkButton ID="btnVerHorario" runat="server" CssClass="btn btn-outline-info btn-sm px-2"
                                CommandName="VerHorarios"
                                CommandArgument='<%# Eval("IdMedico")%>'>
                                <i class="bi bi-calendar3"></i>
                            </asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
            <hr class="my-4" />

            <div>
                <a href="MedicoForm.aspx" class="btn btn-primary py-2 fw-bold">
                    <i></i>Agregar Nuevo Medico
                </a>
            </div>
        </div>
    </div>

</asp:Content>
