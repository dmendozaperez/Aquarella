using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Sistema_AQLocal;
//using Impresora_Epson;
namespace Sistema_AQLocal
{
    public partial class Articulo_Stock : Form
    {
        private Ventana menu;
        public Articulo_Stock()
        {
            InitializeComponent();     
        }

        DataTable dt;

        private void btnmov_Click(object sender, EventArgs e)
        {            
            Cursor.Current = Cursors.WaitCursor;
            string _error = "";
            DataTable dt = Data_Bata.tabla_movimiento(ref _error);

            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    _error = Data_Bata._importar_mov_bata(dt);
                    if (_error.Length == 0)
                    {
                        MessageBox.Show("Se guardaron los movimientos correctamente...  ", Conexion.mensaje, MessageBoxButtons.OK, MessageBoxIcon.Information);                        
                    }
                    else
                    {
                        MessageBox.Show(_error, Conexion.mensaje, MessageBoxButtons.OK, MessageBoxIcon.Error);                        
                    }
                }
                else
                {
                    MessageBox.Show("No hay datos para actualizar...  ", Conexion.mensaje, MessageBoxButtons.OK, MessageBoxIcon.Information);                    
                }
            }
            else
            {
                if (_error.Length == 0)
                {
                    MessageBox.Show("No hay datos para importar...  ", Conexion.mensaje, MessageBoxButtons.OK, MessageBoxIcon.Information);                    
                }
                else
                {
                    MessageBox.Show(_error, Conexion.mensaje, MessageBoxButtons.OK, MessageBoxIcon.Error);                    
                }
            }
            Cursor.Current = Cursors.Default;
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }
        
     
    }
}
