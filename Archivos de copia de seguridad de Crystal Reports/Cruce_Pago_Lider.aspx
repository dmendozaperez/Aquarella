﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Design/Site.Master" AutoEventWireup="true" CodeBehind="Cruce_Pago_Lider.aspx.cs" Inherits="www.aquarella.com.pe.Aquarella.Financiera.Cruce_Pago_Lider" %>
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
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderTitle" runat="server">
    Cruce de pagos y liquidaciones
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderPageDesc" runat="server">
    Por favor seleccione las liquidaciones y los pagos con los cuales va a cruzar.&nbsp;
    El sistema no permite grabar hasta que el "Gran Total" sea un número mayor a cero
    ó sus pagos superen el valor de sus pedidos o deudas.
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
            <asp:AsyncPostBackTrigger ControlID="btCreateClear" EventName="click" />
            <asp:AsyncPostBackTrigger ControlID="dwCustomers" EventName="SelectedIndexChanged" />
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
    <asp:Panel ID="pnlDwCustomers" Visible="false" runat="server">
        <div style="margin: 10px auto 0 auto;">
            <table width="100%" class="tablagris" cellpadding="4">
                <tr>
                    <td class="fsal f13" colspan="2">
                        Selección el lider
                    </td>
                </tr>
                <tr>
                    <td>
                        <table width="100%" cellpadding="1" cellspacing="1">
                            <tr>
                                <td class="f12" width="60%">
                                    Seleccione un lider de la lista:<br />
                                    (Desc: Seleccione un lider sobre el cual realizará las acciones.)
                                </td>
                                <td>
                                    <asp:DropDownList ID="dwCustomers" AutoPostBack="true" DataTextField="Are_Descripcion"
                                        DataValueField="Are_Id" AppendDataBoundItems="true" runat="server"
                                        ToolTip="Selecionar un cliente" Width="220px" OnSelectedIndexChanged="dwCustomers_SelectedIndexChanged">
                                        <asp:ListItem Value="-1" Text=" -- Seleccione de la Lista --"></asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                        </table>
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
                    OnDataBound="gvClear_DataBound" Width="80%" AutoGenerateColumns="False" 
                    CellPadding="4" ForeColor="#333333" GridLines="None">
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
                        <asp:BoundField DataField="Cliente" HeaderText="Lider/Promotor">
                        <HeaderStyle HorizontalAlign="Left" />
                        <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="pedido" HeaderText="Pedido">
                        <HeaderStyle HorizontalAlign="Left" />
                        <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="document_date_desc" HeaderText="Fecha" SortExpression="dtd_document_date"
                            ReadOnly="True" >
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
                <asp:AsyncPostBackTrigger ControlID="btCreateClear" EventName="click" />
                <asp:AsyncPostBackTrigger ControlID="dwCustomers" EventName="SelectedIndexChanged" />
            </Triggers>
        </asp:UpdatePanel>
        <asp:ObjectDataSource ID="odsClear" runat="server" SelectMethod="get_DocTranLiquiByLider"
            TypeName="www.aquarella.com.pe.bll.Documents_Trans" OnSelected="odsClear_Selected">
            <SelectParameters>
                <asp:Parameter Name="_idCust" Type="Int32" />
            </SelectParameters>
        </asp:ObjectDataSource>
        <ajaxToolkit:UpdatePanelAnimationExtender runat="server" ID="upae" TargetControlID="upGridClear">
            <Animations>
                    <OnUpdating>
                        <Sequence>
                            <%-- Disable all the controls --%>
                            <Parallel duration="0">
                                <EnableAction AnimationTarget="btCreateClear" Enabled="false" />
                                <EnableAction AnimationTarget="dwCustomers" Enabled="false" />
                            </Parallel>
                        </Sequence>
                    </OnUpdating>
                    <OnUpdated>
                        <Sequence>
                            <%-- Enable all the controls --%>
                            <Parallel duration="0">
                                <EnableAction AnimationTarget="dwCustomers" Enabled="true" />
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
