<%@ Page Title="" Language="C#" MasterPageFile="~/Design/Site.Master" AutoEventWireup="true"
    CodeBehind="panelPromocion.aspx.cs" Inherits="www.aquarella.com.pe.Aquarella.Maestros.panelPromocion"
    Theme="SiteTheme" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headCPH" runat="server">
    <title>Control de Promociones</title>
    <link media="screen" rel="stylesheet" href="../../Scripts/Colorbox/colorbox.css" />
    <script src="../../Scripts/Colorbox/jquery.colorbox.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(document).ready(function () {


            $(".iframe").colorbox({ width: "40%", height: "80%", iframe: true });
            $("#tabs").tabs({
                collapsible: true
            });
            $('#tabs').tabs('select', '#tab-1'); // Para seleccionar el tab 1 y que este colapsado el panel de insercion de rol 
        });

        function numbersonly(e) {
            var unicode = e.charCode ? e.charCode : e.keyCode
            if (unicode != 8 && unicode != 44) {
                if (unicode < 48 || unicode > 57) //if not a number
                { return false } //disable key press    
            }
        }

        function updateFunction(Ofe_Id, Ofe_Descripcion, Ofe_MaxPares, Ofe_Porc, FechaIni, FechaFin,estadoId) {
            removeFieldsErrors();

            $("#ContentPlaceHolder1_txtDesPromo").val(Ofe_Descripcion);
            $("#ContentPlaceHolder1_txtparProm").val(Ofe_MaxPares);
            $("#ContentPlaceHolder1_txtporcPromo").val(Ofe_Porc);
            $("#ContentPlaceHolder1_txtiniPromo").val(FechaIni);
            $("#ContentPlaceHolder1_txtfinPromo").val(FechaFin);
     
            $("#dialog").dialog({ width: 400, height: 400, modal: true, title: 'Editar Promocion', open: true });
            
            $("#dialog").dialog({ buttons: [{
                text: "Actualizar Promocion",
                Id:"botonActualizar",
                click: function () {
                    //if (Validacion())
                    updatePromocionAjax(Ofe_Id);
                }
            }]
            });
            if (estadoId == 'I') {
                document.getElementById('botonActualizar').style.display= 'none';
            }
            

        }

        function updatePromocionAjax(Ofe_Id) {

            var descripcion = $("#ContentPlaceHolder1_txtDesPromo").val();
            var par = $("#ContentPlaceHolder1_txtparProm").val();
            var porc = $("#ContentPlaceHolder1_txtporcPromo").val();

            if (descripcion == "" || par == "" || porc == "") {
                alert("Ingresar datos requeridos (*).")
            } else { 


                $.ajax({
                    type: "POST",
                    data: "{  'promo_id': '" + Ofe_Id + "','Ofe_Descripcion': '" + $("#ContentPlaceHolder1_txtDesPromo").val() + "','Ofe_MaxPares': '" + $("#ContentPlaceHolder1_txtparProm").val() + "','Ofe_Porc': '" + $("#ContentPlaceHolder1_txtporcPromo").val() + "','FechaIni': '" + $("#ContentPlaceHolder1_txtiniPromo").val() + "','FechaFin': '" + $("#ContentPlaceHolder1_txtfinPromo").val() + "'}",
                    url: "panelPromocion.aspx/ajaxUpdatePromocion",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (result) {
                        if (result.d == new String("1")) {
                            $('#dialog').dialog("close");
                        }
                        else {
                            alert("Ocurrio un error durante la acutalizacion");
                        }
                    },
                    error: function (result) { alert("A ocurrido un error y no se han realizado los cambios, verifique que su sesión no haya expirado, e intente de nuevo." + result); }
                });
            }
        }

        function removeFieldsErrors() {
            $("#validateTips").text("");
            $("#impFav_name").removeClass("ui-state-error");
        }

      
        function pageLoad() {
            var isAsyncPostback = Sys.WebForms.PageRequestManager.getInstance().get_isInAsyncPostBack();
            if (isAsyncPostback) {
                //Examples of how to assign the ColorBox event to elements
                $(".iframe").colorbox({ width: "40%", height: "80%", iframe: true });
            }
        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderTitle" runat="server">
    Control de Promociones
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderPageDesc" runat="server">
    Muestra la lista de promociones creadas. Permite crear una promocion dentro
    del sistema y editar las existentes.
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnableScriptGlobalization="True">
    </asp:ScriptManager>
    <!-- Area de errores -->
    <asp:UpdateProgress ID="UpdateProgress1" runat="server">
        <ProgressTemplate>
            <center>
                <div style="position: absolute; left: 0; background: #f5f5f5; filter: alpha(opacity=85);
                    opacity: 0.85; font-family: Georgia; text-align: center; width: 100%; font-size: medium;">
                    <img src="../../Design/images/ajax-loader.gif" alt="Por Favor Espere; Cargando Información." />
                    Cargando información...
                </div>
            </center>
        </ProgressTemplate>
    </asp:UpdateProgress>
    <asp:UpdatePanel runat="server">
        <ContentTemplate>
            <AQControl:Message ID="msnMessage" Visible="false" runat="server" />
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="btnSavePromocion" EventName="click" />
        </Triggers>
    </asp:UpdatePanel>
    <div id="tabs">
        <ul>
            <li><a href="#tabs-1">Nueva Promoción</a></li>
        </ul>
        <div id="tabs-1">
            <table border="0" cellpadding="0"  cellspacing="0">
                <tr>
                    <td style="width:200px">
                        Descripcion:
                    </td>
                    <td>
                        <asp:TextBox ID="txtDescripcion" runat="server" Width="260px" />
                    </td>
                    <td style="color:red">
                        <asp:RequiredFieldValidator ID="RFValidator" ValidationGroup="Nuevo" runat="server" ErrorMessage="*" ControlToValidate="txtDescripcion"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td style="width:200px">
                        Max. Pares
                    </td>
                    <td>
                        <asp:TextBox ID="txtPares" onkeypress="return numbersonly(event);" runat="server" Width="260px" />
                    </td>
                    <td style="color:red">
                       <asp:RequiredFieldValidator ID="RequiredFieldValidator6" ValidationGroup="Nuevo" runat="server" ErrorMessage="*" ControlToValidate="txtPares"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td>
                        Porcentaje(%):
                    </td>
                    <td>
                        <asp:TextBox ID="txtPorc" runat="server" Width="260px" />
                    </td>
                    <td style="color:red">
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator7" ValidationGroup="Nuevo"  runat="server" ErrorMessage="*" ControlToValidate="txtPorc"></asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="RegexDecimal" runat="server"  ValidationGroup="Nuevo"  ValidationExpression="((\d+)((\.\d{1,2})?))$" ErrorMessage="Ingrese porcentaje Valido" ControlToValidate="txtPorc" />
                        

                    </td>
                </tr>
                 <tr>
                    <td>
                         Fecha de Inicio:
                    </td>
                    <td>
                        <table>
                            <tr> 
                            <td>
                                <asp:UpdatePanel ID="upStartDate" runat="server">
                                    <ContentTemplate>
                                        <asp:TextBox ID="txtDateStart" disabled runat="server"  AccessKey="f"></asp:TextBox>
                                        <ajaxToolkit:CalendarExtender ID="calendar" runat="server" Animated="true" 
                                            FirstDayOfWeek="Monday" Format="dd/MM/yyyy" PopupButtonID="imgCalendar" 
                                            TargetControlID="txtDateStart" />
                                    </ContentTemplate>
                                </asp:UpdatePanel>

                            </td> 
                            <td style="color:red">
                         <asp:Image ID="imgCalendar" runat="server" ImageUrl="~/Design/images/Botones/b_calendar_ico.gif"
                                                onmouseover="this.style.background='red';" onmouseout="this.style.background=''"
                                                Style="cursor: pointer;" />
                                   <asp:RequiredFieldValidator ID="RequiredFieldValidator8"  ValidationGroup="Nuevo"  runat="server" ErrorMessage="*" ControlToValidate="txtDateStart"></asp:RequiredFieldValidator>
                            
                            </td> 
                            </tr>
                            </table>

                        
                                
                    </td>
                    <td>
                                           
                    </td>
                    <td>                               
                               
                </td>
                </tr>
                <tr>
                    <td>
                         Fecha de Fin:
                    </td>
                    <td>
                        <table>
                            <tr> 
                            <td>
                                 <asp:UpdatePanel ID="upEndDate" runat="server">
                                     <ContentTemplate>
                                         <asp:TextBox ID="txtDateEnd" disabled runat="server"  AccessKey="f"></asp:TextBox>
                                         <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" 
                                             Animated="true" FirstDayOfWeek="Monday" Format="dd/MM/yyyy" 
                                             PopupButtonID="imgCalendarDe" TargetControlID="txtDateEnd" />
                                     </ContentTemplate>
                                 </asp:UpdatePanel>
                            </td>
                             <td style="color:red">
                                 <asp:Image ID="imgCalendarDe" runat="server" 
                                     ImageUrl="~/Design/images/Botones/b_calendar_ico.gif" 
                                     onmouseout="this.style.background=''" 
                                     onmouseover="this.style.background='red';" Style="cursor:pointer;" />
                                 <asp:RequiredFieldValidator ID="RequiredFieldValidator9"  ValidationGroup="Nuevo"  runat="server" ErrorMessage="*" ControlToValidate="txtDateEnd"></asp:RequiredFieldValidator>
                            </td>
                                <td>
                                
                         
                            </td> 
                            </tr>
                            </table>

                        
                                
                    </td>
                    <td>
                                           
                    </td>
                    <td>
                               
                </td>
                </tr>
        
                
                <tr>
                    <td>
                    </td>
                    <td align="center">
                        <asp:Button ID="btnSavePromocion" Text="Guardar"  ValidationGroup="Nuevo" runat="server" OnClick="btnSavePromocion_Click" />
                    </td>
                    <td>
                    </td>
                </tr>
            </table>
        </div>
    </div>
  
    <br />
    <asp:UpdatePanel runat="server">
        <ContentTemplate>
            <asp:GridView ID="gridFunctions" AllowPaging="True" runat="server" AutoGenerateColumns="False"
                SkinID="gridviewSkin" 
                OnPageIndexChanging="gridFunctions_PageIndexChanging">
                <%--<RowStyle HorizontalAlign="center" />--%>
                <Columns>                    
                    <asp:BoundField DataField="Ofe_Id" HeaderText="Id" ItemStyle-HorizontalAlign="center"  />
                    <asp:BoundField DataField="Ofe_Descripcion" HeaderText="Descripcion" />
                    <asp:BoundField DataField="Ofe_MaxPares" ItemStyle-HorizontalAlign="center" HeaderText="Nro. Pares" />
                    <asp:BoundField DataField="Ofe_Porc" ItemStyle-HorizontalAlign="center"  HeaderText="Porcentaje (%)" />
                    <asp:BoundField DataField="FechaIni" ItemStyle-HorizontalAlign="center"  HeaderText="Fec. Inicio" />
                    <asp:BoundField DataField="FechaFin" ItemStyle-HorizontalAlign="center"  HeaderText="Fec. Fin" />
                      <asp:BoundField DataField="Estado" ItemStyle-HorizontalAlign="center"  HeaderText="Estado" />
                     <asp:TemplateField HeaderText="Editar">
                        <ItemTemplate>
                            <center>
                                <a href="#" onclick="updateFunction('<%# Eval("Ofe_Id") %>','<%# Eval("Ofe_Descripcion") %>','<%# Eval("Ofe_MaxPares") %>','<%# Eval("Ofe_Porc") %>','<%# Eval("FechaIni") %>','<%# Eval("FechaFin") %>','<%# Eval("estadoId") %>')">
                                    <asp:Image ID="Image1" ImageUrl="~/Design/images/Botones/editOrder.png" runat="server" />
                                </a>
                            </center>
                        </ItemTemplate>
                    </asp:TemplateField>
       
                    <asp:TemplateField HeaderText="ver Articulos">
                        <ItemTemplate>
                            <center>
                                <a class="iframe" href="panelProm_App.aspx?PROM_ID=<%# Eval("Ofe_Id")%>&PROM_DESCRIPCION=<%# Eval("Ofe_Descripcion") %>&ESTADO=<%# Eval("estadoId")%>&TITULO=<%# Eval("Ofe_Descripcion")%>"
                                 title="Adicionar un articulo a la promocion.">
                                    <asp:Image ID="Image2" ImageUrl="~/Design/images/Botones/b_app.png" runat="server" />
                                </a>
                            </center>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </ContentTemplate>
    </asp:UpdatePanel>
     <div id="dialog" class="f13" style="display: none; font-size: 10px;">
            <table border="0" cellpadding="0"  cellspacing="0">
                <tr>
                    <td style="width:200px">
                        Descripcion:
                    </td>
                    <td>
                        <asp:TextBox ID="txtDesPromo" runat="server" Width="260px" />
                    </td>
                    <td style="color:red">
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ValidationGroup="upd"  runat="server" ErrorMessage="*" ControlToValidate="txtDesPromo"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td style="width:200px">
                        Max. Pares
                    </td>
                    <td>
                        <asp:TextBox ID="txtparProm" onkeypress="return numbersonly(event);"  runat="server" Width="260px" />
                    </td>
                    <td style="color:red">
                       <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ValidationGroup="upd"   ErrorMessage="*" ControlToValidate="txtparProm"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td>
                        Porcentaje(%):
                    </td>
                    <td>
                        <asp:TextBox ID="txtporcPromo" runat="server" Width="260px" />
                    </td>
                     <td style="color:red">
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ValidationGroup="upd"   ValidationExpression="((\d+)((\.\d{1,2})?))$" ErrorMessage="*" ControlToValidate="txtporcPromo" />
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ValidationGroup="upd"   ErrorMessage="*" ControlToValidate="txtporcPromo"></asp:RequiredFieldValidator>

                    </td>
                </tr>
                 <tr>
                    <td>
                         Fecha de Inicio:
                    </td>
                    <td>
                        <table>
                            <tr> 
                            <td>
                                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                    <ContentTemplate>
                                        <asp:TextBox ID="txtiniPromo" disabled runat="server"  AccessKey="f"></asp:TextBox>
                                        <ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server" Animated="true" 
                                            FirstDayOfWeek="Monday" Format="dd/MM/yyyy" PopupButtonID="Image3" 
                                            TargetControlID="txtiniPromo" />
                                    </ContentTemplate>
                                </asp:UpdatePanel>

                            </td> 
                            <td>
                           <asp:Image ID="Image3" runat="server" ImageUrl="~/Design/images/Botones/b_calendar_ico.gif"
                                                onmouseover="this.style.background='red';" onmouseout="this.style.background=''"
                                                Style="cursor: pointer;" />
                            </td> 
                            </tr>
                            </table>

                        
                                
                    </td>
                    <td>
                                           
                    </td>
                    <td>
                                <asp:RequiredFieldValidator ValidationGroup="upd"   ID="RequiredFieldValidator3" runat="server"
                                    ToolTip="Fecha de inicio" CssClass="error_asterisck" ErrorMessage="Dígite fecha inicial"
                                    Display="Dynamic" SetFocusOnError="true" ControlToValidate="txtiniPromo">*</asp:RequiredFieldValidator>
                                <asp:CompareValidator ID="CompareValidator2" runat="server" ValidationGroup="upd"  
                                    Type="Date" SetFocusOnError="true" CssClass="error_asterisck" ControlToValidate="txtiniPromo"
                                    Operator="DataTypeCheck" ErrorMessage="Dígite una fecha válida">*</asp:CompareValidator>
                </td>
                </tr>
                <tr>
                    <td>
                         Fecha de Fin:
                    </td>
                    <td>
                        <table>
                            <tr> 
                            <td>
                                 <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                     <ContentTemplate>
                                         <asp:TextBox ID="txtfinPromo" disabled runat="server"  AccessKey="f"></asp:TextBox>
                                         <ajaxToolkit:CalendarExtender ID="CalendarExtender3" runat="server" 
                                             Animated="true" FirstDayOfWeek="Monday" Format="dd/MM/yyyy" 
                                             PopupButtonID="Image4" TargetControlID="txtfinPromo" />
                                     </ContentTemplate>
                                 </asp:UpdatePanel>
                            </td>
                             <td>
                                 <asp:Image ID="Image4" runat="server" 
                                     ImageUrl="~/Design/images/Botones/b_calendar_ico.gif" 
                                     onmouseout="this.style.background=''" 
                                     onmouseover="this.style.background='red';" Style="cursor:pointer;" />
                            </td>
                                <td>
                                 <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" 
                                     ControlToValidate="txtDateEnd" CssClass="error_asterisck" Display="Dynamic" 
                                     ErrorMessage="Dígite fecha final*" SetFocusOnError="true" ToolTip="Fecha final" 
                                     ValidationGroup="upd"  >*</asp:RequiredFieldValidator>
                                 <asp:CompareValidator ID="CompareValidator3" runat="server" 
                                     ControlToValidate="txtfinPromo" CssClass="error_asterisck" 
                                     ErrorMessage="Dígite una fecha final válida" Operator="DataTypeCheck" 
                                     SetFocusOnError="true" Type="Date" ValidationGroup="upd"  >*</asp:CompareValidator>
                                 <asp:CompareValidator ID="CompareValidator4" runat="server" 
                                     ControlToCompare="txtiniPromo" ControlToValidate="txtfinPromo" 
                                     CssClass="error_asterisck" 
                                     ErrorMessage="Dígite una fecha final superior a la fecha inicial" 
                                     Operator="GreaterThanEqual" SetFocusOnError="true" Type="Date" 
                                     ValidationGroup="upd"  >*</asp:CompareValidator>
                            </td> 
                            </tr>
                            </table>

                        
                                
                    </td>
                    <td>
                                           
                    </td>
                    <td>
                                <asp:RequiredFieldValidator ValidationGroup="upd"   ID="RequiredFieldValidator5" runat="server"
                                    ToolTip="Fecha de inicio" CssClass="error_asterisck" ErrorMessage="Dígite fecha inicial"
                                    Display="Dynamic" SetFocusOnError="true" ControlToValidate="txtDateStart">*</asp:RequiredFieldValidator>
                                <asp:CompareValidator ID="CompareValidator5" runat="server" ValidationGroup="upd"  
                                    Type="Date" SetFocusOnError="true" CssClass="error_asterisck" ControlToValidate="txtDateStart"
                                    Operator="DataTypeCheck" ErrorMessage="Dígite una fecha válida">*</asp:CompareValidator>
                </td>
                </tr>
        
            </table>
    </div>

</asp:Content>
