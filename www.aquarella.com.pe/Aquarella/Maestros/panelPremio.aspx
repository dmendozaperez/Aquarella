<%@ Page Title="" Language="C#" MasterPageFile="~/Design/Site.Master" AutoEventWireup="true"
    CodeBehind="panelPremio.aspx.cs" Inherits="www.aquarella.com.pe.Aquarella.Maestros.panelPremio"
    Theme="SiteTheme" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headCPH" runat="server">
    <title>Control de Premios</title>
    <link media="screen" rel="stylesheet" href="../../Scripts/Colorbox/colorbox.css" />
    <script src="../../Scripts/Colorbox/jquery.colorbox.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(document).ready(function () {


            $(".iframe").colorbox({ width: "40%", height: "80%", iframe: true });
            
        });

        function pageLoad() {
            var isAsyncPostback = Sys.WebForms.PageRequestManager.getInstance().get_isInAsyncPostBack();
            


        }

        function numbersonly(e) {
            var unicode = e.charCode ? e.charCode : e.keyCode
            if (unicode != 8 && unicode != 44) {
                if (unicode < 48 || unicode > 57) //if not a number
                { return false } //disable key press    
            }
        }

        function updatePremio(Ofe_Id, Ofe_Descripcion, Ofe_MaxPares, Ofe_Porc, FechaIni, FechaFin,estadoId) {
            removeFieldsErrors();

            document.getElementById('dialog').innerHTML = '<p>hola</p>';
     
            $("#dialog").dialog({ width: 400, height: 400, modal: true, title: 'Editar Promocion', open: true });
            
            $("#dialog").dialog();

       
        }

        function ListarArticulo(stridPremio,Descripcion) {
            //Ajax
         
            var urlMethod = "panelPremio.aspx/ListarArticulo";
            var jsonData = '{idPremio:"' + jQuery.trim(stridPremio) + '"}';
            SendAjax(urlMethod, jsonData, DibujarTallas);
        }

        //Ajax
        function SendAjax(urlMethod, jsonData, returnFunction) {
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                url: urlMethod,
                data: jsonData,
                dataType: "json",
                //async: true,
                success: function (msg) {
                    // 
                    if (msg != null) {
                        returnFunction(msg);
                    }
                },
                error: function (xhr, status, error) {
                    // Boil the ASP.NET AJAX error down to JSON.
                    var err = eval("(" + xhr.responseText + ")");
                    alert(err.Message);
                }
            });
        }
        //

        function DibujarTallas(msg) {

            var ArticuloIni = "";
            var TdArticulo = "";
            var tr = "";
            var listTr = "";
            var listTalla = "";
            var listCantidad = "";
            var total = 0;
            var ini = 0;
            var colortr = 'style="border-color:#DEDFDE;border-width:1px;border-style:Solid;"'
            var colortd = ';border-color:#DEDFDE;border-width:1px;border-style:Solid;'
            $.each(msg.d, function (key, val) {

                ini++;
                var articulo = val.Premio_Articulo

                if (articulo != ArticuloIni && ini == 1) {
                    ArticuloIni = articulo;
                }
                
                if (articulo != ArticuloIni && ini > 1) {
                    TdArticulo += '<tr ' + colortr + '>'
                    TdArticulo += '<td style="text-align:center;width:100px' + colortd + '">'
                    TdArticulo += ArticuloIni
                    TdArticulo += '</td>'
                    TdArticulo += '<td style="width:200px' + colortd + '">'
                    TdArticulo += '<table>'
                    TdArticulo += '<tr>'
                    TdArticulo += listTalla
                    TdArticulo += '</tr>'
                    TdArticulo += '<tr>'
                    TdArticulo += listCantidad
                    TdArticulo += '</tr>'
                    TdArticulo += ' </table>'
                    TdArticulo += '</td>'
                    TdArticulo += '<td style="text-align:center;width:100px' + colortd + '">'
                    TdArticulo +=   total
                    TdArticulo += '</td>'
                    TdArticulo += '</tr>'

                    listTalla = "";
                    listCantidad = "";
                    ArticuloIni = articulo;
                    total = 0;
                }
                
                total += parseInt(val.Premio_cantidad)
                listTalla += '<td style="color:White;background-color:#507CD1;width:20px' + colortd + '">'
                listTalla += val.Premio_talla
                listTalla += '</td>'
  
                listCantidad += '<td style="width:20px' + colortd + '">'
                listCantidad += val.Premio_cantidad
                listCantidad += '</td>'
           
            });

            TdArticulo += '<tr ' + colortr + '>'
            TdArticulo += '<td style="text-align:center;width:100px' + colortd + '">'
            TdArticulo += ArticuloIni
            TdArticulo += '</td>'
            TdArticulo += '<td style="width:200px' + colortd + '">'
            TdArticulo += ' <table>'
            TdArticulo += '<tr>'
            TdArticulo += listTalla
            TdArticulo += '</tr>'
            TdArticulo += '<tr>'
            TdArticulo += listCantidad
            TdArticulo += '</tr>'
            TdArticulo += ' </table>'
            TdArticulo += '</td>'
            TdArticulo += '<td style="text-align:center;width:100px' + colortd + '">'
            TdArticulo += total
            TdArticulo += '</td>'
            TdArticulo += '</tr>'

            var trHeader = '<tr style="color:White;background-color:#507CD1;font-weight:bold;">'
            trHeader += '<td style="text-align:center;width:100px">'
            trHeader +=  'Articulo'
            trHeader += '</td>'
            trHeader += '<td style="text-align:center;width:200px">'
            trHeader += 'Tallas - Cantidades'
            trHeader += '</td>'
            trHeader += '<td style="width:100px;text-align:center;">'
            trHeader += 'Total'
            trHeader += '</td>'
            trHeader += '</tr>'

            var table = ' <table style="color:#333333;background-color:White;border-color:#CCCCCC;border-width:1px;border: 1px solid black;font-size:8pt;width:100%;border-collapse:collapse; border-collapse: collapse;">' + trHeader + TdArticulo + '</table>'
            document.getElementById('dialog').innerHTML = table;

            $("#dialog").dialog({ width: 400, height: 400, modal: true, title: 'Lista de Articulos', open: true });

            $("#dialog").dialog();

  
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

                window.location.href = "panelPromocion.aspx"
            
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
        Control de Premios
    </asp:Content>
    <asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderPageDesc" runat="server">
        Muestra la lista de Premios creadas. Permite ver los articulos asociados al premio y editar el listado.
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
           
        </asp:UpdatePanel>
        
        <br />
        <asp:UpdatePanel runat="server">
            <ContentTemplate>
            <asp:GridView ID="gridPremios" runat="server" Width="98%" SkinID="gridviewSkin"
                AutoGenerateColumns="False"  Font-Size="8" AllowPaging="True" AllowSorting="True" CellPadding="3"
                    ShowHeaderWhenEmpty="True" BackColor="White" BorderColor="#CCCCCC" BorderStyle="Solid" BorderWidth="1px" 
                OnRowCommand="gridPremios_RowCommand" 
                    PageSize="8">
                            
                <Columns>
                    <asp:BoundField DataField="Id" HeaderText="Id" ItemStyle-HorizontalAlign="center"  />
                    <asp:BoundField DataField="Descripcion" HeaderText="Descripcion" />
                    <asp:BoundField DataField="Monto" DataFormatString="{0:C}"   ItemStyle-HorizontalAlign="center" HeaderText="Monto Min." />

                        <asp:TemplateField HeaderText="ver Articulos">
                        <ItemTemplate>
                            <center>
                                <a href="#" onclick="ListarArticulo('<%# Eval("Id") %>','<%# Eval("Descripcion") %>')">
                                    <asp:Image ID="Image1" ImageUrl="~/Design/images/Botones/b_app.png" runat="server" />
                                </a>
                            </center>
                        </ItemTemplate>
                    </asp:TemplateField>
                        <asp:TemplateField HeaderText="Modificar Articulos" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <asp:ImageButton ID="imgedit" CommandArgument='<%# Eval("Id")%>'
                                CommandName="EditPremio"  runat="server" ImageUrl="~/Design/images/Botones/b_modificar_ico.gif"
                                Visible="true" ToolTip="Cargar para edición." BorderWidth="0" />
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
                                    
                </Columns>
                <FooterStyle BackColor="White" ForeColor="#000066" />
                <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" />
                <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                <RowStyle BorderColor="#DEDFDE" BorderWidth="1px" BorderStyle="Solid" ForeColor="#000066" />
                <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                <SortedAscendingCellStyle BackColor="#F1F1F1" />
                <SortedAscendingHeaderStyle BackColor="#007DBB" />
                <SortedDescendingCellStyle BackColor="#CAC9C9" />
                <SortedDescendingHeaderStyle BackColor="#00547E" />
            </asp:GridView>  
                                   
        </ContentTemplate>
        <Triggers>                    
            <asp:AsyncPostBackTrigger ControlID="gridPremios" EventName="PageIndexChanging" />
        </Triggers>




    </asp:UpdatePanel>
     <div id="dialog" class="f13" style="display: none; font-size: 10px;">
          
    </div>

</asp:Content>
