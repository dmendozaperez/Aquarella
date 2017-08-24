<%@ Page Title="" Language="C#" MasterPageFile="~/Design/Site.Master" AutoEventWireup="true" CodeBehind="Relacion_Usuario_Asesor.aspx.cs" Inherits="www.aquarella.com.pe.Aquarella.Admonred.Relacion_Usuario_Asesor" %>
<asp:Content ID="Content1" ContentPlaceHolderID="headCPH" runat="server">
    <style type="text/css">


        .style1
        {
            width: 46px;
        }
        .style2
        {
            width: 273px;
        }
        .style3
        {
            width: 46px;
            height: 42px;
        }
        .style4
        {
            width: 273px;
            height: 42px;
        }
        .style5
        {
            height: 42px;
        }
        .auto-style1 {
            width: 55px;
            height: 42px;
        }
        .auto-style2 {
            width: 55px;
        }
         .ColumnaOculta {display:none;}
        .special.ui-state-default {
            background-color: #3173a5;
            background-image: none;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderTitle" runat="server">
     Relacionar Lider-Asesor Comercial
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderPageDesc" runat="server">
    &nbsp;Esta ventana actualizara la relacion del Lider-Asesor Comercial 
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <p id="validateTips">
    </p>
      <asp:ScriptManager ID="ScriptManager1" runat="server">
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
     <table style="width: 100%;">
        <tr>
            <td class="auto-style1">
                Asesor Comercial</td>
            <td class="style4">
                                    <asp:UpdatePanel ID="upGrid" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <asp:DropDownList ID="dwasesor" runat="server" AppendDataBoundItems="true" 
                                                DataTextField="nombres" DataValueField="Bas_id" 
                                                ToolTip="Selecionar un Asesor" Width="280px" AutoPostBack="True" OnSelectedIndexChanged="dwasesor_SelectedIndexChanged">
                                                <asp:ListItem Text=" -- Seleccionar un Asesor --" Value="-1"></asp:ListItem>
                                            </asp:DropDownList>
                                        </ContentTemplate>                                        
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="btnaceptar" EventName="Click" />
                                        </Triggers>
                                    </asp:UpdatePanel>
                                </td>
            <td class="style5">
                </td>
            <td class="style5">
                &nbsp;</td>
        </tr>
        <tr>
            <td class="auto-style1">
                Lider</td>
            <td class="style4">
                                     <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <asp:DropDownList ID="dwlider" runat="server" AppendDataBoundItems="true" 
                                                DataTextField="nombres" DataValueField="Bas_id" 
                                                ToolTip="Selecionar un Lider" Width="280px" AutoPostBack="true" OnSelectedIndexChanged="dwlider_SelectedIndexChanged">
                                                <asp:ListItem Text=" -- Seleccionar un Lider --" Value="-1"></asp:ListItem>
                                            </asp:DropDownList>
                                        </ContentTemplate>                                       
                                         <Triggers>
                                             <asp:AsyncPostBackTrigger ControlID="btnaceptar" EventName="Click" />
                                             <asp:AsyncPostBackTrigger ControlID="gvrel_lider" EventName="RowCommand" />
                                         </Triggers>
                                    </asp:UpdatePanel>

            </td>
            <td class="style5">
                 <asp:Button ID="btnaceptar" runat="server" Text="Agregar" SortExpression="lider" CausesValidation="true"
                    onclick="btnaceptar_Click" /></td>
            <td>
                    &nbsp;</td>
        </tr>
        <tr>
            <td class="auto-style2">
                &nbsp;</td>
            <td class="style2">
                Si usted desea que este Lider se relacione el Asesor Comercial, por favor dale 
                click en Agregar</td>
            <td>
               
                    <asp:ImageButton ID="ibExportToExcel" ImageUrl="~/Design/images/Botones/b_toExcel.png"
                        onmouseover="this.style.background='green';" onmouseout="this.style.background=''"
                        runat="server" Height="25px" Width="24px" 
                        ToolTip="Exportar Panel de Resultados a Excel." 
                        onclick="ibExportToExcel_Click" />
               
            </td>
            <td>
               
                &nbsp;</td>
        </tr>
        <tr>
            <td colspan="3">
                <asp:UpdatePanel ID="uplider" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <asp:GridView ID="gvrel_lider" runat="server" Width="98%" SkinID="gridviewSkin"
                                AutoGenerateColumns="False"  Font-Size="8pt" AllowPaging="True" AllowSorting="True" CellPadding="3" ShowHeaderWhenEmpty="True" BackColor="White" BorderColor="#CCCCCC" BorderStyle="Solid" BorderWidth="1px" OnRowCommand="gvrel_lider_RowCommand" OnRowDataBound="gvrel_lider_RowDataBound" OnPageIndexChanging="gvrel_lider_PageIndexChanging" PageSize="14" OnRowCreated="gvrel_lider_RowCreated" >
                              <%--  <EmptyDataTemplate>
                                    No existen manifiesto que mostrar.
                                </EmptyDataTemplate>--%>
                                <Columns>
                                     <asp:BoundField DataField="asesor"
                                        HeaderText="Asesor" ItemStyle-HorizontalAlign="Center" ShowHeader="false" ItemStyle-CssClass="ColumnaOculta" HeaderStyle-CssClass="ColumnaOculta">
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle HorizontalAlign="Left" />                                        
                                    </asp:BoundField>
                                    <asp:BoundField DataField="documento"
                                        HeaderText="Documento" ItemStyle-HorizontalAlign="Center">
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:BoundField>
                                    <asp:TemplateField HeaderText="Lider">
                                        <EditItemTemplate>
                                            <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("lider") %>'></asp:TextBox>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lbllider" runat="server" Text='<%# Bind("lider") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="direccion" HeaderText="Direccion" >
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle HorizontalAlign="Left" />
                                    </asp:BoundField>
                                      <asp:BoundField DataField="telefono" HeaderText="Telefono" />
                                    <asp:BoundField DataField="correo" HeaderText="Correo" />
                                       <asp:TemplateField HeaderText="Eliminar" ItemStyle-HorizontalAlign="Center" >
                            <ItemTemplate>
                                            <asp:ImageButton ID="ibanular" CommandName="starnular"  CommandArgument='<%# Eval("id")%>'
                                                  Visible="true" runat="server" ImageUrl="~/Design/images/Botones/delete_off.png" ToolTip="Eliminar registro.." BorderWidth="0" />
                      
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
                            <asp:AsyncPostBackTrigger ControlID="btnaceptar" EventName="Click" />
                            <asp:AsyncPostBackTrigger ControlID="dwasesor" EventName="SelectedIndexChanged" />
                            <asp:AsyncPostBackTrigger ControlID="dwlider" EventName="SelectedIndexChanged" />
                            <asp:AsyncPostBackTrigger ControlID="gvrel_lider" EventName="PageIndexChanging" />
                            <asp:AsyncPostBackTrigger ControlID="gvrel_lider" EventName="RowCommand" />
                        </Triggers>
                    </asp:UpdatePanel>
            </td>
            <td>
                &nbsp;</td>
        </tr>
           <!-- ACTIVITY PANEL -->
        <ajaxToolkit:UpdatePanelAnimationExtender ID="upae" runat="server" TargetControlID="uplider">
            <Animations>
                                        <OnUpdating>
                                            <Sequence>
                                             
                                                <Parallel duration="0">
                                                    <EnableAction AnimationTarget="btnaceptar" Enabled="false" />                                                   
                                                </Parallel>
                                            </Sequence>
                                        </OnUpdating>
                                        <OnUpdated>
                                            <Sequence>
                                             
                                                <Parallel duration="0">
                                                    <EnableAction AnimationTarget="btnaceptar" Enabled="true" />
                                                </Parallel>
                                            </Sequence>
                                        </OnUpdated>
            </Animations>
        </ajaxToolkit:UpdatePanelAnimationExtender>
    </table>
</asp:Content>
