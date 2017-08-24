<%@ Page Title="" Language="C#" MasterPageFile="~/Design/Site.Master" AutoEventWireup="true" CodeBehind="panel_FE.aspx.cs" Inherits="www.aquarella.com.pe.Aquarella.Ventas.panel_FE" %>
<asp:Content ID="Content1" ContentPlaceHolderID="headCPH" runat="server" >
    <style type="text/css">      
        .auto-style4 {
            width: 347px;
        }
        .auto-style5 {
            width: 328px;
        }
    </style>      
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderTitle" runat="server">
       Consultar Facturación Electronica
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderPageDesc" runat="server">
       Información acerca de la facturación electronica
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server" 
        EnableScriptGlobalization="True">
    </asp:ScriptManager>
    <!-- Area de errores -->
    <asp:UpdatePanel ID="upPanelMsg" runat="server">
        <ContentTemplate>
            <AQControl:Message runat="server" Visible="false" ID="msnMessage"></AQControl:Message>
        </ContentTemplate>
    </asp:UpdatePanel>
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
    <!-- -->
    <div style="margin: 10px auto 0 auto;">
        <table width="100%" class="tablagris" cellpadding="4">
            <tr>
                <td class="auto-style6">
                    <table>
                        <tr>
                            <td class="f12">
                                Fecha de Inicio</td>
                             <td>
                            </td>
                             <td>
                            </td>
                             <td class="f12">
                                 Fecha de Cierre</td>
                             <td>
                                 &nbsp;</td>
                             <td>
                                 &nbsp;</td>
                             <td>
                                 &nbsp;</td>
                             <td class="f12" width="5%">
                                    Cliente:</td>
                             <td>
                                </td>
                             <td class="f12" width="5%">
                                 Tipo:</td>
                             <td>
                                </td>
                             <td class="f12" width="5%">
                                 &nbsp;F: dni o ruc, doc </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:UpdatePanel ID="upStartDate" runat="server">
                                    <ContentTemplate>
                                        <asp:TextBox ID="txtDateStart" runat="server" AccessKey="f"></asp:TextBox>
                                        <ajaxToolkit:CalendarExtender ID="calendar" runat="server" Animated="true" 
                                            FirstDayOfWeek="Monday" Format="dd/MM/yyyy" PopupButtonID="imgCalendar" 
                                            TargetControlID="txtDateStart" />
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </td>
                             <td>
                                            <asp:Image ID="imgCalendar" runat="server" ImageUrl="~/Design/images/Botones/b_calendar_ico.gif"
                                                onmouseover="this.style.background='red';" onmouseout="this.style.background=''"
                                                Style="cursor:pointer;" />
                            </td>
                             <td>
                                            <asp:RequiredFieldValidator ValidationGroup="vsConsultForm" ID="rfvDateStart" runat="server"
                                                ToolTip="Fecha de inicio" CssClass="error_asterisck" ErrorMessage="Dígite fecha inicial"
                                                Display="Dynamic" SetFocusOnError="true" ControlToValidate="txtDateStart">*</asp:RequiredFieldValidator>
                                            <asp:CompareValidator ID="cvDateStart" runat="server" ValidationGroup="vsConsultForm"
                                                Type="Date" SetFocusOnError="true" CssClass="error_asterisck" ControlToValidate="txtDateStart"
                                                Operator="DataTypeCheck" ErrorMessage="Dígite una fecha válida">*</asp:CompareValidator>
                            </td>
                             <td>
                                 <asp:UpdatePanel ID="upEndDate" runat="server">
                                     <ContentTemplate>
                                         <asp:TextBox ID="txtDateEnd" runat="server" AccessKey="f"></asp:TextBox>
                                         <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" 
                                             Animated="true" FirstDayOfWeek="Monday" Format="dd/MM/yyyy" 
                                             PopupButtonID="imgCalendarDe" TargetControlID="txtDateEnd" />
                                     </ContentTemplate>
                                 </asp:UpdatePanel>
                            </td>
                             <td>
                                 <asp:Image ID="imgCalendarDe" runat="server" 
                                     ImageUrl="~/Design/images/Botones/b_calendar_ico.gif" 
                                     onmouseout="this.style.background=''" 
                                     onmouseover="this.style.background='red';" Style="cursor: pointer;" />
                            </td>
                             <td>
                                 <asp:RequiredFieldValidator ID="rfvDateEnd" runat="server" 
                                     ControlToValidate="txtDateEnd" CssClass="error_asterisck" Display="Dynamic" 
                                     ErrorMessage="Dígite fecha final*" SetFocusOnError="true" ToolTip="Fecha final" 
                                     ValidationGroup="vsConsultForm">*</asp:RequiredFieldValidator>
                                 <asp:CompareValidator ID="cvDateEnd" runat="server" 
                                     ControlToValidate="txtDateEnd" CssClass="error_asterisck" 
                                     ErrorMessage="Dígite una fecha final válida" Operator="DataTypeCheck" 
                                     SetFocusOnError="true" Type="Date" ValidationGroup="vsConsultForm">*</asp:CompareValidator>
                                 <asp:CompareValidator ID="cvDateStartDateEnd" runat="server" 
                                     ControlToCompare="txtDateStart" ControlToValidate="txtDateEnd" 
                                     CssClass="error_asterisck" 
                                     ErrorMessage="Dígite una fecha final superior a la fecha inicial" 
                                     Operator="GreaterThanEqual" SetFocusOnError="true" Type="Date" 
                                     ValidationGroup="vsConsultForm">*</asp:CompareValidator>
                            </td>
                             <td>
                                 &nbsp;</td>
                             <td>
                                                <asp:DropDownList ID="dwcliente" runat="server" AppendDataBoundItems="true" DataTextField="Are_Descripcion" DataValueField="Are_Id" ToolTip="Selecionar un lider" Width="200px" AutoPostBack="True" OnSelectedIndexChanged="dwcliente_SelectedIndexChanged">
                                                    <asp:ListItem Text=" -- Seleccionar a todos --" Value="-1"></asp:ListItem>
                                                </asp:DropDownList>
                            </td>
                             <td>
                                 &nbsp;</td>
                             <td>
                                                <asp:DropDownList ID="dwtipo" runat="server" AppendDataBoundItems="true" DataTextField="Are_Descripcion" DataValueField="Are_Id"  ToolTip="Selecionar un lider" Width="174px" AutoPostBack="True" OnSelectedIndexChanged="dwtipo_SelectedIndexChanged">
                                                    <asp:ListItem Text=" -- Seleccionar a todos --" Value="-1"></asp:ListItem>
                                                </asp:DropDownList>
                            </td>
                             <td>
                                                &nbsp;</td>
                             <td>
                    <asp:TextBox ID="txtdniruc" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                </td>                
                <td valign="middle" class="style2">
                    <asp:Button ID="btConsult" runat="server" Text="Consultar" ValidationGroup="vsConsultForm"
                        CausesValidation="true" OnClick="btConsult_Click" />
                </td>
                <td align="left" style="border-left: solid 1px silver;">
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style6">
                    <asp:ValidationSummary ID="vsConsultForm" ValidationGroup="vsConsultForm" runat="server"
                        EnableClientScript="true" HeaderText="Realice las siguientes correcciones" ShowMessageBox="true" />
                </td>
            </tr>
        </table>
    </div>
    <div style="margin: 10px auto 0 auto;" class="f-small" id="grid_v">
        <asp:UpdatePanel ID="upGrid" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <asp:GridView ID="gvReturns" runat="server" AutoGenerateColumns="False" 
            DataSourceID="odsReturns" Width="1082px" AllowPaging="True" BackColor="White" BorderColor="#DEDFDE" BorderStyle="None"  BorderWidth="1px" CellPadding="4" GridLines="Vertical" PageSize="15" AllowSorting="True" ForeColor="Black" OnSorting="gvReturns_Sorting" OnRowCreated="gvReturns_RowCreated">
                    <AlternatingRowStyle BackColor="White" />
