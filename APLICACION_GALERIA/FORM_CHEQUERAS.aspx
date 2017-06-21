<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="FORM_CHEQUERAS.aspx.cs" Inherits="APLICACION_GALERIA.Formulario_web1" %>
<%@ MasterType VirtualPath="~/Site1.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
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

    <div style="margin-top:-50px;">
        <h2 style="color: black;  font-family:'Trebuchet MS', 'Lucida Sans Unicode', 'Lucida Grande', 'Lucida Sans', Arial, sans-serif;" class="">CHEQUERAS</h2>
        &nbsp
    </div>
    
    <div class="container-fluid col-lg-4 col-md-4" style="font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif; background-color:#F5F3EE; top:0px;">
        <div class="form-horizontal" style="position: relative; background-color:#F5F3EE; margin-top:0px;">
                      
            
               <asp:TextBox ID="txtCUENTACH" runat="server" BorderStyle="None" BackColor="#F5F3EE" ForeColor="#F5F3EE" Height="1"></asp:TextBox>
            
            
            <div class="container col-lg-10 col-md-10" style="color: black; border-radius: 5px 5px 5px 5px; font-size:12px; margin-top:-35px; ">
                &nbsp
            <div class="form-group">
                <label class="control-label col-md-4" style="color: black">CUENTA:</label>
                <div class="col-xs-12 col-md-8">

                    <asp:TextBox runat="server" ID="TXTCUENTA" CssClass="form-control" Style="text-transform: uppercase" placeholder="Busqueda automática de cuentas" MaxLength="100" ></asp:TextBox>
                    <%--VALIDADOR--%>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server"
                        ControlToValidate="TXTCUENTA"
                        ErrorMessage="(*)Debe escojer una cuenta"
                        ForeColor="Red"
                        ValidationGroup="Registro" Display="Dynamic">
                    </asp:RequiredFieldValidator>
                   
                </div>

            </div>

                <div class="form-group">
                    <label class="control-label col-md-4" style="color: black">F. EMISIÓN:</label>
                    <div class="col-xs-12 col-md-8 col-sm-8">
                        <asp:TextBox runat="server" ID="TXTFECHAEMISION" CssClass="form-control" type="date" MaxLength="45"  Height="35px"></asp:TextBox>
                        <%--VALIDADOR--%>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server"
                            ControlToValidate="TXTFECHAEMISION"
                            ErrorMessage="(*)La fecha de emision es necesaria"
                            ForeColor="Red"
                            ValidationGroup="Registro" Display="Dynamic">
                        </asp:RequiredFieldValidator>
                       
                    </div>

                </div>

                <div class="form-group">
                    <label class="control-label col-md-4" style="color: black">N° INICIAL:</label>
                    <div class="col-xs-12 col-md-8 col-sm-8">
                        <asp:TextBox runat="server" ID="TXTXNUMINI" CssClass="form-control" placeholder="Ingrese número inicial" MaxLength="100"></asp:TextBox>
                        <%--VALIDADOR--%>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server"
                            ControlToValidate="TXTXNUMINI"
                            ErrorMessage="(*)El Número de cheque es requerido"
                            ForeColor="Red"
                            ValidationGroup="Registro" Display="Dynamic">
                        </asp:RequiredFieldValidator>
                        
                    </div>
                </div>

                <div class="form-group">
                    <label class="control-label col-md-4" style="color: black">N° FINAL:</label>
                    <div class="col-xs-12 col-md-8 col-sm-8">
                        <asp:TextBox runat="server" ID="TXTNUMFIN" CssClass="form-control" placeholder="Ingrese número final" MaxLength="100" ></asp:TextBox>
                        <%--VALIDADOR--%>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server"
                            ControlToValidate="TXTNUMFIN"
                            ErrorMessage="(*)El Número de cheque es requerido"
                            ForeColor="Red"
                            ValidationGroup="Registro" Display="Dynamic">
                        </asp:RequiredFieldValidator>
                        
                    </div>
                </div>

                 <div class="form-group">
                    <label class="control-label col-md-4 col-sm-4" style="color: black">F. VCTO:</label>
                    <div class="col-xs-12 col-md-8 col-sm-8">
                        <asp:TextBox runat="server" ID="TXTFECHAVCTO" CssClass="form-control" type="date" MaxLength="45"  Height="35px"></asp:TextBox>
                        <%--VALIDADOR--%>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server"
                            ControlToValidate="TXTFECHAVCTO"
                            ErrorMessage="(*)La fecha de emision es necesaria"
                            ForeColor="Red"
                            ValidationGroup="Registro" Display="Dynamic">
                        </asp:RequiredFieldValidator>
                        
                    </div>

                </div>
                
                <div class="form-group">
                    <label class="control-label col-md-4" style="color: black">TIPO:</label>
                    <div class="col-xs-12 col-md-8 col-sm-8">
                        <asp:DropDownList ID="CBOTIPO" runat="server" CssClass="form-control">
                            <asp:ListItem Text="-SELECCIONE-" Value="" Selected="True"></asp:ListItem>
                            <asp:ListItem Text="PAGO DIFERIDO" Value="DIFERIDO"></asp:ListItem>
                            <asp:ListItem Text="CLASICO" Value="CLASICO"></asp:ListItem>
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server"
                            ControlToValidate="CBOTIPO"
                            ErrorMessage="(*)Seleccione un tipo"
                            ForeColor="Red"
                            ValidationGroup="Registro" Display="Dynamic">
                        </asp:RequiredFieldValidator>
                       
                    </div>
                </div>
                <div class="form-group">
                    <label class="control-label col-md-4" style="color: black">OBSERVACION:</label>
                    <div class="col-xs-12 col-md-8 col-sm-8">
                        <asp:TextBox runat="server" ID="TXTOBS" CssClass="form-control" placeholder="Ingrese número inicial" MaxLength="100"></asp:TextBox>
                       
                        
                    </div>
                </div>
                &nbsp
            <div class="col-md-12 col-sm-12 col-xs-12">


                <div class="col-md-6 col-sm-6">
                    <asp:Button runat="server" CssClass="btn btn-info col-xs-12" ForeColor="White" Font-Bold="true" Text="GRABAR" ValidationGroup="Registro" ID="btnRegistrar" AccessKey="G" OnClick="btnRegistrar_Click"  />
                     &nbsp
                </div>
                
                <div class="col-md-6 col-sm-6">
                    <asp:Button runat="server" CssClass="btn btn-info col-xs-12" Text="CANCELAR" Font-Bold="true" ForeColor="White" ID="btnCancelar" AccessKey="C" />
                    &nbsp
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
                <div class="modal-body">
                    <h3 class="danger" style="text-align: center; font-family: 'Segoe UI'">&nbsp No se pudo realizar la operación</h3>
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

     <div style="overflow-y: scroll;">
                     <asp:GridView ID="dgvBANCOS" runat="server" CssClass="table-striped table-bordered table-responsive table-condensed" BackColor="White" AutoGenerateColumns="False" DataKeyNames="ID_CHEQUERA" Font-Size="Small" OnSelectedIndexChanged="dgvBANCOS_SelectedIndexChanged">

                         <Columns>
                             <asp:BoundField DataField="ID_CHEQUERA" HeaderText="COD" HeaderStyle-Font-Size="Smaller">
                                 <ItemStyle Width="30px" />
                             </asp:BoundField>
                             <asp:BoundField DataField="CUENTA" HeaderText="N° CUENTA - BANCO - MON" HeaderStyle-Font-Size="Smaller">
                                 <ItemStyle Width="200px" />
                             </asp:BoundField>
                             <asp:BoundField DataField="RANGO" HeaderText="RANGO" HeaderStyle-Font-Size="Smaller">
                                 <ItemStyle Width="90px" />
                             </asp:BoundField>
                             <asp:BoundField DataField="F_EMISION" HeaderText="FECHA COBRO" HeaderStyle-Font-Size="Smaller">
                                 <ItemStyle Width="90px" />
                             </asp:BoundField>
                             <asp:BoundField DataField="F_VENCIMIENTO" HeaderText="FECHA COBRO" HeaderStyle-Font-Size="Smaller">
                                 <ItemStyle Width="90px" />
                             </asp:BoundField>
                             <asp:BoundField DataField="N_CORRELATIVO" HeaderText="CORRELATIVO" HeaderStyle-Font-Size="Smaller" ItemStyle-Width="50px" />
                             <asp:BoundField DataField="TIPO" HeaderText="TIPO" HeaderStyle-Font-Size="Smaller" />
                             <asp:BoundField DataField="OBSERVACION" HeaderText="OBSERVACION" DataFormatString="{0:N}" HeaderStyle-Font-Size="Smaller" />
                             
                             <asp:BoundField DataField="ESTADO" HeaderText="ESTADO" HeaderStyle-Font-Size="Smaller" />

                             <asp:TemplateField ItemStyle-HorizontalAlign="Center" ItemStyle-Width="50px">
                                 <ItemTemplate>
                                     <asp:LinkButton ID="LinkButtonEditar" runat="server" CommandName="EDITAR" OnClientClick="return GetSelectedRow(this)" Font-Size="Smaller">EDITAR</asp:LinkButton>
                                   </ItemTemplate>
                             </asp:TemplateField>
                            <asp:TemplateField ItemStyle-HorizontalAlign="Center" ItemStyle-Width="50px">
                                 <ItemTemplate>
                                    <asp:LinkButton ID="LinkButtonEliminar" runat="server" CommandName="ELIMINAR" OnClientClick="if (!confirm('Esta seguro de eliminar el registro?')) return false;" Font-Size="Smaller">ELIMINAR</asp:LinkButton>
                                 </ItemTemplate>
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

</asp:Content>
