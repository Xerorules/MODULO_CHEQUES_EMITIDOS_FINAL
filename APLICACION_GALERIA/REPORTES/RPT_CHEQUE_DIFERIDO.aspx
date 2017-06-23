<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="RPT_CHEQUE_DIFERIDO.aspx.cs" Inherits="APLICACION_GALERIA.Formulario_web15" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <CR:CrystalReportViewer ID="CrystalReportViewer_CHEQUE_DIFERIDO" runat="server" AutoDataBind="true" />
</asp:Content>
