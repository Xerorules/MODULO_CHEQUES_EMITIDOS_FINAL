<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="NESTED_GRID.aspx.cs" Inherits="APLICACION_GALERIA.Formulario_web12" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:GridView ID="GridView1" runat="server">
        <Columns>
            <asp:TemplateField>
                <ItemTemplate>
                    <a href="Javascript: expandcollapse('<%# Eval("ID_CHEQUERA") %>');"></a>
                    <img src="ICONOS/AUMENTAR.png" border="0" id="img<%# Eval("ID_CHEQUERA") %>" />



                </ItemTemplate>



            </asp:TemplateField>

        </Columns>

    </asp:GridView>
</asp:Content>
