<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ucConfigLiq.ascx.cs"
    Inherits="www.aquarella.com.pe.UserControl.ucConfigLiq" %>
<script type="text/javascript" language="javascript">
    $(document).ready(function () {
        lambda();
    });
    function lambda() {
        var value = $("input:radio[name$='rbTypeLiq']:checked").val()
        if (value == '1') {
            $("input[id$='chkbInfoShipp']").attr("disabled", false); //.removeAttr("disabled");
            $('#spShipp').css('text-decoration', 'none');
        }
        else {
            if ($("input[id$='chkbInfoShipp']").is(':checked')) {
                $("input[id$='chkbInfoShipp']").click();
            }
            $('#spShipp').css('text-decoration', 'line-through');
            $("input[id$='chkbInfoShipp']").attr("disabled", true);
        }
    }
</script>
<table width="100%">
    <tr>
        <td align="left">
            <asp:CheckBox ToolTip="Si desea cambiar el sitio o lugar de entrega de la nueva liquidación, entonces asegúrese de tener chequeado este campo."
                ID="chkbInfoShipp" runat="server" />
        </td>
        <td colspan="4">
            <span id="spShipp"><b>Deseo configurar una información de entrega ó envío;</b> ya que
                quiero enviar el pedido a un destinatario diferente a mi residencia actualmente
                configurada. </span>
        </td>
    </tr>
</table>
<table>
    <tr>
        <td>
            <asp:RadioButton ID="rbTypePS" GroupName="rbTypeLiq" runat="server" value="1" onClick="javascript:lambda();"
                Checked="true" />
        </td>
        <td>
            <b>Envíenlo a mi residenca</b>
        </td>
        <td>
            <asp:RadioButton ID="rbTypePRCS" GroupName="rbTypeLiq" runat="server" value="2" onClick="javascript:lambda();" />
        </td>
        <td>
            <b>Iré personalmente</b>
        </td>
        <td>
            | Seleccione o escoja la forma por medio de la cual se le entregara su pedido.
        </td>
    </tr>
</table>
