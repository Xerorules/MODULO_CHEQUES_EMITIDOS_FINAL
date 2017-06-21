<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="FormConsultaVentas.aspx.cs" Inherits="APLICACION_GALERIA.Formulario_web16" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!-- ================================================= -->
    <!-- SCRIPT PARA BLOQUEAR LAS FLECHAS DE NAVEGACION -->
    <script type="text/javascript">
        {
            if (history.forward(1))
                location.replace(history.forward(1))
        }
    </script>

    <meta http-equiv="Expires" content="0" />
    <meta http-equiv="Pragma" content="no-cache" />
    <!-- ================================================= -->
    <script type="text/javascript">
        function jsDecimals(e) {

            var evt = (e) ? e : window.event;
            var key = (evt.keyCode) ? evt.keyCode : evt.which;
            if (key != null) {
                key = parseInt(key, 10);
                if ((key < 48 || key > 57) && (key < 96 || key > 105)) {
                    //Aca tenemos que reemplazar "Decimals" por "NoDecimals" si queremos que no se permitan decimales
                    if (!jsIsUserFriendlyChar(key, "Decimals")) {
                        return false;
                    }
                }
                else {
                    if (evt.shiftKey) {
                        return false;
                    }
                }
            }
            return true;
        }

        // Función para las teclas especiales
        //------------------------------------------
        function jsIsUserFriendlyChar(val, step) {
            // Backspace, Tab, Enter, Insert, y Delete
            if (val == 8 || val == 9 || val == 13 || val == 45 || val == 46) {
                return true;
            }
            // Ctrl, Alt, CapsLock, Home, End, y flechas
            if ((val > 16 && val < 21) || (val > 34 && val < 41)) {
                return true;
            }
            if (step == "Decimals") {
                if (val == 190 || val == 110) {  //Check dot key code should be allowed
                    return true;
                }
            }
            // The rest
            return false;
        }

    </script>
    <script type="text/javascript">


        function openModal() {
            $('#myModal').modal('show');
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
       <!-- -----------------------------Modal POPUP-------------------------------------------------------------->
    <div class="modal fade" id="myModal" tabindex="-1" role="dialog" aria-labelledby="myModalLabel">
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                    <img src="ICONOS/LOGO_GRUPO_DIONYS.png" width="75" height="35" />
                    <h3 class="modal-title bg-color-red" id="myModalLabel" style="text-align: center">"GRUPO DIONYS"</h3>

                </div>
                <div class="modal-body">
                    <h3 class="success" style="text-align: center; font-family: 'Segoe UI'">&nbsp DEBE LLENAR TODOS LOS FILTROS, INTENTE DE NUEVO</h3>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-default" data-dismiss="modal">Aceptar</button>
                </div>
            </div>
        </div>
    </div>
    <!-- -----------------------------Modal POPUP-------------------------------------------------------------->
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
       <asp:UpdatePanel ID="udpOutterUpdatePanel" runat="server">
            <ContentTemplate>
    <div class="form-group col-md-12  col-sm-12  col-xs-12 center-block">

        <div class="col-lg-3 col-sm-12 col-md-6 col-xs-12">
            <label style="color: black;">FECHA:</label>
            <asp:TextBox runat="server" ID="TXTFECHAINI" CssClass="form-control col-xs-12 col-sm-12" Style="text-transform: uppercase"
                Font-Bold="true" TextMode="Date" MaxLength="70"></asp:TextBox>
        </div>

        <div class="col-lg-4 col-sm-12 col-md-6 col-xs-12">
            <label style="color: black;">BANCO:</label>
            <asp:DropDownList runat="server" ID="CBOBANCOS" CssClass="form-control col-xs-12 col-sm-12" AutoPostBack="false">
            </asp:DropDownList>

        </div>


        <div class="col-lg-2 col-sm-12 col-md-6 col-xs-12">
            <label style="color: black;">MONEDA:</label>
            <asp:DropDownList runat="server" ID="CBOMONEDA" CssClass="form-control col-xs-12 col-sm-12" AutoPostBack="false">
                <asp:ListItem Value="">-MON-</asp:ListItem>
                <asp:ListItem Value="S">SOL</asp:ListItem>
                <asp:ListItem Value="D">DOLAR</asp:ListItem>
            </asp:DropDownList>

        </div>

        <div class="col-lg-2 col-sm-12 col-md-6 col-xs-12">
            <label style="color: black;">N° OPERACION:</label>
            <asp:TextBox runat="server" ID="TXTOPE" CssClass="form-control col-xs-12 col-sm-12" Height="35px" Font-Bold="true"
                placeholder="N° de operacion" MaxLength="70"></asp:TextBox>
        </div>
        
    
     
        <%--<div class="col-xs-12 col-md-4">
            <div>
                <label style="color: black; ">FILTRO DOC. VTA:</label>
            </div>
            
            <asp:DropDownList runat="server" ID="cboFTBV" CssClass="form-control col-md-2" Width="65" Height="32" AutoPostBack="true" OnSelectedIndexChanged="cboFTBV_SelectedIndexChanged">
                <asp:ListItem Value="">--</asp:ListItem>
                <asp:ListItem Value="FT">FT</asp:ListItem>
                <asp:ListItem Value="BV">BV</asp:ListItem>
            </asp:DropDownList>

            <asp:TextBox runat="server" ID="txtserie" CssClass="form-control col-md-3 col-xs-2" AutoPostBack="true" MaxLength="3" Width="60" Height="32" onkeydown="return jsDecimals(event);" placeholder="SERIE" OnTextChanged="txtserie_TextChanged"></asp:TextBox>
            
             <asp:TextBox runat="server" ID="txtnumero" AutoPostBack="true" CssClass="form-control col-md-3 col-xs-2" Width="75" Height="32" MaxLength="5" onkeydown="return jsDecimals(event);" placeholder="NUM" OnTextChanged="txtnumero_TextChanged"></asp:TextBox>
            <asp:TextBox runat="server" ID="txtfinal" AutoPostBack="true" Visible="false" CssClass="form-control col-md-3 col-xs-2" Width="75" Height="32" MaxLength="5" onkeydown="return jsDecimals(event);" placeholder="FINAL" OnTextChanged="txtfinal_TextChanged"></asp:TextBox>
            <div>
                <asp:CheckBox ID="CheckBox1" runat="server" AutoPostBack="true" Text="RANGO" CssClass="col-md-3 col-xs-2" Font-Size="Smaller" ToolTip="Chequear si desea registrar un rango de BV" OnCheckedChanged="CheckBox1_CheckedChanged" />
            </div>
           
        </div>--%>

        <br />
        <div class="col-lg-1 col-sm-12 col-md-6 col-xs-12" style="top: 5px;">

            <asp:Button ID="btnConsulta" runat="server" Text="BUSCAR" CssClass="btn btn-danger" Font-Bold="true" OnClick="btnConsulta_Click" />

        </div>
    
   
    </div>
 
    <div class="container-fluid">
       <div style="width: 100%; height: 750px; margin-top: 0px; overflow-y:auto;">
            
                <asp:GridView ID="dgvMOVIMIENTOS" runat="server"  CssClass="table table-bordered table-responsive table-condensed" BackColor="White"
                AutoGenerateColumns="False" DataKeyNames="ID_MOVIMIENTOS" Font-Size="Small" OnRowDataBound="dgvMOVIMIENTOS_RowDataBound">
                <EmptyDataTemplate>
                   < NO HAY DATOS
                </EmptyDataTemplate>
                <Columns>
                    <asp:BoundField DataField="ID_MOVIMIENTOS" HeaderText="CODIGO" HeaderStyle-HorizontalAlign="Center" ItemStyle-Height="35px">
                        <HeaderStyle HorizontalAlign="Center" BackColor="#0066cc" ForeColor="White" Font-Size="Smaller"></HeaderStyle>

                        <ItemStyle Height="35px"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField DataField="FECHA" HeaderText="FECHA" HeaderStyle-HorizontalAlign="Center" ItemStyle-Height="35px">
                        <HeaderStyle HorizontalAlign="Center" BackColor="#0066cc" ForeColor="White" Font-Size="Smaller"></HeaderStyle>

                        <ItemStyle Height="35px"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField DataField="DESCRIPCION" HeaderText="DESCRIPCION" HeaderStyle-HorizontalAlign="Center" ItemStyle-Height="35px"
                        ConvertEmptyStringToNull="true">
                        <HeaderStyle HorizontalAlign="Center" BackColor="#0066cc" ForeColor="White" Font-Size="Smaller"></HeaderStyle>

                        <ItemStyle Height="35px"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField DataField="CONCEPTO" HeaderText="CONCEPTO" HeaderStyle-HorizontalAlign="Center" ItemStyle-Height="35px">
                        <HeaderStyle HorizontalAlign="Center" BackColor="#0066cc" ForeColor="White" Font-Size="Smaller"></HeaderStyle>

                        <ItemStyle Height="35px"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField DataField="OPERACION" HeaderText="N° OPERACION" HeaderStyle-HorizontalAlign="Center" ItemStyle-Height="35px"
                        ConvertEmptyStringToNull="true">
                        <HeaderStyle HorizontalAlign="Center" BackColor="#0066cc" ForeColor="White" Font-Size="Smaller"></HeaderStyle>

                        <ItemStyle HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:BoundField DataField="IMPORTE" HeaderText="IMPORTE" DataFormatString="{0:N}" HeaderStyle-HorizontalAlign="Center" ItemStyle-Height="35px">
                        <HeaderStyle HorizontalAlign="Center" BackColor="#0066cc" ForeColor="White" Font-Size="Smaller"></HeaderStyle>

                        <ItemStyle HorizontalAlign="Right" />
                    </asp:BoundField>
                    <asp:BoundField DataField="CLIENTE" HeaderText="CLIENTE" HeaderStyle-HorizontalAlign="Center" ItemStyle-Height="35px" ConvertEmptyStringToNull="true">
                        <HeaderStyle HorizontalAlign="Center" BackColor="#0066cc" ForeColor="White" Font-Size="Smaller"></HeaderStyle>

                        <ItemStyle Height="35px"></ItemStyle>
                    </asp:BoundField>

                    <asp:BoundField DataField="LUGAR" HeaderText="LUGAR" HeaderStyle-HorizontalAlign="Center" ItemStyle-Height="35px">

                        <HeaderStyle HorizontalAlign="Center" BackColor="#0066cc" ForeColor="White" Font-Size="Smaller"></HeaderStyle>

                        <ItemStyle Height="35px"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField DataField="OBSERVACION" HeaderText="OBSERVACION" ItemStyle-Width="10%" ItemStyle-Height="35px" ControlStyle-Width="500">
                                    <HeaderStyle HorizontalAlign="Right" BackColor="#0066cc" ForeColor="White" Font-Size="Smaller" />
                                    <ItemStyle Height="35px" Width="500" />
                                </asp:BoundField>
                    <asp:BoundField DataField="COD_VENTA" HeaderText="AMARRE" HeaderStyle-HorizontalAlign="Center" ItemStyle-Height="35px">

                        <HeaderStyle HorizontalAlign="Center" BackColor="#0066cc" ForeColor="White" Font-Size="Smaller"></HeaderStyle>

                        <ItemStyle Height="35px"></ItemStyle>
                    </asp:BoundField>

                </Columns>
            </asp:GridView>
                      
       </div>
            
        
    </div>
    </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="btnConsulta" EventName="Click" />
                <%--<asp:AsyncPostBackTrigger ControlID="cboFTBV" EventName="SelectedIndexChanged"/>
                <asp:AsyncPostBackTrigger ControlID="txtserie" EventName="TextChanged"/>
                <asp:AsyncPostBackTrigger ControlID="txtnumero" EventName="TextChanged"/>
                <asp:AsyncPostBackTrigger ControlID="txtfinal" EventName="TextChanged"/>
                <asp:AsyncPostBackTrigger ControlID="CheckBox1" EventName="CheckedChanged"/>--%>
            </Triggers>
            </asp:UpdatePanel>
</asp:Content>
