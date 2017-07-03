<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="FORM_CHEQUERAS.aspx.cs" Inherits="APLICACION_GALERIA.Formulario_web1" %>

<%@ MasterType VirtualPath="~/Site1.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <link href="css/paginador.css" rel="stylesheet" />

    <script type="text/javascript">


        function openModal() {
            $('#myModal').modal('show');
        }

        function openModal1() {

            document.getElementById("<%= lblmensapopup.ClientID %>").value = "El número de inicio debe ser menor al de fin";

            $('#myModal1').modal('show');
        }

        function openModal167() {

            document.getElementById("<%= lblmensapopup.ClientID %>").value = "La fecha de emisión debe ser menor a la de vencimiento";

            $('#myModal1').modal('show');
        }

        function openModal2() {
            $('#myModal2').modal('show');
        }

        function openModal33() {
            $('#myModal33').modal('show');
        }

         function limpiarcuenta()
        {
            var txtcuentavar = document.getElementById("<%= TXTCUENTA.ClientID %>");
            txtcuentavar.disabled = true;
            
        }

         function reactivarcuenta()
         {
             document.getElementById("<%= TXTCUENTA.ClientID %>").value = "";
             document.getElementById("<%= txtCUENTACH.ClientID %>").value = "";
           var txtcuentavar = document.getElementById("<%= TXTCUENTA.ClientID %>");
            txtcuentavar.disabled = false;
            
        }

    </script>
    <script type="text/javascript">
        //funcion solo decimales
        function jsDecimals(e) {

            var evt = (e) ? e : window.event;
            var key = (evt.keyCode) ? evt.keyCode : evt.which;
            if (key != null) {
                key = parseInt(key, 10);
                if ((key < 48 || key > 57) && (key < 96 || key > 109)) {
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
    <!-- ========= AUTOCOMPLETAR DE CLIENTES POR DESCRIPCION ============ -->

    <script type="text/javascript">
        $(function () {
            $("[id$=TXTCUENTA]").autocomplete({
                source: function (request, response) {
                    $.ajax({
                        url: '<%=ResolveUrl("~/SERVICES/AUTOCOMPLETAR_MOVIMIENTOS.asmx/AUTOCOMPLETAR_CUENTAS") %>',
                        data: "{ 'prefix': '" + request.term + "'}",
                        dataType: "json",
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        success: function (data) {
                            response($.map(data.d, function (item) {
                                return {
                                    label: item.split('-')[0],
                                    val: item.split('-')[1]
                                }
                            }))
                        },
                        error: function (response) {
                            alert(response.responseText);
                        },
                        failure: function (response) {
                            alert(response.responseText);
                        }
                    });
                },
                select: function (e, i) {
                    $("[id$=TXTCUENTA]").val(i.item.text);
                    $("[id$=txtCUENTACH]").val(i.item.val);
                },
                minLength: 1
            });
        });
    </script>
    <asp:ScriptManager runat="server" ID="scriptm1"></asp:ScriptManager>
    <div style="margin-top: -0px;">
        <%-- <h2 style="color: black;  font-family:'Trebuchet MS', 'Lucida Sans Unicode', 'Lucida Grande', 'Lucida Sans', Arial, sans-serif;" class="">CHEQUERAS</h2>
        &nbsp--%>
    </div>

    <div class="container col-lg-4 col-md-4" style="font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif; background-color: darkcyan; top: 0px;">
        <div class="form-horizontal" style="position: relative; background-color: darkcyan; margin-top: 0px;">
           
            <div class="container col-lg-12 col-md-12" style="color: black; border-radius: 5px 5px 5px 5px; font-size: 12px; margin-top: -35px;">
                &nbsp
           <div class="form-horizontal col-xs-12 col-md-12 col-lg-12" style="position: relative; left: -30px;">
               <div class="form-group">
                   
                   <div class="col-xs-12 col-md-12">
                       <div id="provee" class="input-group" style="height: 35px; z-index: 1;">

                           <asp:TextBox runat="server" ID="TXTCUENTA" CssClass="form-control" Style="text-transform: uppercase" placeholder="Busqueda automática de cuentas" MaxLength="100"></asp:TextBox>
                           <span class="input-group-btn">
                               <asp:UpdatePanel runat="server" ID="UPDATECHEQUERAS">
                                   <ContentTemplate>
                                       <asp:ImageButton ID="BTNFILTRACHEQUERAS" runat="server" CssClass="btn btn-warning" ForeColor="Black" Text="IR" Height="34" ImageAlign="Middle"  ImageUrl="~/ICONOS/busqueda.png" OnClick="BTNFILTRACHEQUERAS_Click" />
                           
                                   </ContentTemplate>
                                   <Triggers>
                                       <asp:AsyncPostBackTrigger ControlID="BTNFILTRACHEQUERAS" EventName="Click"/>
                                   </Triggers>
                               </asp:UpdatePanel>
                               
                           </span>
                       </div>
                        <asp:UpdatePanel runat="server" ID="UpdatePanel1" UpdateMode="Always">
                                   <ContentTemplate>
                      <asp:Label runat="server" ID="lblmensajecuenta" ForeColor="Gold" Visible="false" Text="(*)Debe elegir una cuenta válida"/>
                                        <asp:TextBox ID="txtCUENTACH" runat="server" BorderStyle="None" BackColor="darkcyan" AutoPostBack="true" ForeColor="darkcyan" Height="1"></asp:TextBox>
                                       </ContentTemplate>
                            </asp:UpdatePanel>
                   </div>

               </div>
               <asp:UpdatePanel runat="server" ID="UPCUERPO">
                   <ContentTemplate>

               <div class="form-group">
                   <label class="control-label col-md-3" style="color: white">F.EMISION:</label>
                   <div class="col-xs-12 col-md-9 col-sm-9">
                       <asp:TextBox runat="server" ID="TXTFECHAEMISION" CssClass="form-control" type="date" MaxLength="45" Height="35px"></asp:TextBox>
                       <%--VALIDADOR--%>
                       <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server"
                           ControlToValidate="TXTFECHAEMISION"
                           ErrorMessage="(*)La fecha de emision es necesaria"
                           ForeColor="Gold"
                           ValidationGroup="Registro" Display="Dynamic">
                       </asp:RequiredFieldValidator>

                   </div>

               </div>

                       
               <div class="form-group">
                   <label class="control-label col-md-3 col-sm-3" style="color: white">F. VCTO:</label>
                   <div class="col-xs-12 col-md-9 col-sm-9">
                       <asp:TextBox runat="server" ID="TXTFECHAVCTO" CssClass="form-control" type="date" MaxLength="45" Height="35px"></asp:TextBox>
                       <%--VALIDADOR--%>
                       <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server"
                           ControlToValidate="TXTFECHAVCTO"
                           ErrorMessage="(*)La fecha de emision es necesaria"
                           ForeColor="Gold"
                           ValidationGroup="Registro" Display="Dynamic">
                       </asp:RequiredFieldValidator>

                   </div>

               </div>
               <div class="form-group">
                   <label class="control-label col-md-3" style="color: white">N° INI:</label>
                   <div class="col-xs-12 col-md-9 col-sm-9">
                       <asp:TextBox runat="server" ID="TXTXNUMINI" CssClass="form-control" placeholder="Ingrese número inicial" MaxLength="100" onkeydown="return jsDecimals(event);"></asp:TextBox>
                       <%--VALIDADOR--%>
                       <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server"
                           ControlToValidate="TXTXNUMINI"
                           ErrorMessage="(*)El Número de cheque es requerido"
                           ForeColor="Gold"
                           ValidationGroup="Registro" Display="Dynamic">
                       </asp:RequiredFieldValidator>

                   </div>
               </div>
               <%--</div>
                <div class="form-horizontal col-xs-12 col-md-4 col-lg-4" style="position: relative;">--%>
               <div class="form-group">
                   <label class="control-label col-md-3" style="color: white">N° FINAL:</label>
                   <div class="col-xs-12 col-md-9 col-sm-9">
                       <asp:TextBox runat="server" ID="TXTNUMFIN" CssClass="form-control" placeholder="Ingrese número final" MaxLength="100" onkeydown="return jsDecimals(event);"></asp:TextBox>
                       <%--VALIDADOR--%>
                       <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server"
                           ControlToValidate="TXTNUMFIN"
                           ErrorMessage="(*)El Número de cheque es requerido"
                           ForeColor="Gold"
                           ValidationGroup="Registro" Display="Dynamic">
                       </asp:RequiredFieldValidator>

                   </div>
               </div>

               <div class="form-group">
                   <label class="control-label col-md-3" style="color: white">TIPO:</label>
                   <div class="col-xs-12 col-md-9 col-sm-9">
                       <asp:DropDownList ID="CBOTIPO" runat="server" CssClass="form-control">
                           <asp:ListItem Text="-SELECCIONE-" Value="" Selected="True"></asp:ListItem>
                           <asp:ListItem Text="PAGO DIFERIDO" Value="DIFERIDO"></asp:ListItem>
                           <asp:ListItem Text="CLASICO" Value="CLASICO"></asp:ListItem>
                       </asp:DropDownList>
                       <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server"
                           ControlToValidate="CBOTIPO"
                           ErrorMessage="(*)Seleccione un tipo"
                           ForeColor="Gold"
                           ValidationGroup="Registro" Display="Dynamic">
                       </asp:RequiredFieldValidator>

                   </div>
               </div>

               <%--</div>
                <div class="form-horizontal col-xs-12 col-md-4 col-lg-4" style="position: relative;">--%>
               <div class="form-group">
                   <label class="control-label col-md-3" style="color: white">OBSERV:</label>
                   <div class="col-xs-12 col-md-9 col-sm-9">
                       <asp:TextBox runat="server" ID="TXTOBS" CssClass="form-control" placeholder="Ingrese observación" MaxLength="250"></asp:TextBox>
                   </div>
               </div>
               &nbsp
            <div class="col-md-12 col-sm-12 col-xs-12">


                <div class="col-md-6 col-sm-6">
                    <asp:Button runat="server" CssClass="btn btn-info col-xs-12" ForeColor="White" Font-Bold="true" Text="GRABAR" ValidationGroup="Registro" ID="btnRegistrar" AccessKey="G" OnClick="btnRegistrar_Click" />
                    &nbsp
                </div>

                <div class="col-md-6 col-sm-6">
                    <asp:Button runat="server" CssClass="btn btn-info col-xs-12" Text="LIMPIAR" Font-Bold="true" ForeColor="White" ID="btnCancelar" AccessKey="C" OnClick="btnCancelar_Click" />
                    &nbsp
                </div>



                   </ContentTemplate>
                   <Triggers>
                       <asp:AsyncPostBackTrigger  ControlID="btnRegistrar" EventName="Click"/>
                   </Triggers>
               </asp:UpdatePanel>

            </div>
           </div>
            </div>

        </div>
    </div>
    <!-- -----------------------------Modal Registro-------------------------------------------------------------->
    <div class="modal fade" id="myModal" tabindex="-1" role="dialog" aria-labelledby="myModalLabel">
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                    <img src="ICONOS/LOGO_GRUPO_DIONYS.png" width="75" height="35" />
                    <h3 class="modal-title bg-color-green" id="myModalLabel" style="text-align: center">"GRUPO DIONYS"</h3>

                </div>
                <div class="modal-body">
                    <h3 class="success" style="text-align: center; font-family: 'Segoe UI';">&nbsp Operacion realizada correctamente</h3>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-success" data-dismiss="modal">Aceptar</button>
                </div>
            </div>
        </div>
    </div>
    <!-- -----------------------------Modal Registro-------------------------------------------------------------->
    <!-- -----------------------------Modal RegistroError-------------------------------------------------------------->
    <div class="modal fade" id="myModal1" tabindex="-1" role="dialog" aria-labelledby="myModalLabel1">
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                    <img src="ICONOS/LOGO_GRUPO_DIONYS.png" width="75" height="35" />
                    <h3 class="modal-title bg-color-red" id="myModalLabel1" style="text-align: center">"GRUPO DIONYS"</h3>

                </div>
                <div class="modal-body" style="text-align: center; font-family: 'Segoe UI'">
                    <asp:Label runat="server" ID="lblmensapopup" CssClass="danger">&nbsp No se pudo realizar la operación</asp:Label>
                    <%--<h3 class="danger" style="text-align: center; font-family: 'Segoe UI'">&nbsp No se pudo realizar la operación</h3>--%>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-danger" data-dismiss="modal">Aceptar</button>
                </div>
            </div>
        </div>
    </div>
    <!-- -----------------------------Modal RegistroError-------------------------------------------------------------->
    <!-- -----------------------------Modal Actualizar-------------------------------------------------------------->
    <div class="modal fade" id="myModal2" tabindex="-1" role="dialog" aria-labelledby="myModalLabel2">
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                    <img src="ICONOS/LOGO_GRUPO_DIONYS.png" width="80" height="35" />
                    <h3 class="modal-title bg-color-red" id="myModalLabel2" style="text-align: center">"GRUPO DIONYS"</h3>

                </div>
                <div class="modal-body">
                    <h3 class="warning" style="text-align: center; font-family: 'Segoe UI'">&nbsp No se llenaron los campos requeridos</h3>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-warning" data-dismiss="modal">Aceptar</button>
                </div>
            </div>
        </div>
    </div>
    <!-- -----------------------------Modal RegistroError-------------------------------------------------------------->
    <!-- -----------------------------Modal No valido-------------------------------------------------------------->
    <div class="modal fade" id="myModal4" tabindex="-1" role="dialog" aria-labelledby="myModalLabel2">
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                    <img src="ICONOS/LOGO_GRUPO_DIONYS.png" width="80" height="35" />
                    <h3 class="modal-title bg-color-red" id="myModalLabel4" style="text-align: center">"GRUPO DIONYS"</h3>

                </div>
                <div class="modal-body">
                    <h3 class="danger" style="text-align: center; font-family: 'Segoe UI'">&nbsp Debe escoger una cuenta valida de la lista</h3>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-danger" data-dismiss="modal">Aceptar</button>
                </div>
            </div>
        </div>
    </div>
    <!-- -----------------------------Modal RegistroError-------------------------------------------------------------->
    <asp:UpdatePanel runat="server" ID="UPDGRILLA_CHEQUERAS">
        <ContentTemplate>
 <div class="container col-lg-8 col-md-8" style="font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif; background-color: darkcyan; left: -40px;">
        <div style="width: 105%;">
            <asp:GridView ID="dgvBANCOS" runat="server" CssClass="table table-bordered table-responsive table-condensed" Font-Size="Small"
                HeaderStyle-BackColor="#3366ff" HeaderStyle-ForeColor="White" BackColor="White" OnPageIndexChanging="dgvBANCOS_PageIndexChanging" AutoGenerateColumns="False" PageSize="20" DataKeyNames="ID_CHEQUERA" OnSelectedIndexChanged="dgvBANCOS_SelectedIndexChanged" AllowPaging="True" OnRowCommand="dgvBANCOS_RowCommand">

                <HeaderStyle BackColor="#3366FF" ForeColor="White"></HeaderStyle>

                <PagerStyle CssClass="pagination-ys" HorizontalAlign="Center" />
                <EmptyDataTemplate>
                    No hay registros 
                </EmptyDataTemplate>
                <Columns>
                    <asp:BoundField DataField="ID_CHEQUERA" HeaderText="COD" HeaderStyle-Font-Size="Smaller">
                        <HeaderStyle Font-Size="Smaller"></HeaderStyle>

                        <ItemStyle Width="30px" />
                    </asp:BoundField>

                    <asp:BoundField DataField="CUENTA" HeaderText="N° CUENTA" HeaderStyle-Font-Size="Smaller">
                        <ControlStyle Width="300px" />
                        <HeaderStyle Font-Size="Smaller"></HeaderStyle>

                        <ItemStyle Width="400px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="BANCO" HeaderText="BANCO" HeaderStyle-Font-Size="Smaller">
                        <ControlStyle Width="400px" />
                        <HeaderStyle Font-Size="Smaller"></HeaderStyle>

                        <ItemStyle Width="400px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="MONEDA" HeaderText="MON" HeaderStyle-Font-Size="Smaller">
                        <ControlStyle Width="30px" />
                        <HeaderStyle Font-Size="Smaller"></HeaderStyle>

                        <ItemStyle Width="90px" />
                    </asp:BoundField>

                    <asp:BoundField DataField="N_INI" HeaderText="N° INI" HeaderStyle-Font-Size="Smaller">
                        <HeaderStyle Font-Size="Smaller"></HeaderStyle>

                        <ItemStyle Width="90px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="N_FIN" HeaderText="N° FIN" HeaderStyle-Font-Size="Smaller">
                        <HeaderStyle Font-Size="Smaller"></HeaderStyle>

                        <ItemStyle Width="90px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="N_CORRELATIVO" HeaderText="N° CORR." HeaderStyle-Font-Size="Smaller" ItemStyle-Width="90px">
                        <HeaderStyle Font-Size="Smaller"></HeaderStyle>

                        <ItemStyle Width="50px"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField DataField="F_EMISION" HeaderText="F.RECEPCION" HeaderStyle-Font-Size="Smaller">
                        <HeaderStyle Font-Size="Smaller"></HeaderStyle>

                        <ItemStyle Width="90px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="F_VENCIMIENTO" HeaderText="F.VCTO" HeaderStyle-Font-Size="Smaller">
                        <HeaderStyle Font-Size="Smaller"></HeaderStyle>

                        <ItemStyle Width="90px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="TIPO" HeaderText="TIPO" HeaderStyle-Font-Size="Smaller" ItemStyle-Width="50px">
                        <HeaderStyle Font-Size="Smaller"></HeaderStyle>

                        <ItemStyle Width="50px"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField DataField="OBSERVACION" HeaderText="OBS" DataFormatString="{0:N}" HeaderStyle-Font-Size="Smaller">

                        <HeaderStyle Font-Size="Smaller"></HeaderStyle>
                    </asp:BoundField>

                    <asp:BoundField DataField="ESTADO" HeaderText="ESTADO" HeaderStyle-Font-Size="Smaller" Visible="false">

                        <HeaderStyle Font-Size="Smaller"></HeaderStyle>
                    </asp:BoundField>

                    <asp:TemplateField ItemStyle-HorizontalAlign="Center" ItemStyle-Width="50px">
                        <ItemTemplate>
                            <asp:ImageButton ID="ImageButtonEditar" runat="server" CommandName="EDITAR" Width="20" Height="20" ToolTip="Editar" ImageUrl="~/ICONOS/writing.png" OnClientClick="return GetSelectedRow(this)" Font-Size="Smaller" />
                        </ItemTemplate>

                        <ItemStyle HorizontalAlign="Center" Width="50px"></ItemStyle>
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-HorizontalAlign="Center" ItemStyle-Width="50px">
                        <ItemTemplate>
                            <asp:ImageButton ID="ImageButtonEliminar" runat="server" CommandName="ELIMINAR" Width="20" Height="20" ToolTip="Eliminar" ImageUrl="~/ICONOS/rubbish.png" OnClientClick="if (!confirm('Esta seguro de eliminar el registro?')) return false;" Font-Size="Smaller" />
                        </ItemTemplate>

                        <ItemStyle HorizontalAlign="Center" Width="50px"></ItemStyle>
                    </asp:TemplateField>

                    <%--<asp:TemplateField HeaderText="IMG" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="40px">
                                 <ItemTemplate>
                                     <asp:ImageButton ID="ImageButton1" CommandName="IMAGEN" runat="server" ImageAlign="Middle" ImageUrl="~/imagenes/camera.png"
                                         Width="20px" Height="20px" Style="cursor: pointer" OnCommand="lnkCustDetails_Click" />
                                 </ItemTemplate>
                             </asp:TemplateField>--%>
                </Columns>
            </asp:GridView>
        </div>
    </div>
        </ContentTemplate>
        <Triggers>
        </Triggers>
    </asp:UpdatePanel>
   


</asp:Content>
