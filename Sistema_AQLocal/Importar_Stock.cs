using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.Runtime.InteropServices;

namespace Sistema_AQLocal
{
    public partial class Importar_Stock : Form
    {
        private string _alm_id = "";

        const int MF_BYPOSITION = 0x400;
        //cmbAlmacen.ValueMember = "alm_id";

        [DllImport("User32")]

        private static extern int RemoveMenu(IntPtr hMenu, int nPosition, int wFlags);

        [DllImport("User32")]

        private static extern IntPtr GetSystemMenu(IntPtr hWnd, bool bRevert);

        [DllImport("User32")]

        private static extern int GetMenuItemCount(IntPtr hWnd);


        Int32 _valida;
        string _error = "";
        Int32 _boton;
        public Importar_Stock()
        {
            InitializeComponent();
        }

        private void btnmov_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            _boton = 1;
            lblmensaje1.Visible = true;
            lblmensaje2.Visible = false;
            prg1.Visible = true;
            prg1.Style = ProgressBarStyle.Marquee;
            trabajo.RunWorkerAsync();
            btnmov.Enabled = false;
            btnstock.Enabled = false;
            btnsalir.Enabled = false;
            lblmensaje1.BackColor = Color.Khaki;
            lblmensaje1.Text = "Importando Movimientos de Almacen...";           
        }

        private void Importar_Stock_Load(object sender, EventArgs e)
        {
            inicio();
        }       

        private void cargaCombo()
        {
            cmbAlmacen.DataSource = Data_Bata.getAlmacenes().Tables[0];
            cmbAlmacen.DisplayMember = "Alm_Descripcion";
            cmbAlmacen.ValueMember = "Alm_Novell";
            cmbAlmacen.SelectedIndex = -1;
            cmbAlmacen.Focus();

        }
        private void inicio()
        {
            lblmensaje1.Visible = false;
            btnmov.Enabled = false;
            cmbAlmacen.DropDownStyle = ComboBoxStyle.DropDownList;

            prg1.Value = 0;
            prg1.Style = ProgressBarStyle.Blocks;
            lblmensaje1.Visible = false;
            prg2.Value = 0;
            prg2.Style = ProgressBarStyle.Blocks;
            lblmensaje2.Visible = false;

            btnsalir.Enabled = true;
            //deshabilitar cerrar en el form
            IntPtr hMenu = GetSystemMenu(this.Handle, false);

            int menuItemCount = GetMenuItemCount(hMenu);

            RemoveMenu(hMenu, menuItemCount - 1, MF_BYPOSITION);
            cargaCombo();
        }

