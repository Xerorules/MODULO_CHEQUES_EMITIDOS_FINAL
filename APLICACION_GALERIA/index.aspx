<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="index.aspx.cs" Inherits="APLICACION_GALERIA.Formulario_web11" %>
<%@ MasterType VirtualPath="~/Site1.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script type="text/javascript">


        function openModal5() {
            $('#myModal5').modal('show');
        }

        function openModal1() {
            $('#myModal1').modal('show');
        }

        function openModal2() {
            $('#myModal2').modal('show');
        }

        function openModal33() {
            $('#myModal33').modal('show');
        }

        
    </script>
     <div class="row">


        <div class="col-sm-6 col-md-4 col-md-offset-4">
            <h1 class="text-center">INGRESAR AL SISTEMA</h1>
            <br />
            <div class="account-wall">
                <img class="profile-img center-block visible-md visible-lg visible-sm" src="ICONOS/LOGO_GRUPO_DIONYS.png" height="90" width="180"/>
                    <br />
                <div class="form-signin">

                    <asp:DropDownList ID="CBOEMPRESA" CssClass="form-control" runat="server"></asp:DropDownList>
                    <br />
                    <asp:TextBox runat="server" CssClass="form-control" ID="txtusuario" MaxLength="50" placeholder="Usuario"></asp:TextBox>
                    <br />
                    <asp:TextBox runat="server" CssClass="form-control" ID="txtcontraseña" TextMode="Password" MaxLength="50" placeholder="Contraseña"></asp:TextBox>
                    <br />
                    <asp:Button runat="server" ID="btnACEPTAR" CssClass="btn btn-info btn-lg btn-block" Text="INGRESAR" OnClick="btnACEPTAR_Click1" />
                    <br />
                    <asp:Button runat="server" ID="btnLIMPIAR" CssClass="btn btn-info btn-lg btn-block" Text="LIMPIAR" />

                    
                    
                </div>
            </div>
            
        </div>
            <!-- -----------------------------Modal RegistroError-------------------------------------------------------------->
    <div class="modal fade" id="myModal5" tabindex="-1" role="dialog" aria-labelledby="myModalLabel5">
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                    <img src="ICONOS/LOGO_GRUPO_DIONYS.png" width="75" height="35" />
                    <h3 class="modal-title bg-color-red" id="myModalLabel1" style="text-align: center">"GRUPO DIONYS"</h3>

                </div>
                <div class="modal-body">
                    <h3 class="danger" style="text-align: center; font-family: 'Segoe UI'">&nbsp Los datos ingresados son INCORRECTOS!</h3>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-danger" data-dismiss="modal">Aceptar</button>
                </div>
            </div>
        </div>
    </div>
    <!-- -----------------------------Modal RegistroError-------------------------------------------------------------->

        
        <asp:HiddenField runat="server" ID="hfsede" />
        <asp:HiddenField runat="server" ID="hfid_emp" />

    </div>
</asp:Content>
