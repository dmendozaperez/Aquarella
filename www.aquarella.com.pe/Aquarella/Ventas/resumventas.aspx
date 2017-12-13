<%@ Page Title="" Language="C#" MasterPageFile="~/Design/Site.Master" AutoEventWireup="true" CodeBehind="resumventas.aspx.cs" Inherits="www.aquarella.com.pe.Aquarella.Ventas.resumventas" %>
<asp:Content ID="Content1" ContentPlaceHolderID="headCPH" runat="server">
    <style type="text/css">
        .auto-style1 {
            width: 126px;
        }
        .auto-style2 {
            width: 221px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderTitle" runat="server">
     Resumen de ventas por Semana
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderPageDesc" runat="server">
    Este reporte compara ventas de semana con el año anterior
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
      <asp:ScriptManager ID="ScriptManager1" runat="server" 
        EnableScriptGlobalization="True">
    </asp:ScriptManager>
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
     <div style="margin: 10px auto 0 auto;">
          <table width="100%" class="tablagris" cellpadding="4">
            <tr>
                <td class="style1">
                    <table>                       
                        <tr>
                            <td class="auto-style2">
                                Año</td>
                             <td class="auto-style1">
                            </td>
                        </tr>
                        <tr>
                            <td class="auto-style2">
                                                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                                        <ContentTemplate>
                                                            <asp:DropDownList ID="dwanio" runat="server" AppendDataBoundItems="true" 
                                                                DataTextField="anio" DataValueField="idanio" ToolTip="Selecionar el año" 
                                                                Width="180px">
                                                                <asp:ListItem Text=" -- Seleccionar el año --" Value="-1"></asp:ListItem>
                                                            </asp:DropDownList>
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>
                            </td>
                             <td class="auto-style1">
                       <asp:Button ID="btConsult" runat="server" Text="Consultar" ValidatioGroup="vsConsultForm"
                        CausesValidation="true" OnClick="btConsult_Click" /> 
                            </td>
                              <td align="left" style="border-left: solid 1px silver;">
                    <asp:ImageButton ID="ibExportToExcel" ImageUrl="~/Design/images/Botones/b_toExcel.png"
                        onmouseover="this.style.background='green';" onmouseout="this.style.background=''"
                        runat="server" Height="25px" Width="24px" 
                        ToolTip="Exportar Panel de Resultados a Excel." OnClick="ibExportToExcel_Click" 
                         />
                </td>
                        </tr>
                    </table>
                </td>
                <td valign="middle" class="style4"  >
                       <br />
                </td>
                <td align="left" style="border-left: solid 1px silver;">
                    &nbsp;</td>
            </tr>
            </table>
     </div>
     <div style="margin: 1px 1px 1px 1px;  width: 1078px; overflow: auto;">
        <asp:UpdatePanel ID="upGrid" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <asp:GridView ID="gvReturns" runat="server" AllowSorting="True" 
                    ShowFooter="True" AllowPaging="True" PageSize="20"
                 SkinID="gridviewSkin" PagerStyle-HorizontalAlign="Left" Font-Size="Small" 
                    CellPadding="3" Width="1072px" OnPageIndexChanging="gvReturns_PageIndexChanging" AutoGenerateColumns="False" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px">
                    <FooterStyle BackColor="White" ForeColor="#000066" />
                    <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" />
                    <PagerStyle CssClass="GridViewBlue-tf" BackColor="White" ForeColor="#000066" 
                        HorizontalAlign="Left" />
                    <Columns>
                        <asp:BoundField DataField="Año" HeaderText="Año">
                        <HeaderStyle HorizontalAlign="Center" />
                        <ItemStyle BackColor="#FFFFCC" HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField DataField="Semana" HeaderText="Semana">
                        <ControlStyle BackColor="#FFFFCC" />
                        <HeaderStyle HorizontalAlign="Center" />
                        <ItemStyle BackColor="#FFFFCC" HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField DataField="Total Tickets" HeaderText="Total Tickets">
                        <HeaderStyle HorizontalAlign="Center" Wrap="True" />
                        <ItemStyle BackColor="#FFFFCC" HorizontalAlign="Center" Wrap="True" />
                        </asp:BoundField>
                        <asp:BoundField DataField="Pares" HeaderText="Pares">
                        <HeaderStyle HorizontalAlign="Center" />
                        <ItemStyle BackColor="#FFFFCC" HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField DataField="Total + Igv" HeaderText="Total + Igv">
                        <HeaderStyle HorizontalAlign="Center" />
                        <ItemStyle BackColor="#FFFFCC" HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField DataField="Precio Promedio" HeaderText="Precio Promedio">
                        <HeaderStyle HorizontalAlign="Center" />
                        <ItemStyle BackColor="#FFFFCC" HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField DataField="N Pares por Ticket" HeaderText="N Pares por Ticket">
                        <HeaderStyle HorizontalAlign="Center" />
                        <ItemStyle BackColor="#FFFFCC" HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField DataField="Año1" HeaderText="Año">
                        <HeaderStyle HorizontalAlign="Center" />
                        <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField DataField="Semana1" HeaderText="Semana">
                        <HeaderStyle HorizontalAlign="Center" />
                        <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField DataField="Total Tickets1" HeaderText="Total Tickets">
                        <HeaderStyle HorizontalAlign="Center" />
                        <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField DataField="Pares1" HeaderText="Pares">
                        <HeaderStyle HorizontalAlign="Center" />
                        <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField DataField="Total + Igv1" HeaderText="Total + Igv">
                        <HeaderStyle HorizontalAlign="Center" />
                        <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField DataField="Precio Promedio1" HeaderText="Precio Promedio">
                        <HeaderStyle HorizontalAlign="Center" />
                        <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField DataField="N Pares por Ticket1" HeaderText="N Pares por Ticket">
                        <HeaderStyle HorizontalAlign="Center" />
                        <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                    </Columns>
                    <EmptyDataTemplate>
                        No existen registros para mostrar.
                    </EmptyDataTemplate>
                    <RowStyle ForeColor="#000066" />
                    <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                    <SortedAscendingCellStyle BackColor="#F1F1F1" />
                    <SortedAscendingHeaderStyle BackColor="#007DBB" />
                    <SortedDescendingCellStyle BackColor="#CAC9C9" />
                    <SortedDescendingHeaderStyle BackColor="#00547E" />
                </asp:GridView>
            </ContentTemplate>
            <Triggers>
               
                <asp:AsyncPostBackTrigger ControlID="btConsult" EventName="Click" />
                <asp:AsyncPostBackTrigger ControlID="gvReturns" EventName="PageIndexChanging" />
               
            </Triggers>
        </asp:UpdatePanel>
       
        <!-- ACTIVITY PANEL -->
        <ajaxToolkit:UpdatePanelAnimationExtender ID="upae" runat="server" TargetControlID="upGrid">
            <Animations>
                                        <OnUpdating>
                                            <Sequence>
                                                <%-- Disable all the controls --%>
                                                <Parallel duration="0">
                                                    <EnableAction AnimationTarget="btConsult" Enabled="false" />                                                   
                                                </Parallel>
                                            </Sequence>
                                        </OnUpdating>
                                        <OnUpdated>
                                            <Sequence>
                                                <%-- Enable all the controls --%>
                                                <Parallel duration="0">
                                                    <EnableAction AnimationTarget="btConsult" Enabled="true" />
                                                </Parallel>
                                            </Sequence>
                                        </OnUpdated>
            </Animations>
        </ajaxToolkit:UpdatePanelAnimationExtender>
    </div>
</asp:Content>
