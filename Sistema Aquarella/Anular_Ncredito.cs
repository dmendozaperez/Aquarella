using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Sistema_Aquarella;
using Variables;
using Impresora_Epson;
namespace Sistema_Aquarella
{
    public partial class Anular_NCredito : Form
    {
        private Ventana menu;
        public Anular_NCredito()
        {
            InitializeComponent();     
        }

        DataTable dt;
        private void Anular_Documento_Load(object sender, EventArgs e)
        {
            //menu = new Ventana(Basico._form_principal);
            //refrescagrilla();
            inicio();
        }

        private void inicio()
        {
            DateTime fecha = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            dtpinicio.Value = fecha;
            dtpfinal.Value = DateTime.Today;

            txtdocumento.Enabled = false;
            chkactivar.Checked = false;
            consultar();
        }

        private void chkactivar_CheckedChanged(object sender, EventArgs e)
        {
            if (chkactivar.Checked)
            {
                dtpinicio.Enabled = false;
                dtpfinal.Enabled = false;
                txtdocumento.Enabled = true;
                txtdocumento.Focus();
            }
            else
            {
                txtdocumento.Text = "";
                consultar();
                dtpinicio.Enabled = true;
                dtpfinal.Enabled = true;
                txtdocumento.Enabled = false;                
            }
        }


        public void consultar()
        {
            Cursor.Current = Cursors.WaitCursor;
            try
            {
                Boolean _tipo=chkactivar.Checked;

                if (_tipo)
                {
                    if (txtdocumento.Text.Length == 0)
                    {
                        MessageBox.Show("Por favor ingrese el numero de documento a consultar", Global.mensaje, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        txtdocumento.Focus();
                        return;
                    }
                }

                DateTime _fechaini=dtpinicio.Value;
                DateTime _fechafin=dtpfinal.Value;
                string _doc=txtdocumento.Text;
                dg1.AutoGenerateColumns = false;
                dt = NotaCredito_Negocio.dt_consulta_notacredito(_tipo, _fechaini, _fechafin, _doc);

                dg1.DataSource = dt;

                grillaformato(dg1);
                
                //totales(dt);
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message, Global.mensaje, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            Cursor.Current = Cursors.Default;
        }

        private void grillaformato(DataGridView dg)
        {
            //dg.AllowUserToAddRows = false;
            dg.RowHeadersWidth = 22;
            dg.AllowUserToResizeColumns = true;
            dg.Refresh();

        }

        private void btnconsultar_Click(object sender, EventArgs e)
        {
            consultar();
        }

        private void txtdocumento_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
                consultar();
        }

        private void dg1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dg1.Rows.Count == 0) return;
            if (e.RowIndex < 0) return;

            Int32 columna = dg1.CurrentCell.ColumnIndex;

            switch (columna)
            {
                case 6:               
                    decimal _not_id = Convert.ToDecimal(dg1.Rows[e.RowIndex].Cells["Not_Id"].Value.ToString());
                    string _tipo = dg1.Rows[e.RowIndex].Cells["tipodoc"].Value.ToString();
                    string _numdoc = dg1.Rows[e.RowIndex].Cells["numdoc"].Value.ToString();
                    string _doc = dg1.Rows[e.RowIndex].Cells["ven_id"].Value.ToString();
                    Boolean _anulado = Convert.ToBoolean(dg1.Rows[e.RowIndex].Cells["anulado"].Value);
                    //verificar si el documento paso las 72 horas de enviarse a la web service efact
                    Boolean _valida = Convert.ToBoolean(dg1.Rows[e.RowIndex].Cells["docu_vencido"].Value);

                    string _not_numero = dg1.Rows[e.RowIndex].Cells["not_numero"].Value.ToString();

                    if (_anulado)
                    {
                        MessageBox.Show("!El documento de tipo : " + _tipo + " con numero : " + _numdoc + " no se puede anular, porque YA ESTA ANULADO...", Global.mensaje, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }

                    if (_valida)
                    {
                        MessageBox.Show("!El documento de tipo : " + _tipo + " con numero : " + _numdoc + " no se puede anular, porque no es de la fecha actual...", Global.mensaje, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }

                    Cursor.Current = Cursors.WaitCursor;
                    DialogResult resulado = MessageBox.Show("¿Realmente desea anular el documento de tipo : " + _tipo + " con numero : " + _numdoc,
                             Global.mensaje, MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                    if (resulado == DialogResult.OK)
                    {
                        string _error = "";
                                                
                            //Facturacion_Electronica.anular_facturacion_electronica(_doc, ref _error,"NC");
                                

                            if (_error.Length == 0)
                            {
                                string _error_venta = Venta._anular_ncredito(_not_id, Global._bas_id_codigo);
                                string _codigo_hashn = "";
                                Facturacion_Electronica.ejecutar_factura_electronica("N", _not_id.ToString(), ref _codigo_hashn, ref _error);


                                if (_error.Length==0)
                                {
                                    Basico._enviar_webservice_xml();
                                    MessageBox.Show("!El documento de tipo : " + _tipo + " con numero : " + _numdoc + " Se Anulo con exito...", Global.mensaje, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    consultar();

                                }
                                else
                                {
                                    MessageBox.Show(_error_venta, Global.mensaje, MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                                
                            }
                            else
                            {
                                MessageBox.Show(_error, Global.mensaje, MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }                        
                       
                    }
                    //}
                    Cursor.Current = Cursors.Default;

                    break;
                case 7:                    
                    //MessageBox.Show("CONSULTE CON SISTEMAS...", Global.mensaje, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    string _codigo_hash = dg1.Rows[e.RowIndex].Cells["Not_Cod_Hash"].Value.ToString();;
                    decimal _not_id_imp = Convert.ToDecimal(dg1.Rows[e.RowIndex].Cells["Not_Id"].Value.ToString());
                    string _numdoc_r=dg1.Rows[e.RowIndex].Cells["numdoc"].Value.ToString();;
                     Cursor.Current = Cursors.WaitCursor;
                    DialogResult resulado_imp = MessageBox.Show("¿Realmente desea REIMPRIMIR el documento de tipo : Nota de Credito  con numero : " + _numdoc_r,
                             Global.mensaje, MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                    if (resulado_imp == DialogResult.OK)
                    {
                        //string _genera_tk = Impresora_Epson.Config_Imp_NC.GenerarTicketNC(_not_id_imp.ToString(), 1, _codigo_hash);

                        //if (_genera_tk == null)
                        //{                            
                        //    MessageBox.Show(" >> Se producjo un error en la impresión del ticket", Global.mensaje, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        //}                       
                                
                    }
                    Cursor.Current = Cursors.Default;

                    
                    //string tickets = Config_Imp.GenerarTicketFact(txtcomprobante.Text, 1, _codigo_hash);
                    break;
            }

        }       
    }
}
