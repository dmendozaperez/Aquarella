using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using www.aquarella.com.pe.bll;
using System.Data;
namespace www.aquarella.com.pe.Aquarella.Logistica
{
    public partial class Importar_Mov_Bata : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void ibrecalcular_Click(object sender, ImageClickEventArgs e)
        {
            msnMessage.Visible = false;
            string _error="";
            DataTable dt = Data_Bata.tabla_movimiento(ref _error);

            if (dt!=null)
            {
                if (dt.Rows.Count>0)
                { 
                    _error = Data_Bata._importar_mov_bata(dt);
                    if (_error.Length==0)
                    {
                        msnMessage.LoadMessage("Se los movimientos correctamente...  ", UserControl.ucMessage.MessageType.Information);
                    }
                    else
                    {
                        msnMessage.LoadMessage(_error, UserControl.ucMessage.MessageType.Error);
                    }
                }
                else
                {
                    msnMessage.LoadMessage("No hay datos para actualizar...  ", UserControl.ucMessage.MessageType.Information);
                }
            }
            else
            {
                if (_error.Length==0)
                {
                    msnMessage.LoadMessage("No hay datos para importar...  ", UserControl.ucMessage.MessageType.Information);
                }
                else
                {
                    msnMessage.LoadMessage(_error, UserControl.ucMessage.MessageType.Error);
                }
            }
            //if (_mensaje.Length == 0)
            //{
            //    msnMessage.LoadMessage("Se Recalculo el stock correctamente...  ", UserControl.ucMessage.MessageType.Information);
            //}
            //else
            //    msnMessage.LoadMessage(_mensaje, UserControl.ucMessage.MessageType.Error);
        }
    }
}