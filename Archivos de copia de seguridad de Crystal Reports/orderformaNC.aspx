<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="orderformaNC.aspx.cs" Inherits="www.aquarella.com.pe.Aquarella.Logistica.orderformaNC" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <asp:ScriptManager ID="ScriptManager1" runat="server" AsyncPostBackTimeout="999999">
    </asp:ScriptManager>
     <asp:UpdatePanel ID="upPanelMsg" runat="server">
        <ContentTemplate>
            <AQControl:Message runat="server" Visible="false" ID="msnMessage"></AQControl:Message>
        </ContentTemplate>
    </asp:UpdatePanel>
        <fieldset> 
      <table style="width:100%;">
          <tr>
              <td>
              <asp:UpdatePanel ID="upGridnc" runat="server">
                      <ContentTemplate>
                          <asp:GridView ID="gvnc" runat="server" CellPadding="4" HorizontalAlign="Center" 
                              ShowFooter="True" SkinID="gridviewSkin" 
                              Width="99%" Font-Bold="False" Height="275px" AutoGenerateColumns="False" 
                              PageSize="7" OnRowDataBound="gvnc_RowDataBound" 
                              OnDataBound="gvnc_DataBound" BackColor="White" BorderColor="#3366CC" 
                              BorderStyle="None" BorderWidth="1px">
                              <Columns>
                                  <asp:TemplateField>
                                      <ItemTemplate>
                                          <asp:CheckBox ID="chkDocument" runat="server" OnCheckedChanged="chkDocument_CheckedChanged"
                                             AutoPostBack="true" ToolTip='<%# Eval("ncredito")%>' />
                                      </ItemTemplate>
                                  </asp:TemplateField>
                                  <asp:BoundField DataField="ncredito" HeaderText="Numero" ReadOnly="True"
                                  SortExpression="ncredito" FooterStyle-CssClass="f13" FooterStyle-Font-Bold="true"
                                  FooterText="Gran Total : " FooterStyle-HorizontalAlign="Right" >
                                  <FooterStyle CssClass="f13" Font-Bold="True" HorizontalAlign="Right" />
                                  <HeaderStyle HorizontalAlign="Left" />
                                  <ItemStyle HorizontalAlign="Left" />
                                  </asp:BoundField>
                                  <asp:BoundField DataField="importe" DataFormatString="{0:C2}" 
                                      HeaderText="Importe">
                                  <HeaderStyle HorizontalAlign="Right" />
                                  <ItemStyle HorizontalAlign="Right" />
                                  </asp:BoundField>
                              </Columns>
                              <EmptyDataTemplate>
                                  No posee ninguna Nota de credito.
                              </EmptyDataTemplate>
                              <FooterStyle BackColor="#99CCCC" ForeColor="#003399" />
                              <HeaderStyle BackColor="#003399" Font-Bold="True" ForeColor="#CCCCFF" />
                              <PagerStyle BackColor="#99CCCC" ForeColor="#003399" HorizontalAlign="Left" />
                              <RowStyle BackColor="White" ForeColor="#003399" />
                              <SelectedRowStyle BackColor="#009999" Font-Bold="True" ForeColor="#CCFF99" />
                              <SortedAscendingCellStyle BackColor="#EDF6F6" />
                              <SortedAscendingHeaderStyle BackColor="#0D4AC4" />
                              <SortedDescendingCellStyle BackColor="#D6DFDF" />
                              <SortedDescendingHeaderStyle BackColor="#002876" />
                          </asp:GridView>
                          <asp:ObjectDataSource ID="odsnc" runat="server" OnSelected="odsnc_Selected" 
                              SelectMethod="get_PagoNcredito" TypeName="www.aquarella.com.pe.bll.Documents_Trans">
                              <SelectParameters>
                                  <asp:Parameter Name="_bas_id" Type="String" /> 
                                  <asp:Parameter Name="_idliq" Type="String"/>
                              </SelectParameters>
                          </asp:ObjectDataSource>
                      </ContentTemplate>
                      </asp:UpdatePanel>
              </td>
          </tr>
          </table>
 </fieldset>
    </div>
            <asp:HiddenField ID="hdCreditValue" Value="0" runat="server" />
    </form>
</body>
</html>
