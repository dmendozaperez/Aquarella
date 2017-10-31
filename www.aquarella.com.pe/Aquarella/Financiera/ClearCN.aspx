<%@ Page Title="" Language="C#" MasterPageFile="~/Design/Site.Master" AutoEventWireup="true" CodeBehind="ClearCN.aspx.cs" Inherits="www.aquarella.com.pe.Aquarella.Financiera.ClearCN" %>
<asp:Content ID="Content1" ContentPlaceHolderID="headCPH" runat="server">
     <script type="text/javascript" language="javascript">
        // Habilitar el thickbox despues de una llamAQUARELLA asincrona por el ajax
        function pageLoad() {
            var isAsyncPostback = Sys.WebForms.PageRequestManager.getInstance().get_isInAsyncPostBack();
            if (isAsyncPostback) {
                $("input:submit,button").button();
            }
        }
    </script>
     <style type="text/css">
         .auto-style1 {
             width: 373px;
         }
         .auto-style2 {
             width: 484px;
         }
     </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderTitle" runat="server">
    Cruce de pagos y Facturacion (CENTRO DE NEGOCIO)
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderPageDesc" runat="server">
    Por favor seleccione las facturas y los pagos con los cuales va a cruzar.&nbsp;
    El sistema no permite grabar hasta que el "Gran Total" sea un número mayor a cero
    ó sus pagos superen el valor de sus facturas.
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <asp:ScriptManager ID="ScriptManager1" runat="server" AsyncPostBackTimeout="99999">
    </asp:ScriptManager>
     <asp:UpdatePanel ID="upHd" runat="server">
        <ContentTemplate>
            <asp:HiddenField ID="hdCreditValue" Value="0" runat="server" />
        </ContentTemplate>
    </asp:UpdatePanel>
    <!-- Area de errores -->
    <asp:UpdatePanel ID="upPanelMsg" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <AQControl:Message runat="server" Visible="false" ID="msnMessage"></AQControl:Message>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="btConsult" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="btCreateClear" EventName="Click" />
        </Triggers>
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
    <!-- SEL CUSTOMER -->
    <asp:Panel ID="pnlDwCustomers" Visible="true" runat="server">
           <div style="margin: 10px auto 0 auto;">
        <table width="100%" class="tablagris" cellpadding="4">
            <tr>
                <td class="auto-style1">
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
                                                Style="cursor: pointer;" />
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
                        </tr>
                    </table>
                </td>
                <td valign="middle" class="auto-style2">
                    <asp:Button ID="btConsult" runat="server" Text="Consultar" ValidationGroup="vsConsultForm"
                        CausesValidation="true" OnClick="btConsult_Click" />
                </td>
                <td align="left" style="border-left: solid 1px silver;">
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style1">
                    <asp:ValidationSummary ID="vsConsultForm" ValidationGroup="vsConsultForm" runat="server"
                        EnableClientScript="true" HeaderText="Realice las siguientes correcciones" ShowMessageBox="true" />
                </td>
            </tr>
        </table>
    </div>
    </asp:Panel>
    <!-- PANEL DE RESULTADOS -->
    <div style="margin: 10px auto 0 auto; min-height: 200px;" class="f13">
        <asp:UpdatePanel ID="upGridClear" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <asp:GridView ID="gvClear" runat="server" SkinID="gridviewSkin" ShowFooter="True"
                    HorizontalAlign="Center" OnRowDataBound="gvClear_RowDataBound"
                    OnDataBound="gvClear_DataBound" Width="80%" CellPadding="4" ForeColor="#333333" GridLines="None" AutoGenerateColumns="False">
                    <EditRowStyle BackColor="#2461BF" />
                    <EmptyDataTemplate>
                        No posee ninguna liquidación o saldo a favor.
                    </EmptyDataTemplate>
                    <AlternatingRowStyle BackColor="White" />
                    <Columns>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:CheckBox ID="chkDocument" runat="server" OnCheckedChanged="chkDocument_CheckedChanged"
                                    AutoPostBack="True" ToolTip='<%# Eval("dtv_transdoc_id")%>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="document_date_desc" HeaderText="Fecha" SortExpression="dtd_document_date"
                            ReadOnly="True" >
                          <HeaderStyle HorizontalAlign="Left" />
                        <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                          <asp:BoundField DataField="dtv_concept_id" HeaderText="Tipo" ReadOnly="True"
                            SortExpression="dtv_concept_id" FooterStyle-CssClass="f13" FooterStyle-Font-Bold="true">                        
                        <FooterStyle CssClass="f13" Font-Bold="True" />
                        <HeaderStyle HorizontalAlign="Left" />
                        <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="cov_description" HeaderText="Concepto" ReadOnly="True"
                            SortExpression="cov_description" FooterStyle-CssClass="f13" FooterStyle-Font-Bold="true"
                            FooterText="Gran Total : " >
                        <FooterStyle CssClass="f13" Font-Bold="True" />
                        <HeaderStyle HorizontalAlign="Left" />
                        <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="val" HeaderText="Monto" ReadOnly="True" ItemStyle-CssClass="f13"
                            SortExpression="val" ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right"
                            FooterStyle-CssClass="f13" FooterStyle-Font-Bold="true" >
                        <FooterStyle CssClass="f13" Font-Bold="True" HorizontalAlign="Right" />
                        <HeaderStyle HorizontalAlign="Right" />
                        <ItemStyle CssClass="f13" HorizontalAlign="Right" />
                        </asp:BoundField>
                        <asp:BoundField DataField="tipo" HeaderText="Tipo" ReadOnly="True" SortExpression="tipo"
                            ItemStyle-Font-Bold="true" ItemStyle-CssClass="f13" ItemStyle-HorizontalAlign="Center"
                            FooterStyle-HorizontalAlign="Center" >
                        <FooterStyle HorizontalAlign="Center" />
                        <ItemStyle CssClass="f13" Font-Bold="True" HorizontalAlign="Center" />
                        </asp:BoundField>
                    </Columns>
                    <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                    <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                    <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                    <RowStyle BackColor="#EFF3FB" />
                    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                    <SortedAscendingCellStyle BackColor="#F5F7FB" />
                    <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                    <SortedDescendingCellStyle BackColor="#E9EBEF" />
                    <SortedDescendingHeaderStyle BackColor="#4870BE" />
                </asp:GridView>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="btConsult" EventName="Click" />
                <asp:AsyncPostBackTrigger ControlID="btCreateClear" EventName="Click" />
            </Triggers>
        </asp:UpdatePanel>
        <asp:ObjectDataSource ID="odsClear" runat="server" SelectMethod="get_DocTranFacBy"
            TypeName="www.aquarella.com.pe.bll.Documents_Trans" OnSelected="odsClear_Selected">
            <SelectParameters>
                <asp:Parameter Name="_fechaini" Type="String" />
                <asp:Parameter Name="_fechafin" Type="String" />
            </SelectParameters>
        </asp:ObjectDataSource>
        <ajaxToolkit:UpdatePanelAnimationExtender runat="server" ID="upae" TargetControlID="upGridClear">
            <Animations>
                    <OnUpdating>
                        <Sequence>
                            <%-- Disable all the controls --%>
                            <Parallel duration="0">
                             <EnableAction AnimationTarget="btCreateClear" Enabled="false" />
                                <EnableAction AnimationTarget="btConsult" Enabled="false" />
                            </Parallel>
                        </Sequence>
                    </OnUpdating>
                    <OnUpdated>
                        <Sequence>
                            <%-- Enable all the controls --%>
                            <Parallel duration="0">
                                <EnableAction AnimationTarget="btConsult" Enabled="true" />
                                <EnableAction AnimationTarget="btCreateClear" Enabled="true" />
                            </Parallel>
                        </Sequence>
                    </OnUpdated>
            </Animations>
        </ajaxToolkit:UpdatePanelAnimationExtender>
    </div>
    <div style="margin: 10px auto 0 auto;">
        <table width="100%" cellpadding="4" cellspacing="4" class="f-small">
            <tr>
                <td align="center">
                    <asp:UpdatePanel ID="upBtClear" runat="server">
                        <ContentTemplate>
                            <asp:Button ID="btCreateClear" runat="server" AccessKey="o" Enabled="false" Text="(G)uardar el cruce realizado"
                                OnClick="btCreateClear_Click" />
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
