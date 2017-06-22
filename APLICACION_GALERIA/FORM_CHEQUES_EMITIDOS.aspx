<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="FORM_CHEQUES_EMITIDOS.aspx.cs" Inherits="APLICACION_GALERIA.Formulario_web13" %>

<%@ MasterType VirtualPath="~/Site1.master" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <link href="css/font-awesome.css" rel="stylesheet" />
    <script type="text/javascript">

        function openModalForm() {
            $('#myModalForm').modal('show');
        }

        function openModalFormClose() {
            $('#myModalForm').modal('hide');
        }

        function openModal() {
            $('#myModal').modal('show');
        }

        function openModal2() {
            $('#myModal2').modal('show');
        }

        function openModal9() {
            $('#myModal9').modal('show');
        }

        function openModal01() {
            $('#myModal01').modal('show');
        }

        function openModalF_MAYOR() {
            $('#myModalFECHA').modal('show');
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
    <!--SCRIPT PARA HACER LA ANIMACION DE DESAPARECER EL LABEL DE ERROR -->
    <script>
        $(document).ready(function () {
            $('#provee').click(function () {
                $('#test').fadeOut('slow');
            });
        });
    </script>

    <script type="text/javascript">
        function addCommas(clientID) {

            var nStr = document.getElementById(clientID.id).value;

            nStr += '';
            x = nStr.split('.');
            if (!x[0]) {
                x[0] = "0";

            }
            x1 = x[0];
            if (!x[1]) {
                x[1] = "00";
            }
            x2 = x.length > 1 ? '.' + x[1] : '';
            var rgx = /(\d+)(\d{3})/;
            while (rgx.test(x1)) {
                x1 = x1.replace(rgx, '$1' + ',' + '$2');
            }

            document.getElementById(clientID.id).value = x1 + x2;
            return true;

        }
    </script>
    <script type="text/javascript">
        function ChangeColor(GridViewId, SelectedRowId) {
            var GridViewControl = document.getElementById(GridViewId);
            if (GridViewControl != null) {
                var GridViewRows = GridViewControl.rows;
                if (GridViewRows != null) {
                    var SelectedRow = GridViewRows[SelectedRowId];
                    //Remove Selected Row color if any
                    for (var i = 1; i < GridViewRows.length; i++) {
                        var row = GridViewRows[i];
                        if (row == SelectedRow) {
                            //Apply Yellow color to selected Row
                            row.style.backgroundColor = "#ffffda";
                        }
                        else {
                            //Apply White color to rest of rows
                            row.style.backgroundColor = "#ffffff";
                        }
                    }

                }
            }

        }
    </script>
    <style>
        .hide {
            opacity: 0;
        }

        .fade-out {
            transition: 1s linear all;
        }
    </style>

    <style type="text/css">
        .Background {
            background-color: Black;
            filter: alpha(opacity=90);
            opacity: 0.2;
        }

        .Background2 {
            background-color: Black;
            filter: alpha(opacity=90);
            opacity: 0.2;
        }

        .Popup {
            background-color: #FFFFFF;
            border-width: 3px;
            border-radius: 5px;
            border-style: ridge;
            border-color: black;
            padding-top: 6px;
            padding-left: 10px;
            width: 600px;
            height: 380px;
            opacity: 50;
        }

        .Popup2 {
            background-color: #FFFFFF;
            border-width: 3px;
            border-radius: 5px;
            border-style: ridge;
            border-color: black;
            padding-top: 6px;
            padding-left: 10px;
            width: 700px;
            height: 380px;
            opacity: 50;
        }

        .Popup7 {
            background-color: #FFFFFF;
            border-width: 3px;
            border-radius: 5px;
            border-style: ridge;
            border-color: black;
            padding-top: 6px;
            padding-left: 10px;
            width: 700px;
            height: 450px;
            opacity: 50;
        }
       

        .lbl {
            font-size: 12px;
            font-style: normal;
            font-weight: bold;
        }
    </style>
    <!-- ========= AUTOCOMPLETAR DE CLIENTES POR DESCRIPCION ============ -->

    <script type="text/javascript">
        $(function () {
            $("[id$=TXTCUENTA2]").autocomplete({
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
                    $("[id$=TXTCUENTA2]").val(i.item.text);
                    $("[id$=txtID_CUENTA]").val(i.item.val);
                    if (i.item.val != '') {
                        openModalForm();
                    }
                },
                minLength: 1
            });
        });


        var prm = Sys.WebForms.PageRequestManager.getInstance();
        if (prm != null) {
            prm.add_endRequest(function (sender, e) {
                if (sender._postBackSettings.panelsToUpdate != null) {

                    $(function () {
                        $("[id$=TXTCUENTA2]").autocomplete({
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
                                $("[id$=TXTCUENTA2]").val(i.item.text);
                                $("[id$=txtID_CUENTA]").val(i.item.val);
                                if (i.item.val != '') {
                                    openModalForm();
                                }
                            },
                            minLength: 1
                        });
                    });
                }
            });
        };



    </script>




    <script type="text/javascript">
        $(function () {
            $("[id$=TXTPROOVEEDOR]").autocomplete({
                source: function (request, response) {
                    $.ajax({
                        url: '<%=ResolveUrl("~/SERVICES/AUTOCOMPLETAR_MOVIMIENTOS.asmx/AUTOCOMPLETAR_PROVEEDORES") %>',
                        data: "{ 'prefix': '" + request.term + "'}",
                        dataType: "json",
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        success: function (data) {
                            response($.map(data.d, function (item) {
                                return {
                                    label: item.split('**')[0],
                                    val: item.split('**')[1]
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
                    $("[id$=TXTPROOVEEDOR]").val(i.item.text);
                    $("[id$=txtPROO]").val(i.item.val);
                },
                minLength: 1

            });
        });


        var prm = Sys.WebForms.PageRequestManager.getInstance();
        if (prm != null) {
            prm.add_endRequest(function (sender, e) {
                if (sender._postBackSettings.panelsToUpdate != null) {

                    $(function () {
                        $("[id$=TXTPROOVEEDOR]").autocomplete({
                            source: function (request, response) {
                                $.ajax({
                                    url: '<%=ResolveUrl("~/SERVICES/AUTOCOMPLETAR_MOVIMIENTOS.asmx/AUTOCOMPLETAR_PROVEEDORES") %>',
                                    data: "{ 'prefix': '" + request.term + "'}",
                                    dataType: "json",
                                    type: "POST",
                                    contentType: "application/json; charset=utf-8",
                                    success: function (data) {
                                        response($.map(data.d, function (item) {
                                            return {
                                                label: item.split('**')[0],
                                                val: item.split('**')[1]
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
                                $("[id$=TXTPROOVEEDOR]").val(i.item.text);
                                $("[id$=txtPROO]").val(i.item.val);
                            },
                            minLength: 1

                        });
                    });


                    var prm = Sys.WebForms.PageRequestManager.getInstance();
                    if (prm != null) {
                        prm.add_endRequest(function (sender, e) {
                            if (sender._postBackSettings.panelsToUpdate != null) {



                            }
                        });
                    };

                }
            });
        };


    </script>
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <div style="margin-top: -30px;">
        <%--<h2 style="color: black; font-family: 'Trebuchet MS', 'Lucida Sans Unicode', 'Lucida Grande', 'Lucida Sans', Arial, sans-serif;" class="">EMISION DE CHEQUES</h2>--%>
        &nbsp
    </div>
    <div class="visible-xs">
        <asp:TextBox ID="txtPROO" runat="server" BorderStyle="None" AutoPostBack="true" ForeColor="White" Height="1"></asp:TextBox>
        <asp:TextBox ID="txtIDCHEQUERA" runat="server" BorderStyle="None" AutoPostBack="true" ForeColor="White" Height="1"></asp:TextBox>
        <asp:TextBox ID="txtID_CUENTA" runat="server" BorderStyle="None" ForeColor="White" Height="1" AutoPostBack="true" OnTextChanged="txtID_CUENTA_TextChanged"></asp:TextBox>
    </div>


    <div class="container-fluid col-lg-12 col-md-12" style="font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif; top: 0px;">
        <div class="form-horizontal" style="position: relative; margin-top: 0px;">

            <div class="container col-lg-12 col-md-12" style="color: black; border-radius: 5px 5px 5px 5px; background-color: white; font-size: 12px; margin-top: -20px;">
                <div class="form-inline">
                    <div class="input-group col-md-5" style="height: 34px; z-index: 1; float: left; margin-left: -20px;">

                        <asp:TextBox runat="server" ID="TXTCUENTA2" CssClass="form-control" placeholder="Busqueda de cuentas" Style="text-transform: uppercase"></asp:TextBox>

                        <span class="input-group-btn">
                            <asp:UpdatePanel ID="UpdatePanel5" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>

                                    <asp:Button runat="server" ID="BTNCHEQUERA" CssClass="btn btn-info" Text="..." Height="34" ToolTip="Seleccionar Chequera" OnClick="BTNCHEQUERA_Click" />

                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="BTNCHEQUERA" EventName="Click" />

                                </Triggers>
                            </asp:UpdatePanel>
                        </span>
                    </div>
                    <div class="col-md-7 right">
                        <asp:UpdatePanel ID="UpdatePanel7" runat="server" UpdateMode="Always">
                            <ContentTemplate>
                                <div class="col-xs-6 col-md-4" style="left: -30px; margin-top: 10px;">
                                    <asp:Label runat="server" ID="LBLBANCOCTA" CssClass="label" Text="-" Font-Size="Small" BackColor="Black"></asp:Label>
                                </div>
                                <div class="form-group col-xs-6 col-md-4" style="left: 10px;">
                                    <label class="control-label col-md-2" style="color: black; left: -10px;">S.CONT:</label>
                                    <div class="col-xs-6 col-md-2" style="margin-top: 10px; left: 10px;">
                                        <asp:Label runat="server" ID="LBLSALDOCONT" CssClass="label" Text="-" Font-Size="Small" BackColor="Black"></asp:Label>

                                    </div>

                                </div>

                                <div class="form-group col-xs-6 col-md-4" style="left: 20px;">

                                    <label class="control-label col-md-2" style="color: black; left: -5px;">S.DISP:</label>

                                    <div class="col-xs-6 col-md-2" style="margin-top: 10px; left: 10px;">
                                        <asp:Label runat="server" ID="LBLSALDODIP" CssClass="label" Text="-" Font-Size="Small" BackColor="Black"></asp:Label>
                                    </div>
                                </div>
                            </ContentTemplate>

                        </asp:UpdatePanel>
                    </div>



                </div>







            </div>

            <div>&nbsp;</div>

            <div style="font-size: 11px;">
                <hr />
                <asp:UpdatePanel ID="upd3" runat="server">
                    <ContentTemplate>

                        <div class="form-horizontal col-xs-12 col-md-3 col-lg-3" style="position: relative; background-color: white;">
                            <div class="form-group">

                                <div id="provee" class="input-group" style="height: 35px; z-index: 1;">

                                    <asp:TextBox runat="server" ID="TXTPROOVEEDOR" CssClass="form-control" placeholder="Busqueda de proveedores" MaxLength="999"></asp:TextBox>
                                    <span class="input-group-btn">
                                        <asp:Button ID="BTNNUEVOPROVEE" runat="server" CssClass="btn btn-warning" ForeColor="Black" Text="..." Height="34" OnClick="BTNNUEVOPROVEE_Click" />
                                    </span>
                                </div>

                                <div id="test" class="list fade-out" style="text-align: center;">
                                    <asp:Label runat="server" ID="lblmensajeproveedor" ForeColor="Red" Text="(*)Debe seleccionar el Proveedor" Visible="false"></asp:Label>


                                </div>


                            </div>

                            <div class="form-group">
                                <label class="control-label col-md-3" style="color: black">F. GIRO:</label>
                                <div class="col-xs-12 col-md-9 col-sm-9">
                                    <asp:TextBox runat="server" ID="TXTFGIRO" CssClass="form-control" type="date" MaxLength="123" Height="35px"></asp:TextBox>
                                    <%--VALIDADOR--%>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server"
                                        ControlToValidate="TXTFGIRO"
                                        ErrorMessage="(*)La fecha de giro es necesaria"
                                        ForeColor="Red"
                                        ValidationGroup="Registro" Display="Dynamic">
                                    </asp:RequiredFieldValidator>

                                </div>

                            </div>

                            <div class="form-group">
                                <label class="control-label col-md-3" style="color: black">F.COBRO:</label>
                                <div class="col-xs-12 col-md-9 col-sm-9">
                                    <asp:TextBox runat="server" ID="TXTFCOBRO" CssClass="form-control" type="date" MaxLength="123" Height="35px"></asp:TextBox>
                                    <%--VALIDADOR--%>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server"
                                        ControlToValidate="TXTFCOBRO"
                                        ErrorMessage="(*)La fecha de cobro es necesaria"
                                        ForeColor="Red"
                                        ValidationGroup="Registro" Display="Dynamic">
                                    </asp:RequiredFieldValidator>

                                </div>

                            </div>
                        </div>
                        <!-- PRIMER GRUPO -->
                        <div class="form-horizontal col-xs-12 col-md-3 col-lg-3" style="position: relative; background-color: white;">
                            <div class="form-group">
                                <label class="control-label col-md-3" style="color: black">N° CHEQ:</label>
                                <div class="col-xs-12 col-md-9 col-sm-9">
                                    <asp:TextBox runat="server" ID="TXTNUMERO" CssClass="form-control" placeholder="Ingrese número de cheque" MaxLength="100" onkeydown="return jsDecimals(event);"></asp:TextBox>
                                    <%--VALIDADOR--%>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server"
                                        ControlToValidate="TXTNUMERO"
                                        ErrorMessage="(*)El Número de cheque es requerido"
                                        ForeColor="Red"
                                        ValidationGroup="Registro" Display="Dynamic">
                                    </asp:RequiredFieldValidator>

                                </div>
                            </div>

                            <div class="form-group">
                                <label class="control-label col-md-3" style="color: black">IMPORTE:</label>
                                <div class="col-xs-12 col-md-9 col-sm-9">
                                    <asp:TextBox runat="server" ID="TXTIMPORTE" CssClass="form-control" placeholder="Ingrese importe" MaxLength="100" onkeydown="return jsDecimals(event);" OnBlur="addCommas(this)"></asp:TextBox>
                                    <%--VALIDADOR--%>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server"
                                        ControlToValidate="TXTIMPORTE"
                                        ErrorMessage="(*)El Importe es requerido"
                                        ForeColor="Red"
                                        ValidationGroup="Registro" Display="Dynamic">
                                    </asp:RequiredFieldValidator>

                                </div>
                            </div>




                            <div class="form-group">
                                <label class="control-label col-md-3" style="color: black">OBSERV:</label>

                                <div class="col-xs-12 col-md-9" style="height: 35px; z-index: 1;">
                                    <asp:TextBox runat="server" ID="TXTOBS" CssClass="form-control" placeholder="Ingrese observación" MaxLength="500" Enabled="False"></asp:TextBox>
                                    <%--<span class="input-group-btn">
                                        <asp:ImageButton ID="BTNAGREGARDOCVTA" runat="server" CssClass="btn btn-danger" ForeColor="Black" ImageUrl="~/ICONOS/invoice (1).png" Height="34" OnClick="BTNAGREGARDOCVTA_Click" />
                                    </span>--%>
                                </div>
                            </div>
                        </div>
                        <div class="form-horizontal col-xs-12 col-md-4 col-lg-4" style="position: relative; background-color: white;">
                            <div class="form-group">
                                <div class="col-xs-12 col-md-12" style="margin-left: 0px; background-color: #f1f1f1;">
                                    <asp:UpdatePanel ID="UpdatePanel13" runat="server">
                                        <ContentTemplate>

                                            <div class="form-inline col-md-12" style="float: inherit">

                                                <asp:DropDownList runat="server" ID="cboFVBV" CssClass="form-control col-md-2" Width="68" Height="34" AutoPostBack="true" OnSelectedIndexChanged="cboFVBV_SelectedIndexChanged">
                                                    <asp:ListItem Value="">--</asp:ListItem>
                                                    <asp:ListItem Value="FT">FT</asp:ListItem>
                                                    <asp:ListItem Value="BV">BV</asp:ListItem>
                                                </asp:DropDownList>

                                                <asp:TextBox runat="server" ID="txtserie" CssClass="form-control col-md-2" AutoPostBack="true" MaxLength="3" Width="60" onkeydown="return jsDecimals(event);" placeholder="SERIE" OnTextChanged="txtserie_TextChanged"></asp:TextBox>

                                                <asp:TextBox runat="server" ID="txtnumero1" AutoPostBack="true" CssClass="form-control col-md-2" Width="70" MaxLength="5" onkeydown="return jsDecimals(event);" placeholder="NUM" OnTextChanged="txtnumero1_TextChanged"></asp:TextBox>
                                                <asp:TextBox runat="server" ID="txtnumero2" AutoPostBack="true" Visible="false" CssClass="form-control col-md-2" Width="70" Height="32" MaxLength="5" onkeydown="return jsDecimals(event);" placeholder="FINAL" OnTextChanged="txtnumero2_TextChanged"></asp:TextBox>
                                                <div class="form-group">
                                                    <asp:Button runat="server" CssClass="btn btn-warning col-md-6" Height="35" ID="btnagregar" Width="48" Font-Bold="true" Text="OK" OnClick="btnagregar_Click" />
                                                </div>


                                                <asp:CheckBox ID="chkMULTIPLE" CssClass="col-md-2" runat="server" AutoPostBack="true" Text="Rango" Font-Size="Smaller" ToolTip="Chequear si desea registrar un rango de BV" OnCheckedChanged="chkMULTIPLE_CheckedChanged" />
                                            </div>

                                            <div style="width: 100%; height: 100px; overflow-y: scroll;">
                                                <asp:GridView ID="dgvDATOS2" runat="server" AutoGenerateColumns="False"
                                                    Font-Size="Smaller" AutoGenerateEditButton="false" CssClass="table table-responsive table-condensed table-bordered" BackColor="White" EmptyDataText="No existen datos" OnRowCommand="dgvDATOS2_RowCommand">
                                                    <Columns>

                                                        <asp:BoundField HeaderText="DOC" DataField="DOC" HeaderStyle-Font-Size="Small" HeaderStyle-BackColor="#0066cc" HeaderStyle-ForeColor="White">
                                                            <HeaderStyle Font-Size="Smaller" />
                                                        </asp:BoundField>
                                                        <asp:BoundField HeaderText="SERIE" DataField="SERIE" HeaderStyle-Font-Size="Small" HeaderStyle-BackColor="#0066cc" HeaderStyle-ForeColor="White">
                                                            <HeaderStyle Font-Size="Smaller" />
                                                        </asp:BoundField>
                                                        <asp:BoundField HeaderText="NUMERO" DataField="NUMERO" HeaderStyle-Font-Size="Small" HeaderStyle-BackColor="#0066cc" HeaderStyle-ForeColor="White">
                                                            <HeaderStyle Font-Size="Smaller" />
                                                        </asp:BoundField>
                                                        <asp:TemplateField HeaderStyle-BackColor="#0066cc" HeaderStyle-ForeColor="White" ControlStyle-Width="50">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="Buttoneliminar" runat="server" CommandName="ELIM" CssClass="btn btn-danger fa fa-trash-o" Text="" Font-Bold="true" Height="25" Font-Size="Small" />
                                                            </ItemTemplate>
                                                            <HeaderStyle BackColor="#0066CC" ForeColor="White" />
                                                        </asp:TemplateField>

                                                    </Columns>
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <SelectedRowStyle BackColor="#66CCFF" />
                                                </asp:GridView>
                                            </div>

                                        </ContentTemplate>
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="btnagregar" EventName="Click" />

                                            <asp:AsyncPostBackTrigger ControlID="dgvDATOS" EventName="RowCommand" />
                                            <asp:AsyncPostBackTrigger ControlID="chkMULTIPLE" EventName="CheckedChanged" />
                                            <asp:AsyncPostBackTrigger ControlID="cboFVBV" EventName="SelectedIndexChanged" />
                                            <asp:AsyncPostBackTrigger ControlID="txtserie" EventName="TextChanged" />
                                            <asp:AsyncPostBackTrigger ControlID="txtnumero" EventName="TextChanged" />
                                        </Triggers>
                                    </asp:UpdatePanel>
                                </div>

                            </div>
                        </div>
                        <!-- SEGUNDO GRUPO -->

                        <div class="form-horizontal col-xs-12 col-md-2 col-lg-2" style="position: relative; background-color: white;">
                            <div class="form-group">
                                <asp:Button runat="server" Height="0" Width="0" ID="Button2" CssClass="visible-xs" AccessKey="N" OnClick="Button2_Click" />

                                <div class="col-md-12 col-sm-12">
                                    <asp:Button runat="server" CssClass="btn btn-info col-xs-12" ForeColor="White" Font-Bold="true" Text="GRABAR" ValidationGroup="Registro" ID="btnRegistrar" AccessKey="G" OnClick="btnRegistrar_Click" />

                                </div>
                                <div>&nbsp;</div>
                                <div class="col-md-12 col-sm-12">
                                    <asp:Button runat="server" CssClass="btn btn-info col-xs-12" Text="CANCELAR" Font-Bold="true" ForeColor="White" ID="btnCancelar" AccessKey="C" OnClick="btnCancelar_Click" />

                                </div>

                            </div>
                        </div>
                    </ContentTemplate>
                    <Triggers>
                        <asp:PostBackTrigger ControlID="btnRegistrar" />
                    </Triggers>
                </asp:UpdatePanel>
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
    <!-- -----------------------------Modal Registro-------------------------------------------------------------->
    <div class="modal fade" id="myModalFECHA" tabindex="-1" role="dialog" aria-labelledby="myModalLabel">
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                    <img src="ICONOS/LOGO_GRUPO_DIONYS.png" width="75" height="35" />
                    <h3 class="modal-title bg-color-green" id="myModalFECHALabel" style="text-align: center">"GRUPO DIONYS"</h3>

                </div>
                <div class="modal-body">
                    <h3 class="danger" style="text-align: center; font-family: 'Segoe UI';">&nbsp La fecha de giro no puede ser mayor a la fecha de cobro</h3>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-danger" data-dismiss="modal">Aceptar</button>
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
                    <h3 class="warning" style="text-align: center; font-family: 'Segoe UI'">&nbsp Cheque eliminado correctamente</h3>
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
    <!-- -----------------------------Modal No valido-------------------------------------------------------------->
    <div class="modal fade" id="myModal9" tabindex="-1" role="dialog" aria-labelledby="myModalLabel">
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                    <img src="ICONOS/LOGO_GRUPO_DIONYS.png" width="80" height="35" />
                    <h3 class="modal-title bg-color-red" id="myModalLabel9" style="text-align: center">"GRUPO DIONYS"</h3>

                </div>
                <div class="modal-body">
                    <h3 class="danger" style="text-align: center; font-family: 'Segoe UI'">&nbsp El número de CHEQUE ya se ha usado</h3>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-danger" data-dismiss="modal">Aceptar</button>
                </div>
            </div>
        </div>
    </div>
    <!-- -----------------------------Modal RegistroError-------------------------------------------------------------->
    <!-- -----------------------------Modal No valido-------------------------------------------------------------->
    <div class="modal fade" id="myModal01" tabindex="-1" role="dialog" aria-labelledby="myModalLabel">
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                    <img src="ICONOS/LOGO_GRUPO_DIONYS.png" width="80" height="35" />
                    <h3 class="modal-title bg-color-red" id="myModalLabel01" style="text-align: center">"GRUPO DIONYS"</h3>

                </div>
                <div class="modal-body">
                    <h3 class="danger" style="text-align: center; font-family: 'Segoe UI'">&nbsp El número de CHEQUE no es correcto</h3>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-danger" data-dismiss="modal">Aceptar</button>
                </div>
            </div>
        </div>
    </div>
    <!-- -----------------------------Modal RegistroError-------------------------------------------------------------->

    <asp:UpdatePanel ID="UPD2" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <!-- -----------------------------Modal FORMULARIO-------------------------------------------------------------->
            <div style="font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif">
                <asp:UpdatePanel ID="udpOutterUpdatePanel" runat="server" UpdateMode="Always">
                    <ContentTemplate>
                        <input id="Hid_Sno" type="hidden" name="hddclick" runat="server" />
                        <!-- ModalPopupExtender -->
                        <cc1:ModalPopupExtender ID="mp1" runat="server" PopupControlID="Panl1" TargetControlID="Hid_Sno" BackgroundCssClass="Background"
                            BehaviorID="mp1">
                        </cc1:ModalPopupExtender>
                        <asp:Panel ID="Panl1" runat="server" CssClass="Popup" align="center" Style="display: none">

                            <div class="container col-lg-12 col-md-12 col-xs-12" style="text-align: center">
                                <h2 style="font-size: large; color: red;">CHEQUERAS DE LA CUENTA: </h2>
                                &nbsp
                            </div>

                            <asp:UpdatePanel ID="udpInnerUpdatePanel" runat="Server" UpdateMode="Conditional">
                                <ContentTemplate>



                                    <div class="col-sm-12">
                                        <div class="col-sm-6 col-xs-10">
                                            <asp:Label runat="server" CssClass="success">CHEQUERAS CLASICAS</asp:Label>
                                            <asp:GridView ID="dgvCHEQUERA_CLAS" runat="server" AutoGenerateColumns="true" Font-Size="Small" CssClass="table table-bordered table-condensed table-responsive"
                                                OnRowCommand="dgvCHEQUERA_CLAS_RowCommand">
                                                <EmptyDataTemplate>
                                                    No hay chequeras de este tipo
                                                </EmptyDataTemplate>
                                                <Columns>
                                                    <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:ImageButton ID="LinkButtonAGREGAR" TabIndex="4" runat="server" CommandName="AGREGAR" Font-Size="Smaller" ImageUrl="~/ICONOS/AUMENTAR.png" Width="19" Height="19"></asp:ImageButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>

                                        </div>
                                        <div class="col-sm-6 col-xs-10">
                                            <asp:Label runat="server" CssClass="success">CHEQUERAS PAGO DIFERIDO</asp:Label>
                                            <asp:GridView ID="dgvCHEQUERA_DIFE" runat="server" AutoGenerateColumns="true" Font-Size="Small" CssClass="table table-bordered table-condensed table-responsive"
                                                OnRowCommand="dgvCHEQUERA_DIFE_RowCommand">
                                                <EmptyDataTemplate>
                                                    No hay chequeras de este tipo
                                                </EmptyDataTemplate>
                                                <Columns>
                                                    <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:ImageButton ID="LinkButtonAGREGAR" TabIndex="4" runat="server" CommandName="AGREGAR" Font-Size="Smaller" ImageUrl="~/ICONOS/AUMENTAR.png" Width="19" Height="19"></asp:ImageButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>

                                        </div>
                                    </div>

                                    <div class="col-sm-12 col-xs-12">
                                        <hr />
                                        <asp:Label runat="server" CssClass="success">CHEQUERA SELECCIONADA</asp:Label>
                                        <asp:GridView ID="dgvDATOS" runat="server" AutoGenerateColumns="true" Font-Size="Small" CssClass="table table-bordered table-condensed table-responsive"
                                            OnRowCommand="dgvDATOS_RowCommand" HeaderStyle-BackColor="#3366ff" HeaderStyle-ForeColor="White">
                                            <EmptyDataTemplate>
                                                No ha agregado ninguna chequera
                                            </EmptyDataTemplate>
                                            <Columns>

                                                <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="LinkButtonQUITAR" TabIndex="4" runat="server" CommandName="ELIM" Font-Size="Smaller" ImageUrl="~/ICONOS/ELIMINAR.png" Width="19" Height="19"></asp:ImageButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>



                                    </div>
                                    <div class="container col-lg-12 col-md-12">


                                        <div class="form-group col-lg-12 col-md-12 col-xs-12">

                                            <div class="col-lg-6 col-md-6 col-xs-12">
                                                <asp:Button ID="btnAceptarPopUp" runat="server" Text="ACEPTAR" Width="120px" CssClass="form-control btn btn-info" OnClick="btnAceptarPopUp_Click" />
                                            </div>
                                            <div class="col-lg-6 col-md-6 col-xs-12">
                                                <asp:Button ID="btnSalirPopUp" runat="server" Text="SALIR" Width="120px" CssClass="form-control btn btn-info" OnClick="btnSalirPopUp_Click" />
                                            </div>
                                        </div>

                                    </div>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="btnSalirPopUp" EventName="Click" />
                                    <asp:AsyncPostBackTrigger ControlID="dgvCHEQUERA_CLAS" EventName="RowCommand" />
                                    <asp:AsyncPostBackTrigger ControlID="dgvCHEQUERA_DIFE" EventName="RowCommand" />
                                    <asp:AsyncPostBackTrigger ControlID="dgvDATOS" EventName="RowCommand" />


                                    <%--VERIFICAR SI SALE EVENTO Y RECARGA EL POPUP--%>
                                </Triggers>
                            </asp:UpdatePanel>
                        </asp:Panel>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
            <%--POPUP AJAX--%>
            <!-- -----------------------------Modal RegistroError-------------------------------------------------------------->

        </ContentTemplate>
        <Triggers>
        </Triggers>
    </asp:UpdatePanel>

    <!--------------------------------- POPUP ACTUALIZAR ESTADO DE CHEUQE Y GENERAR MOVIMIENTO-------------------------------->

    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <!-- -----------------------------Modal FORMULARIO---------------------------------------------------------------->
            <div style="font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif">
                <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Always">
                    <ContentTemplate>
                        <input id="Hidden1" type="hidden" name="hddclick" runat="server" />
                        <!-- ModalPopupExtender -->
                        <cc1:ModalPopupExtender ID="mp2" runat="server" PopupControlID="Panel1" TargetControlID="Hidden1" BackgroundCssClass="Background"
                            BehaviorID="mp2">
                        </cc1:ModalPopupExtender>
                        <asp:Panel ID="Panel1" runat="server" CssClass="Popup2" align="center" Style="display: none">

                            <div class="container col-lg-12 col-md-12 col-xs-12" style="text-align: center">
                                <h2 style="font-size: large; color: red;">AMARRAR MOVIMIENTOS</h2>
                                &nbsp
                            </div>

                            <asp:UpdatePanel ID="UpdatePanel3" runat="Server" UpdateMode="Conditional">
                                <ContentTemplate>

                                    <div class="col-sm-12 col-xs-10">


                                        <asp:Label runat="server" CssClass="success">MOVIMIENTO VINCULADO</asp:Label>
                                        <asp:GridView ID="dgvMOV_CHE" runat="server" AutoGenerateColumns="true" Font-Size="Small" CssClass="table table-bordered table-condensed table-responsive"
                                            OnRowCommand="dgvMOV_CHE_RowCommand" SelectedRowStyle-BackColor="SkyBlue">
                                            <EmptyDataTemplate>
                                                Aun no existen movimientos asociados
                                            </EmptyDataTemplate>
                                            <Columns>
                                                <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="QUITAR2" TabIndex="4" runat="server" CommandName="QUITAR" Font-Size="Smaller" ImageUrl="~/ICONOS/ELIMINAR.png" Width="19" Height="19"></asp:ImageButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>


                                    </div>


                                    <div class="col-sm-12 col-xs-12">
                                        <hr />
                                        <asp:Label runat="server" CssClass="success">MOVIMIENTOS</asp:Label>
                                        <asp:GridView ID="dgvMOVIMIENTOS" runat="server" AutoGenerateColumns="true" Font-Size="Small" CssClass="table table-bordered table-condensed table-responsive"
                                            OnRowCommand="dgvMOVIMIENTOS_RowCommand" HeaderStyle-BackColor="#3366ff" HeaderStyle-ForeColor="White">
                                            <EmptyDataTemplate>
                                                Aun no existen movimientos asociados
                                            </EmptyDataTemplate>
                                            <Columns>

                                                <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="AGREGAR2" TabIndex="4" runat="server" CommandName="AGREGAR" Font-Size="Smaller" ImageUrl="~/ICONOS/AUMENTAR.png" Width="19" Height="19"></asp:ImageButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>



                                    </div>
                                    <div class="container col-lg-12 col-md-12">


                                        <div class="form-group col-lg-12 col-md-12 col-xs-12 center-block">


                                            <asp:Button ID="btnGRABARMOV" runat="server" Text="GRABAR" Width="120px" CssClass="form-control btn btn-info" OnClick="btnGRABARMOV_Click" />
                                            <asp:Button ID="btnCANCELARMOV" runat="server" Text="SALIR" Width="120px" CssClass="form-control btn btn-info " OnClick="btnCANCELARMOV_Click" />

                                        </div>

                                    </div>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="btnGRABARMOV" EventName="Click" />
                                    <asp:AsyncPostBackTrigger ControlID="dgvMOV_CHE" EventName="RowCommand" />
                                    <asp:AsyncPostBackTrigger ControlID="dgvMOVIMIENTOS" EventName="RowCommand" />


                                </Triggers>
                            </asp:UpdatePanel>
                        </asp:Panel>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>


        </ContentTemplate>
        <Triggers>
        </Triggers>
    </asp:UpdatePanel>
    <!--POPUP AJAX-->
    <!---------------------------------------------------/////////////////////////////////////////////////////////////////////----- -->
    <!--------------------------------- POPUP REGISTRAR PROVEEDOR-------------------------------->

    <asp:UpdatePanel ID="UpdatePanel4" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <!-- -----------------------------Modal FORMULARIO---------------------------------------------------------------->
            <div style="font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif">
                <asp:UpdatePanel ID="UpdatePanel6" runat="server" UpdateMode="Always">
                    <ContentTemplate>
                        <input id="Hidden2" type="hidden" name="hddclick" runat="server" />
                        <!-- ModalPopupExtender -->
                        <cc1:ModalPopupExtender ID="mp3" runat="server" PopupControlID="Panel2" TargetControlID="Hidden2" BackgroundCssClass="Background2"
                            BehaviorID="mp3">
                        </cc1:ModalPopupExtender>
                        <asp:Panel ID="Panel2" runat="server" CssClass="Popup7" align="center" Style="display: none; 
            background-image: linear-gradient( 135deg, #90F7EC 0%, #32CCBC 100%); ">

                            <div class="container col-lg-12 col-md-12 col-xs-12" style="text-align: center; ">
                                <h2  style="font-size: large; color: orangered;">REGISTRAR PROVEEDOR</h2>
                                &nbsp
                                <table border="0"> 
                                    <tr style="height: 40px">
                                        <td style="padding-right:10px;">
                                            <asp:Label runat="server" CssClass="lbl" Text="TIPO PROV:"></asp:Label>
                                        </td>
                                        <td style="padding-left:10px;">
                                            <asp:DropDownList runat="server" ID="CBOTIPOPROV" CssClass="form-control" AutoPostBack="true" Width="220" OnSelectedIndexChanged="CBOTIPOPROV_SelectedIndexChanged">
                                                <asp:ListItem Text="-ELEGIR-" Value="OTRO" />
                                                <asp:ListItem Text="P. NATURAL" Value="PN" />
                                                <asp:ListItem Text="P.JURÍDICA" Value="PJ" />
                                            </asp:DropDownList>

                                        </td>
                                        <td style="padding-left:10px; padding-right:10px;">
                                            <asp:Label ID="Label5" runat="server" CssClass="lbl" Text="ORIGEN:"></asp:Label>
                                        </td>
                                        <td style="padding-left:20px;">
                                            <asp:DropDownList runat="server" ID="CBONACIOPROV" CssClass="form-control" AutoPostBack="false" Width="220">
                                                <asp:ListItem Text="-ELEGIR-" Value="OTRO" />
                                                <asp:ListItem Text="P. NACIONAL" Value="PN" />
                                                <asp:ListItem Text="P. EXTRANJERA" Value="PE" />
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    </table>
                                <table> 
                                    <tr style="height: 40px">
                                        <td>
                                            <asp:Label ID="Label6" runat="server" CssClass="lbl" Text="DESCRIP:"></asp:Label>
                                        </td>
                                        <td style="padding-left:18px;">
                                            <asp:TextBox ID="TXTDESCRIP_PROV" runat="server" Font-Size="14px" CssClass="form-control" Width="480px" Height="35"></asp:TextBox>
                                        </td>
                                        
                                      
                                    </tr>
                                    <tr style="height: 40px;">
                                        <td>
                                            <asp:Label ID="Label1" runat="server" CssClass="lbl" Text="DIRECCION:"></asp:Label>
                                        </td>
                                        <td style="padding-left:18px;">
                                            <asp:TextBox ID="TXTDIR_PROV" runat="server" Font-Size="14px" CssClass="form-control" Width="480px"></asp:TextBox>
                                        </td>
                                        
                                    </tr>
                                    </table>
                                <table>
                                   
                                    <tr style="height: 40px">
                                        <td style="padding-right:0px; ">
                                            <asp:UpdatePanel runat="server" ID="UPDLBL" UpdateMode="Conditional">
                                                <ContentTemplate>
                                                    <asp:Label ID="LBLRUC_DNI" runat="server" CssClass="lbl" Text="">N° DOC(*):</asp:Label>
                                                    </ContentTemplate>
                                            </asp:UpdatePanel>
                                             
                                        </td>
                                        <td style="padding-left:25px;">
                                            <asp:UpdatePanel runat="server" ID="UpdatePanel8" UpdateMode="Conditional">
                                                <ContentTemplate>
                                                    <asp:TextBox ID="TXTRUC_DNI_PROV" runat="server" Font-Size="14px" CssClass="form-control" Width="220" Height="35"></asp:TextBox>
                                                    </ContentTemplate>
                                            </asp:UpdatePanel>
                                            
                                        </td>
                                        <td style="padding-left:10px;">
                                            <asp:Label ID="Label2" runat="server" CssClass="lbl" Text="TELEFONO:"></asp:Label>
                                        </td>
                                        <td style="padding-left:20px;">
                                            <asp:TextBox ID="TXTTELEFONOPROV" runat="server" Font-Size="14px" Style="text-transform: uppercase" CssClass="form-control" ></asp:TextBox>
                                        </td>
                                    </tr>
                                     <tr style="height: 40px">
                                        <td style="padding-right:0px;" >
                                            <asp:Label ID="Label3" runat="server" CssClass="lbl" Text="MOVIL:"></asp:Label>
                                        </td>
                                        <td style="padding-left:25px;">
                                            <asp:TextBox ID="TVTMOVILPRO" runat="server" Font-Size="14px" CssClass="form-control" Width="220"></asp:TextBox>
                                        </td>
                                        <td style="padding-left:10px;">
                                            <asp:Label ID="Label4" runat="server" CssClass="lbl" Text="F. NACI:"></asp:Label>
                                        </td>
                                        <td style="padding-left:20px;">
                                            <asp:TextBox ID="TXTFECHAPROV" TextMode="Date" runat="server" Font-Size="14px" Style="text-transform: uppercase" Height="35" CssClass="form-control" Width="220"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr style="height: 40px">
                                        <td style="padding-right:0px;" >
                                            <asp:Label ID="Label7" runat="server" CssClass="lbl" Text="EMAIL:"></asp:Label>
                                        </td>
                                        <td style="padding-left:25px;">
                                            <asp:TextBox ID="TextBox1" runat="server" Font-Size="14px" CssClass="form-control" Width="220"></asp:TextBox>
                                        </td>
                                        <td style="padding-left:10px;">
                                            <asp:Label ID="Label8" runat="server" CssClass="lbl" Text="WEBSITE:"></asp:Label>
                                        </td>
                                        <td style="padding-left:20px;">
                                            <asp:TextBox ID="TextBox2" TextMode="Date" runat="server" Font-Size="14px" Style="text-transform: uppercase" Height="35" CssClass="form-control" Width="220"></asp:TextBox>
                                        </td>
                                    </tr>
                                    </table>
                                <table>
                                    <tr style="height: 40px">
                                        <td style="padding-right:0px;" >
                                            <asp:DropDownList runat="server" ID="DropDownList1" CssClass="form-control" AutoPostBack="false" Width="100">
                                                <asp:ListItem Text="-PAIS-" Value="OTRO" />
                                            </asp:DropDownList>
                                        </td>
                                        <td style="padding-left:25px;">
                                            <asp:DropDownList runat="server" ID="DropDownList2" CssClass="form-control" AutoPostBack="false" Width="150">
                                                <asp:ListItem Text="-REGION-" Value="OTRO" />
                                            </asp:DropDownList>
                                        </td>
                                        
                                        <td style="padding-left:25px;">
                                            <asp:DropDownList runat="server" ID="DropDownList3" CssClass="form-control" AutoPostBack="false" Width="180">
                                                <asp:ListItem Text="-PROVINCIA-" Value="OTRO" />
                                            </asp:DropDownList>
                                        </td>
                                        
                                        <td style="padding-left:25px;">
                                           <asp:DropDownList runat="server" ID="DropDownList4" CssClass="form-control" AutoPostBack="false" Width="150">
                                                <asp:ListItem Text="-DISTRITO-" Value="OTRO" />
                                            </asp:DropDownList>
                                        </td>
                                    </tr>

                                    <asp:Label ID="lblid_cliente" runat="server" CssClass="visible-xs" Text="DESCRIPCIÓN:"></asp:Label>
                                    <asp:Label ID="lblid_cheque" runat="server" CssClass="visible-xs" Text="DESCRIPCIÓN:"></asp:Label>
                                    <asp:Label ID="LBLID_MOV" runat="server" CssClass="visible-xs" Text="DESCRIPCIÓN:"></asp:Label>

                                </table>
                                <div>&nbsp;</div>
                                <div class="container col-lg-12 col-md-12">


                                        <div class="form-group col-lg-12 col-md-12 col-xs-12 center-block">


                                            <asp:Button ID="BTNGRABARPROV" runat="server" Text="GRABAR" Width="120px" CssClass="form-control btn btn-info"  OnClick="BTNGRABARPROV_Click" />
                                            <asp:Button ID="BTNCANCELARPROV" runat="server" Text="CANCELAR" Width="120px" CssClass="form-control btn btn-danger" OnClick="BTNCANCELARPROV_Click"/>

                                        </div>

                                    </div>
                            </div>


                        </asp:Panel>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="CBOTIPOPROV" EventName="SelectedIndexChanged"/>
                        <asp:AsyncPostBackTrigger ControlID="BTNGRABARPROV"  EventName="Click"/>
                        <asp:AsyncPostBackTrigger ControlID="BTNCANCELARPROV" EventName="Click"/>
                    </Triggers>
                </asp:UpdatePanel>
            </div>


        </ContentTemplate>
        <Triggers>
        </Triggers>
    </asp:UpdatePanel>
    <!--POPUP AJAX-->
    <!--------------------------------------------------------///////////////////////////////////////////////////////////////////////////////////// -->


    <asp:UpdatePanel runat="server" ID="UPDGRILLA">
        <ContentTemplate>

            <div class="container-fluid col-lg-12 col-md-12" style="font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif; background-color: #F5F3EE; top: 0px;">

                <div class="form-inline">
                    <div class="form-group">
                        <asp:TextBox runat="server" ID="LBLCHEQUERA2" BorderStyle="None" Width="180" BackColor="#F5F3EE" Height="25" AutoPostBack="true" Font-Size="Smaller" ForeColor="Green" />
                    </div>
                    <div class="form-group">
                        <asp:TextBox runat="server" ID="LBLRANGOINI" BorderStyle="None" BackColor="#F5F3EE" Height="25" AutoPostBack="true" Font-Size="Smaller" ForeColor="Green" />
                    </div>
                    <div class="form-group">
                        <asp:TextBox runat="server" ID="LBLRANGOFIN" BorderStyle="None" BackColor="#F5F3EE" Height="25" AutoPostBack="true" Font-Size="Smaller" ForeColor="Green" />
                    </div>
                    <div class="form-group">
                        <asp:TextBox runat="server" ID="txtBUSQUEDA" CssClass="form-control" placeholder="Busqueda de cheques" Width="230"></asp:TextBox>

                    </div>
                    <div class="form-group">
                        <asp:Button ID="btnMOSTRARTODOS" runat="server" CssClass="btn btn-danger" Width="80" Text="BUSCAR" Height="35" OnClick="btnMOSTRARTODOS_Click" />
                    </div>
                </div>

                <div style="overflow-y: scroll;">
                    <asp:GridView ID="dgvBANCOS" runat="server" EnablePersistedSelection="true" OnSelectedIndexChanging="dgvBANCOS_SelectedIndexChanging" BackColor="White" Font-Names="Tahoma" AutoGenerateColumns="False" DataKeyNames="ID_CHEQUESEM" Font-Size="Small" CssClass="table table-bordered table-condensed table-responsive"
                        HeaderStyle-BackColor="#3366ff" HeaderStyle-ForeColor="White" OnSelectedIndexChanged="dgvBANCOS_SelectedIndexChanged" OnRowDataBound="dgvBANCOS_RowDataBound" OnRowCommand="dgvBANCOS_RowCommand" OnRowCreated="dgvBANCOS_RowCreated">
                        <EmptyDataTemplate>
                            No hay registros!
                        </EmptyDataTemplate>
                        <Columns>

                            <asp:BoundField DataField="ID_CHEQUESEM" HeaderText="COD" HeaderStyle-Font-Size="Smaller">
                                <ItemStyle Width="30px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="PROVEEDOR" HeaderText="PROVEEDOR" HeaderStyle-Font-Size="Smaller">
                                <ItemStyle Width="600px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="F_GIRO" HeaderText="F. GIRO" HeaderStyle-Font-Size="Smaller">
                                <ItemStyle Width="90px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="F_COBRO" HeaderText="F. COBRO" HeaderStyle-Font-Size="Smaller">
                                <ItemStyle Width="90px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="F_COBRADO" HeaderText="F. COBRADO" HeaderStyle-Font-Size="Smaller" ConvertEmptyStringToNull="true">
                                <ItemStyle Width="90px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="N_CHEQUE" HeaderText="N° CHQ" HeaderStyle-Font-Size="Smaller" ItemStyle-Width="70px" />
                            <asp:BoundField DataField="IMPORTE" HeaderText="IMPORTE" DataFormatString="{0:N}" HeaderStyle-Font-Size="Smaller" ItemStyle-HorizontalAlign="Right" />
                            <asp:BoundField DataField="ESTADO" HeaderText="ESTADO" HeaderStyle-Font-Size="Smaller" />

                            <asp:BoundField DataField="ID_MOV" HeaderText="ID MOV" HeaderStyle-Font-Size="Smaller" ItemStyle-Width="70px" ConvertEmptyStringToNull="true" />
                            <asp:BoundField DataField="OBSERVACION" HeaderText="OBSERVACION" HeaderStyle-Font-Size="Smaller" ConvertEmptyStringToNull="true" />
                            <asp:TemplateField ItemStyle-HorizontalAlign="Center" ItemStyle-Width="50px">
                                <ItemTemplate>
                                    <asp:ImageButton ID="ACTUALIZAR" runat="server" CommandName="ACTUALIZAR" OnClientClick="return GetSelectedRow(this)" ImageAlign="Middle"
                                        ImageUrl="~/ICONOS/refresh.png" Width="20px" Height="20px" Font-Size="Smaller" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField ItemStyle-HorizontalAlign="Center" ItemStyle-Width="40px">
                                <ItemTemplate>
                                    <asp:ImageButton ID="EDITAR" runat="server" CommandName="EDITAR" OnClientClick="return GetSelectedRow(this)" ImageAlign="Middle" ImageUrl="~/ICONOS/writing.png"
                                        Width="20px" Height="20px" Font-Size="Smaller" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField ItemStyle-HorizontalAlign="Center" ItemStyle-Width="40px">
                                <ItemTemplate>
                                    <asp:ImageButton ID="ELIMINAR" CommandName="ELIMINAR" runat="server" ImageAlign="Middle" ImageUrl="~/ICONOS/rubbish.png"
                                        OnClientClick="if (!confirm('Esta seguro de eliminar el registro?')) return false;" Width="20px" Height="20px" Style="cursor: pointer" />
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="IMG" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="40px">
                                <ItemTemplate>
                                    <asp:ImageButton ID="IMAGEN" CommandName="IMAGEN" runat="server" ImageAlign="Middle" ImageUrl="~/ICONOS/camera.png"
                                        Width="20px" Height="20px" Style="cursor: pointer" />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="dgvBANCOS" EventName="SelectedIndexChanging" />
        </Triggers>
    </asp:UpdatePanel>

    <asp:HiddenField ID="hfCurrentRowIndex" runat="server"></asp:HiddenField>
    <asp:HiddenField ID="hfParentContainer" runat="server"></asp:HiddenField>
</asp:Content>
