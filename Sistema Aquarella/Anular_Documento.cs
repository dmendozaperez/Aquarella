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
    public partial class Anular_Documento : Form
    {
        private Ventana menu;
        public Anular_Documento()
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
                dt = Venta.dt_consulta_venta(_tipo, _fechaini, _fechafin, _doc);

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
                    string _not_id = dg1.Rows[e.RowIndex].Cells["ven_id"].Value.ToString();
                    string _tipo = dg1.Rows[e.RowIndex].Cells["tipodoc"].Value.ToString();
                    string _numdoc = dg1.Rows[e.RowIndex].Cells["numdoc"].Value.ToString();
                    string _doc = dg1.Rows[e.RowIndex].Cells["ven_id"].Value.ToString();
                    Boolean _anulado = Convert.ToBoolean(dg1.Rows[e.RowIndex].Cells["anulado"].Value);
                    //verificar si el documento paso las 72 horas de enviarse a la web service efact
                    Boolean _valida = Convert.ToBoolean(dg1.Rows[e.RowIndex].Cells["docu_vencido"].Value);

                    //string _not_numero = dg1.Rows[e.RowIndex].Cells["not_numero"].Value.ToString();

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
                                string _error_venta = Venta._anular_venta(_not_id.ToString());
                                string _codigo_hashn = "";
                                Facturacion_Electronica.ejecutar_factura_electronica("F", _not_id.ToString(), ref _codigo_hashn, ref _error);


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
                //    string liq = dg1.Rows[e.RowIndex].Cells["liqno"].Value.ToString();
                //    string cliente = dg1.Rows[e.RowIndex].Cells["nombre"].Value.ToString();
                //    ConfiguraGuia._liq = liq;
                //    ConfiguraGuia._cliente = cliente;
                //    ConfiguraGuia frm = new ConfiguraGuia() { Owner = this };
                //    frm.ShowDialog();
                //    break;
                //case 12:
                    //string _guia = dg1.Rows[e.RowIndex].Cells["Guia"].Value.ToString();
                    //string _liq = dg1.Rows[e.RowIndex].Cells["liqno"].Value.ToString();
                    //string _cliente = dg1.Rows[e.RowIndex].Cells["Nombre"].Value.ToString();
                    //if (_guia.Length == 0)
                    //{

                    //    MessageBox.Show("Asigne una guia a la liquidación No. " + _liq + " antes de proceder al empacado.", Global.mensaje, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);


                    //}
                    //else
                    //{
                    //string _tipo = dg1.Rows[e.RowIndex].Cells["tipodoc"].Value.ToString();
                    //string _numdoc = dg1.Rows[e.RowIndex].Cells["numdoc"].Value.ToString();
                    //string _doc = dg1.Rows[e.RowIndex].Cells["ven_id"].Value.ToString();
                    //Boolean _anulado=Convert.ToBoolean(dg1.Rows[e.RowIndex].Cells["anulado"].Value);
                    //Boolean _doc_valida = Convert.ToBoolean(dg1.Rows[e.RowIndex].Cells["doc_valida"].Value);
                    ////verificar si el documento paso las 72 horas de enviarse a la web service efact
                    //Boolean _valida = Convert.ToBoolean(dg1.Rows[e.RowIndex].Cells["docu_vencido"].Value);

                    //if (_anulado)
                    //{
                    //    MessageBox.Show("!El documento de tipo : " + _tipo + " con numero : " + _numdoc + " no se puede anular, porque YA ESTA ANULADO...", Global.mensaje, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    //    return;
                    //}

                    //if (_valida)
                    //{
                    //    MessageBox.Show("!El documento de tipo : " + _tipo + " con numero : " + _numdoc + " no se puede anular, porque se vencio el plazo de 72 horas del envio del documento...", Global.mensaje, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    //    return;
                    //}

                    //if (_doc_valida)
                    //{
                    //    MessageBox.Show("!El documento de tipo : " + _tipo + " con numero : " + _numdoc + " no se puede anular, porque esta relacionado con una nota de credito...", Global.mensaje, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    //    return;
                    //}

                    //Cursor.Current = Cursors.WaitCursor;
                    //DialogResult resulado = MessageBox.Show("¿Realmente desea anular el documento de tipo : " + _tipo + " con numero : " + _numdoc,
                    //         Global.mensaje, MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                    //if (resulado == DialogResult.OK)
                    //{
                    //        string _error = "";// Venta._anular_venta(_doc);                         
                    //        Facturacion_Electronica.anular_facturacion_electronica(_doc, ref _error);                            
                    //        if (_error.Length==0)
                    //        {
                    //            string _error_grabar = Venta._anular_venta(_doc);
                    //            if (_error_grabar.Length == 0)
                    //            { 
                    //                MessageBox.Show("!El documento de tipo : " + _tipo + " con numero : " + _numdoc + " Se Anulo con exito...", Global.mensaje, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //                consultar();
                    //            }
                    //            else
                    //            {
                    //                MessageBox.Show(_error_grabar, Global.mensaje, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    //            }
                    //        }
                    //        else
                    //        {
                    //            MessageBox.Show(_error, Global.mensaje, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    //        }                                                                           
                    //}
                    ////}
                    //Cursor.Current = Cursors.Default;

                    break;
                case 7:
                     string _tipo_r = dg1.Rows[e.RowIndex].Cells["tipodoc"].Value.ToString();
                    string _numdoc_r = dg1.Rows[e.RowIndex].Cells["numdoc"].Value.ToString();
                    string _doc_r = dg1.Rows[e.RowIndex].Cells["ven_id"].Value.ToString();
                    string _cod_hash = dg1.Rows[e.RowIndex].Cells["cod_hash"].Value.ToString();
                     Cursor.Current = Cursors.WaitCursor;
                    DialogResult resulado_imp = MessageBox.Show("¿Realmente desea REIMPRIMIR el documento de tipo : " + _tipo_r + " con numero : " + _numdoc_r,
                             Global.mensaje, MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                    if (resulado_imp == DialogResult.OK)
                    {
                        //string tickets = Config_Imp.GenerarTicketFact(_doc_r, 1, _cod_hash);

                        //if (tickets == null)
                        //{                            
                        //    MessageBox.Show(" >> Se producjo un error en la impresión del ticket", Global.mensaje, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        //}                       
                                
                    }
                    Cursor.Current = Cursors.Default;
                    break;
            }

        }  
     
    }
}
