<%@ Page Title="" Language="C#" MasterPageFile="~/Design/Site.Master" AutoEventWireup="true" CodeBehind="Consulta_Archivo_Banco.aspx.cs" Inherits="www.aquarella.com.pe.Aquarella.Financiera.Consulta_Archivo_Banco" %>
<asp:Content ID="Content1" ContentPlaceHolderID="headCPH" runat="server">
    <style type="text/css">
        .style1
        {
            width: 29%;
        }
        .style2
        {
            width: 206px;
        }
        .style3
        {
            width: 92px;
        }
        .style4
        {
            width: 639px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderTitle" runat="server">
    Consultar Archivo de Banco
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderPageDesc" runat="server">
    Información acerca del Archivo de banco
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
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
                <td class="style4">
                    Panel de listado de archivo de banco
                </td>
                <td>
                    <asp:ImageButton ID="ibExportToExcel" ImageUrl="~/Design/images/Botones/b_toExcel.png"
                        runat="server" Height="25px" Width="24px" ToolTip="Exportar Panel de Resultados a Excel."
                        onmouseover="this.style.background='green';" onmouseout="this.style.background=''"
                        OnClick="ibExportToExcel_Click" Visible="False" />
                </td>
            </tr>
            <tr>
                <td class="style4">
                    <table width="100%" cellpadding="1" cellspacing="1">
                        <tr>
                            <td class="style1">
                                Realice un filtro de los resultados:<br />
                                (Desc: Puede buscar por cliente,dni/ruc,monto,operacion)
                            </td>
                            <td class="style2">
                                Dígite un filtro:
                                <asp:UpdatePanel ID="upFilter" runat="server">
                                    <ContentTemplate>
                                        <asp:TextBox ID="txtFilter" runat="server" Width="190px"></asp:TextBox>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </td>
                            <td class="style3">
                                <br />
                                <asp:Button ID="btFilter" runat="server" Text="Filtrar" OnClick="btFilter_Click" />
                            </td>
                            <td>
                                <br />
                                <asp:Button ID="btRefresh" runat="server" Text="Refrescar panel" OnClick="btRefresh_Click" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </div>
    <!-- PANEL DE RESULTADOS -->
    <div style="margin: 10px auto 0 auto;" class="f-small">
        <asp:UpdatePanel ID="upGridLiqPick" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <asp:GridView ID="gvbanco" runat="server" SkinID="gridviewSkin" ShowFooter="True"
                    OnDataBound="gvLiqs_DataBound" FooterStyle-HorizontalAlign="Right" 
                    CellPadding="4" ForeColor="Black" 
                    AutoGenerateColumns="False" Width="764px" AllowPaging="True" 
                    PageSize="17" BackColor="White" BorderColor="#3366CC" BorderStyle="None" 
                    BorderWidth="1px" Font-Size="Small">
                    <EmptyDataTemplate>
                        No existen pagos a verificar.
                    </EmptyDataTemplate>
                    <Columns>
                        <asp:BoundField DataField="Fecha_OP" HeaderText="Fecha" 
                            SortExpression="Fecha_OP" >                        
                        <HeaderStyle HorizontalAlign="Left" />
                        <ItemStyle HorizontalAlign="Left" ForeColor="Black" />
                        </asp:BoundField>
                        <asp:BoundField DataField="Cliente" HeaderText="Cliente" 
                            SortExpression="Cliente" >
                        <HeaderStyle HorizontalAlign="Left" />
                        <ItemStyle HorizontalAlign="Left" ForeColor="Black" />
                        </asp:BoundField>
                        <asp:BoundField DataField="dni_ruc" HeaderText="Dni-Ruc" 
                            SortExpression="dni_ruc" >
                        <HeaderStyle HorizontalAlign="Left" />
                        <ItemStyle HorizontalAlign="Left" ForeColor="Black" />
                        </asp:BoundField>
                        <asp:BoundField DataField="Voucher_Monto" HeaderText="Monto" SortExpression="Voucher_Monto"
                            ItemStyle-HorizontalAlign="Center" DataFormatString="{0:N2}" >
                        <HeaderStyle HorizontalAlign="Right" />
                        <ItemStyle HorizontalAlign="Right" ForeColor="Black" />
                        </asp:BoundField>
                        <asp:BoundField DataField="Voucher_Op" HeaderText="Operacion" 
                            SortExpression="Voucher_Op" >
                        <HeaderStyle HorizontalAlign="Left" />
                        <ItemStyle HorizontalAlign="Left" ForeColor="Black" />
                        </asp:BoundField>
                    </Columns>
                    <FooterStyle BackColor="#99CCCC" ForeColor="#003399" 
                        HorizontalAlign="Right" />
                    <HeaderStyle BackColor="#003399" Font-Bold="True" ForeColor="#CCCCFF" />
                    <PagerStyle BackColor="#99CCCC" ForeColor="#003399" HorizontalAlign="Left" />
                    <RowStyle BackColor="White" ForeColor="#003399" />
                    <SelectedRowStyle BackColor="#009999" Font-Bold="True" ForeColor="#CCFF99" />
                    <SortedAscendingCellStyle BackColor="#EDF6F6" />
                    <SortedAscendingHeaderStyle BackColor="#0D4AC4" />
                    <SortedDescendingCellStyle BackColor="#D6DFDF" />
                    <SortedDescendingHeaderStyle BackColor="#002876" />
                </asp:GridView>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="btRefresh" EventName="click" />
                <asp:AsyncPostBackTrigger ControlID="btFilter" EventName="click" />                
            </Triggers>
        </asp:UpdatePanel>
        <asp:ObjectDataSource ID="odsbanco" runat="server" 
            SelectMethod="archivo_data_banco" TypeName="www.aquarella.com.pe.bll.Payments"
            OnSelected="odsbanco_Selected">           
        </asp:ObjectDataSource>
        <asp:ObjectDataSource ID="odsFilter" runat="server" SelectMethod="getFilterObject"
            TypeName="www.aquarella.com.pe.bll.Util.Utilities" OnSelecting="odsFilter_Selecting"
            OnSelected="odsFilter_Selected">
            <SelectParameters>
                <asp:Parameter Name="dtObj" Type="Object" DefaultValue="" />
                <asp:Parameter DefaultValue="0" Name="posTable" Type="Int32" />
                <asp:Parameter DefaultValue="Cliente" Name="f1" Type="String" />
                <asp:Parameter DefaultValue="dni_ruc" Name="f2" Type="String" />
                <asp:Parameter DefaultValue="voucher_monto" Name="f3" Type="String" />
                <asp:Parameter DefaultValue="voucher_op" Name="f4" Type="String" />
                <asp:ControlParameter ControlID="txtFilter" DefaultValue="" Name="fieldValue1" PropertyName="Text"
                    Type="String" />
                <asp:ControlParameter ControlID="txtFilter" Name="fieldValue2" PropertyName="Text"
                    Type="String" />
                <asp:ControlParameter ControlID="txtFilter" DefaultValue="" Name="fieldValue3" PropertyName="Text"
                    Type="String" />
                <asp:ControlParameter ControlID="txtFilter" DefaultValue="" Name="fieldValue4" PropertyName="Text"
                Type="String" />
                <asp:Parameter DefaultValue="banco_archivo" Name="fieldOrder" Type="String" />
            </SelectParameters>
        </asp:ObjectDataSource>
        <!-- -->
      <%--  <asp:ObjectDataSource ID="odsFilter2" runat="server" SelectMethod="getFilterObject"
            TypeName="www.aquarella.com.pe.bll.Util.Utilities" OnSelected="odsFilter_Selected"
            OnSelecting="odsFilter2_Selecting">
            <SelectParameters>
                <asp:Parameter Name="dtObj" Type="Object" DefaultValue="" />
                <asp:Parameter DefaultValue="0" Name="posTable" Type="Int32" />
                <asp:Parameter DefaultValue="voucher_op" Name="f1" Type="String" />               
                <asp:Parameter DefaultValue="voucher_op" Name="fieldOrder" Type="String" />
            </SelectParameters>
        </asp:ObjectDataSource>--%>
    </div>
</asp:Content>
