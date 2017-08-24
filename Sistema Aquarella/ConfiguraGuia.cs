using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Sistema_Aquarella;


namespace Sistema_Aquarella
{
    public partial class ConfiguraGuia : Form
    {
       
        public ConfiguraGuia()
        {
            InitializeComponent();
        }       
        private void ConfiguraGuia_Load(object sender, EventArgs e)
        {
            defecto();
        }
        private void llenarcombo()
        {
            dwtransportador.DataSource = Basico.leertrasnportador();
            dwtransportador.DisplayMember = "Tra_Descripcion";
            dwtransportador.ValueMember = "Tra_id";
        }
        private void defecto()
        {
            llenarcombo();
            dwtransportador.SelectedIndex = -1;
            lblliq.Text = _liq;
            lblcliente.Text=_cliente;
            txtguia.Text = Basico.guiasecuencia();
        }
        private void ConfiguraGuia_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape) this.Close();            
        }

        private void cmdcancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        //propiedades para datos de la liquidacion
        private static string liq;
        private static string cliente;

        public static string _liq
        {
            set { liq = value; }
            get { return liq; }
        }
        public static string _cliente
        {
            set { cliente = value; }
            get { return cliente; }
        }

        private void cmdaceptar_Click(object sender, EventArgs e)
        {
            if (!(fvalida())) return;
            Int32 _valida_guia;
            Int32 _idtrans = Convert.ToInt32(dwtransportador.SelectedValue);
            string _gui_no=txtguia.Text;
            Cursor.Current = Cursors.WaitCursor;
            Liquidacion.insertar_guia(_gui_no, _idtrans, _liq,out _valida_guia );

            if (_valida_guia == 0)
            {

                IPanelPedidos forminterface = this.Owner as IPanelPedidos;
                if (forminterface != null)
                    forminterface.refrescagrilla();
                this.Close();
                                               
            }
            else
            {
                lblmensaje.Text = "Es probable que la guia ya exista, por favor dígite un nuevo numero de guia.";
                txtguia.Focus();
            }
            Cursor.Current = Cursors.Default;
        }

        private Boolean fvalida()
        {
            Boolean valida = true;
            lblmensaje.Text = "";
            if (Convert.ToString(dwtransportador.SelectedValue) == "")
            {
                lblmensaje.Text = "Por Favor, seleccione la transportadora";
                valida = false;
                dwtransportador.Focus();
                return valida; ;
            }
            if (txtguia.Text.Length == 0)
            {
                lblmensaje.Text = "Por favor dígite el numero de guia.";
                valida = false;
                txtguia.Focus();
                return valida;
            }



            return valida;
        }

        private void txtguia_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(Char.IsDigit(e.KeyChar)) && !(e.KeyChar == 8))
            {
                
                e.Handled = true;
            }
        }
    }
}