<Columns>
                        <asp:BoundField DataField="tipo_des" HeaderText="Tipo" SortExpression="Tipo" >                        
                        <HeaderStyle HorizontalAlign="Left" />
                        <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="numero" HeaderText="Numero" SortExpression="Numero" >
                        <HeaderStyle HorizontalAlign="Left" />
                        <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                          <asp:BoundField DataField="Pedido" HeaderText="Pedido" SortExpression="Pedido" >
                        <HeaderStyle HorizontalAlign="Left" />
                        <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="dniruc" HeaderText="Dni-Ruc" SortExpression="dniruc" >
                        <HeaderStyle HorizontalAlign="Left" />
                        <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="nombres" HeaderText="Nombres"
                            ItemStyle-HorizontalAlign="Center" SortExpression="Nombres" >
                        <HeaderStyle HorizontalAlign="Left" />
                        <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="monto" HeaderText="Monto" >
                        <HeaderStyle HorizontalAlign="Right" />
                        <ItemStyle HorizontalAlign="Right" />
                        </asp:BoundField>
                        <asp:BoundField DataField="FechaEmision" HeaderText="Fecha E." SortExpression="FechaEmision">
                        <HeaderStyle HorizontalAlign="Center" />
                        <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                         <asp:BoundField DataField="esBaja" HeaderText="Anulado(Doc)">
                        <HeaderStyle HorizontalAlign="Center" />
                        <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField DataField="estadosunat" HeaderText="Estado(Sunat)">
                        <HeaderStyle HorizontalAlign="Left" />
                        <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                         <asp:TemplateField HeaderText="PDF" Visible="False">
                             <EditItemTemplate>
                                 <asp:TextBox ID="TextBox2" runat="server" Text='<%# Bind("pdf") %>'></asp:TextBox>
                             </EditItemTemplate>
                             <ItemTemplate>
                                 <asp:Label ID="Label2" runat="server" Text='<%# Bind("pdf") %>'></asp:Label>
                             </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Pdf">
                            <EditItemTemplate>
                                <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <center>
                                     <%--<a href='../../Aquarella/Ventas/descargar_pdf.aspx?numero=<%# Eval("numero")%>&rutapdf=<%# Eval("Pdf")%>'--%>
                                    <%--<a href='http://10.10.10.208/Web_DescargaPDF/Download_PDF.aspx?ruc=20101951872&tipo=03&serie=B253&numero=69816'--%>
                                    <a href='<%# Eval("URL_PDF")%>'
                                   title="Descargar pdf. <%# Eval("numero")%> ">
                                    <asp:Image ID="imgpdf" runat="server" Visible="false" Height="20px" ImageUrl="~/Design/images/pdf-viewer.png" Width="22px" />
                                    </a>
                                </center>                                
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <EmptyDataTemplate>
                        No existen datos para mostrar.
                    </EmptyDataTemplate>
                    <FooterStyle BackColor="#CCCC99" HorizontalAlign="Center" />
                    <HeaderStyle BackColor="#6B696B" Font-Bold="True" ForeColor="White" />
                    <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Right" />
                    <RowStyle BackColor="#F7F7DE" Font-Size="9pt" />
                    <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                    <SortedAscendingCellStyle BackColor="#FBFBF2" />
                    <SortedAscendingHeaderStyle BackColor="#848384" />
                    <SortedDescendingCellStyle BackColor="#EAEAD3" />
                    <SortedDescendingHeaderStyle BackColor="#575357" />
                </asp:GridView>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="btConsult" EventName="click" />                                      
                <asp:AsyncPostBackTrigger ControlID="dwcliente" EventName="SelectedIndexChanged" />
                <asp:AsyncPostBackTrigger ControlID="dwtipo" EventName="SelectedIndexChanged" />                
            </Triggers>
        </asp:UpdatePanel>
          <asp:ObjectDataSource ID="odsReturns" runat="server" SelectMethod="_leer_FE"
            TypeName="www.aquarella.com.pe.bll.F_Electronico" OnSelected="odsReturns_Selected">
            <SelectParameters>                
                <asp:ControlParameter ControlID="txtDateStart" Name="_fechaini" PropertyName="Text" Type="DateTime" />
                <asp:ControlParameter ControlID="txtDateEnd" Name="_fechafin" PropertyName="Text" Type="DateTime" />
                <asp:ControlParameter ControlID="dwcliente" Name="_bas_id" PropertyName="SelectedValue" />
                <asp:ControlParameter ControlID="dwtipo" Name="_tipo" PropertyName="SelectedValue" />
                <asp:ControlParameter ControlID="txtdniruc" Name="_numdocdniruc" PropertyName="Text" DefaultValue="-1" />
                <asp:Parameter Name="_cliente" Type="String" />
            </SelectParameters>
        </asp:ObjectDataSource>
    </div>
        <!-- ACTIVITY PANEL -->
        <ajaxToolkit:UpdatePanelAnimationExtender ID="upae" runat="server" TargetControlID="upGrid">
            <Animations>
                                        <OnUpdating>
                                            <Sequence>
                                                <%-- Disable all the controls --%>
                                                <Parallel duration="0">
                                                    <EnableAction AnimationTarget="btConsult" Enabled="false" />                                                  
                                                    <EnableAction AnimationTarget="btFilter" Enabled="false" />                                                  
                                                    <EnableAction AnimationTarget="chksd" Enabled="false" />                                                  
                                                    <FadeOut minimumOpacity=".7" />
                                                </Parallel>
                                            </Sequence>
                                        </OnUpdating>
                                        <OnUpdated>
                                            <Sequence>
                                                <%-- Enable all the controls --%>
                                                <Parallel duration="0">
                                                    <EnableAction AnimationTarget="btConsult" Enabled="true" />    
                                                    <EnableAction AnimationTarget="btFilter" Enabled="true" />  
                                                    <EnableAction AnimationTarget="chksd" Enabled="true" />                                                                                                                                                                                                   
                                                    <FadeIn minimumOpacity=".7" /> 
                                                </Parallel>
                                            </Sequence>
                                        </OnUpdated>
            </Animations>
        </ajaxToolkit:UpdatePanelAnimationExtender>    
</asp:Content>