        private void trabajo_DoWork(object sender, DoWorkEventArgs e)
        {
            trabajo.WorkerReportsProgress = true;
            for (Int32 i = 1; i <= 100; ++i)
            {
                trabajo.ReportProgress(i / 10);
                Thread.Sleep(10);
            }
            if (_boton == 1)
            {
                e.Result = dt_importa_mov();
            }
            else
            {
                e.Result = dt_importa_stk();
            }

        }
        private DataTable dt_importa_stk()
        {
            string _codigo_almacen = _alm_id;
            DataTable dt = Data_Bata.tabla_stock(ref _error, ref _codigo_almacen);

            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    _error = Data_Bata._importar_stk_bata(_alm_id, dt);
                    if (_error.Length == 0)
                    {
                        _valida = 1;
                        //lblmensaje1.BackColor = Color.Khaki;
                        //lblmensaje1.Text = "Se guardaron los movimientos correctamente...";
                        //MessageBox.Show("Se guardaron los movimientos correctamente...  ", Conexion.mensaje, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        _valida = 4;
                        //MessageBox.Show(_error, Conexion.mensaje, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    _valida = 2;
                    //lblmensaje1.BackColor = Color.Khaki;
                    //lblmensaje1.Text = "No hay datos para actualizar...";
                    //MessageBox.Show("No hay datos para actualizar...  ", Conexion.mensaje, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                if (_error.Length == 0)
                {
                    _valida = 3;
                    //MessageBox.Show("No hay datos para importar...  ", Conexion.mensaje, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    _valida = 5;
                    //MessageBox.Show(_error, Conexion.mensaje, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            return dt;
        }
        private DataTable dt_importa_mov()
        {
                        
            DataTable dt = Data_Bata.tabla_movimiento(ref _error);

            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    _error = Data_Bata._importar_mov_bata(dt);
                    if (_error.Length == 0)
                    {
                        _valida = 1;
                        //lblmensaje1.BackColor = Color.Khaki;
                        //lblmensaje1.Text = "Se guardaron los movimientos correctamente...";
                        //MessageBox.Show("Se guardaron los movimientos correctamente...  ", Conexion.mensaje, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        _valida = 4;
                        //MessageBox.Show(_error, Conexion.mensaje, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    _valida = 2;
                    //lblmensaje1.BackColor = Color.Khaki;
                    //lblmensaje1.Text = "No hay datos para actualizar...";
                    //MessageBox.Show("No hay datos para actualizar...  ", Conexion.mensaje, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                if (_error.Length == 0)
                {
                    _valida = 3;
                    //MessageBox.Show("No hay datos para importar...  ", Conexion.mensaje, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    _valida = 5;
                    //MessageBox.Show(_error, Conexion.mensaje, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            return dt;
        }

        private void trabajo_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
             //prg1.Value = e.ProgressPercentage;               
        }

        private void trabajo_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            ProgressBar pr;
            Label lbl;
            if (_boton==1)
            {
                pr = prg1;
                lbl = lblmensaje1;
            }
            else
            {
                pr = prg2;
                lbl = lblmensaje2;
            }

            pr.Value = 0;
            pr.Style = ProgressBarStyle.Blocks;
            //pr.Visible = false;
           
            switch (_valida)
            {
                case 1:
                    lbl.BackColor = Color.Khaki;
                    lbl.Text = "Se guardaron los datos correctamente...";
                    break;
                case 2:
                    lbl.BackColor = Color.Khaki;
                    lbl.Text = "No hay datos para actualizar...";
                     break;
                case 3:
                     lbl.BackColor = Color.Khaki;
                     lbl.Text = "No hay datos para importar...";
                     break;
                case 4:
                     lbl.BackColor = Color.Coral;
                     lbl.Text = _error;
                     break;
                case 5:
                     lbl.BackColor = Color.Coral;
                     lbl.Text = _error;
                     break;
            }
            _valida = 0;
            _error = "";
            btnmov.Enabled = true;
            btnstock.Enabled = true;
            btnsalir.Enabled = true;
            Cursor.Current = Cursors.Default;
        }

        private void btnstock_Click(object sender, EventArgs e)
        {
            //MessageBox.Show("se esta trabajando en este proceso...  ", Conexion.mensaje, MessageBoxButtons.OK, MessageBoxIcon.Information);
            //Cursor.Current = Cursors.WaitCursor;            
            //return;
            _boton = 2;
            lblmensaje2.Visible = true;
            lblmensaje1.Visible = false;
            btnsalir.Enabled = false;
            prg2.Visible = true;
            prg2.Style = ProgressBarStyle.Marquee;
            trabajo.RunWorkerAsync();
            btnmov.Enabled = false;
            btnstock.Enabled = false;
            lblmensaje2.BackColor = Color.Khaki;
            lblmensaje2.Text = "Importando Stock de Almacen...";         
        }

        private void btnsalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cmbAlmacen_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!cmbAlmacen.Focused) return;
            string _cod = cmbAlmacen.SelectedValue.ToString();

            //if (_cod == "01")
            //{
            //    MessageBox.Show("SI SELECCIONA ESTA OPCION [" + dwestado.Text.ToUpper() + "] , ANULARA LA OPERACION DEL DOCUMENTO DE REFERENCIA", Global.mensaje, MessageBoxButtons.OK, MessageBoxIcon.Error);
            //}

        }

        private void cmbAlmacen_SelectedValueChanged(object sender, EventArgs e)
        {
            try
            {
                //if (!(dwcliente.Focused)) return;
                String almSelect = cmbAlmacen.SelectedValue.ToString();
                Cursor.Current = Cursors.WaitCursor;
                /// Verificar que sea una selección valida
                if (almSelect != "-1")
                {
                    _alm_id = almSelect;
                }
            }
            catch
            {
            }
            Cursor.Current = Cursors.Default;

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void lbl1_Click(object sender, EventArgs e)
        {

        }
    }
}
