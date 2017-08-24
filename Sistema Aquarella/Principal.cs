using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Impresora_Epson;
using Sistema_Aquarella;
using Variables;
using System.IO;
namespace Sistema_Aquarella
{
    public partial class Principal : Form, Sistema_Aquarella.IPrincipal
    {
        private Ventana menu;
        public Principal()
        {
            InitializeComponent();
        }

        private void Principal_Load(object sender, EventArgs e)
        {
           // Permisos.SetPermissions();
            //Basico.crear_archivo_properties();
            //string _efact = Path.GetDirectoryName(Application.ExecutablePath).ToString() + "/Efact";
            //MessageBox.Show(_efact);

            //verificar si es que los archivos de configuracion existe del app.config

            //if  Basico._cerificado
            string _error="";
            Basico.copiar_archivo_config(ref _error);
            if (_error.Length>0)
            {
                MessageBox.Show(_error + "==>> CONSULTE CON SISTEMA ACERCA DEL ERROR....", Global.mensaje, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }



            Reloj.Start();
            menu = new Ventana(this);
            Basico._form_principal = this;

            //MessageBox.Show(Path.GetDirectoryName(Application.ExecutablePath).ToString());
            //Config_Imp.GenerarTicketFact("",2);
            //Config_Imp.Mainimp();
        }

        public void lnk_opcion(string valor,Int32 cambio)
        {
            lnkinicio.Text = valor;
            lnkinicio.Tag = cambio;
        }

        private void Reloj_Tick(object sender, EventArgs e)
        {
            lblreloj.Text = DateTime.Now.ToLongTimeString();
            if (Global._form_activo == 1)
            {
                lnkinicio.Text = "Volver";                
            }
            else
            {
                lnkinicio.Text = "Inicio";
            }
        }

        private void btnsession_Click(object sender, EventArgs e)
        {
            iniciasession();
        }
        private bool validainicio()
        {
            bool valida=true;
            if (txtusuario.TextLength == 0) 
            {
                MessageBox.Show("Ingrese el usuario...", Global.mensaje, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                valida=false;
                txtusuario.Focus();
                return valida;
            }
            if (txtcontraseña.TextLength == 0)
            {
                MessageBox.Show("Ingrese la contraseña...", Global.mensaje, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                valida = false;
                txtcontraseña.Focus();
                return valida;
            }
            return valida;
        }
        private void activadesctiva(Boolean valida)
        {
            switch  (valida)
                {
                case true:
                        btnanulardoc.Enabled = true;
                        btnanularnc.Enabled = true;
                        //btndcomprobante.Enabled = true;
                        btndguia.Enabled = true;
                        btnpedidos.Enabled = true;
                        btnc.Enabled = true;
                        lnksession.Enabled = true;
                        pnlinicio.Visible = false;
                        Calendario.Visible = true;
                    
                        break;
                case false:
                        btnanulardoc.Enabled = false;
                        btnanularnc.Enabled = false;
                        btndcomprobante.Enabled = false;
                        btndguia.Enabled =false;
                        btnpedidos.Enabled = false;
                        btnc.Enabled = false;
                        lnksession.Enabled =false;
                        pnlinicio.Visible = true;
                        Calendario.Visible = false;
                        lblnombres.Text = "";
                        break;
                }
                 
              
        }
        private void iniciasession()
        {
            try
            {
                if (!(validainicio()))  return;
                string usuario = txtusuario.Text;
                string contraseña = txtcontraseña.Text;
                Usuario user = new Usuario();
                user = cargardatos(usuario);

                if (user != null)
                {
                    if (user._usu_est_id.Equals(Constantes.IdStatusActive))
                    {
                        string con_usu = Cryptographic.decrypt(user._usu_contraseña);
                        if (contraseña.Equals(con_usu))
                        {
                            lblnombres.Text = "Usuario | " + user._nombre;
                            activadesctiva(true);
                            
                        }
                        else
                        {
                            MessageBox.Show("Usuario y/o contraseña invalidos.", Global.mensaje, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Validaccion del usuario fallo.", Global.mensaje, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message,Global.mensaje,MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
        }
        private Usuario cargardatos(string nombre)
        {
            DataTable dtusuario = Acceso.F_LeerUsuario(nombre);
            if (dtusuario == null || dtusuario.Rows.Count <= 0)
            {
                return null;
            }
            DataRow dr = dtusuario.Rows[0];
            Global._bas_id_codigo = Convert.ToInt32(dr["bas_id"].ToString());
            Usuario u = new Usuario
            {
                _bas_id = Convert.ToInt32(dr["bas_id"].ToString()),
                _usu_nombre = dr["usu_nombre"].ToString(),
                _usu_contraseña = dr["usu_contraseña"].ToString(),
                _usu_est_id = dr["usu_est_id"].ToString(),
                _nombre = dr["nombre"].ToString(),
                _usu_tip_id = dr["usu_tip_id"].ToString(),
                _usu_tip_nombre = dr["usu_tip_nombre"].ToString(),
                _usv_area = dr["bas_Are_id"].ToString(),
                _usn_userid = Convert.ToInt32(dr["bas_id"].ToString()),
                _usv_username = dr["usu_nombre"].ToString(),

                //_usn_userid = Convert.ToDecimal(dr["usn_userid"]),
                //_usv_co = dr["usv_co"].ToString(),
                //_usv_employee = isEmployee,
                //_usv_customer = isCustomer,
                //_usv_answer = dr["usv_answer"].ToString(),
                //_usv_question = dr["usv_question"].ToString(),
                //_usv_status = dr["usv_status"].ToString(),
                //_usv_username = dr["usv_username"].ToString(),
                //_usv_name = dr["name"].ToString(),
                _usd_creation = System.DateTime.Parse(dr["usu_fecha_cre"].ToString()),
                //_usv_warehouse = wareId,
                //_usv_warehouse_name = wareDesc,
                //_usv_area = areaId,
                //_usv_area_name = areaDesc,
                //_usv_region = regionId,
                //_usv_nivel = nivel,
                //_usv_password = dr["usv_password"].ToString(), 
                //_attempts = int.Parse(dr["usn_failedattemptcount"].ToString()),
                //_usv_postpago = dr["postpago"].ToString()
                _usv_postpago = dr["postpago"].ToString()
            };

            return u;
        }

        private void btncancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtcontraseña_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                Cursor.Current = Cursors.WaitCursor;
                e.Handled = true;
                btnsession.Focus();
                iniciasession();
                Cursor.Current = Cursors.Default;
            }
        }

        private void txtusuario_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                e.Handled = true;
                txtcontraseña.Focus();
            }
        }

        private void lnksession_Click(object sender, EventArgs e)
        {
            Global._form_activo = 0;
            lnkinicio.Visible = false;
            pnlprincipal.Visible = true;
            txtusuario.Text = "";
            txtcontraseña.Text = "";
            activadesctiva(false);
            txtusuario.Focus();

        }

        private void Principal_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                if (pnlinicio.Visible) this.Close();

            }
        }

        private void btnpedidos_Click(object sender, EventArgs e)
        {
            try
            {

                //Form activo = new Form();
                //activo = Basico._form_principal.ActiveMdiChild;
                //activo.Close();

                //PanelPedidos frm = new PanelPedidos() { Owner = this };
                //frm.MdiParent = this;
                //frm.BringToFront();
                //frm.Show();
                //frm.ShowDialog();
                //menu.ActivarFormulario(new PanelPedidos());
                //menu.ActivarFormulario(frm);
                pnlprincipal.Visible = false;
                lnkinicio.Visible = true;               
            }
            catch
            {
            }
            
        }

        private void lnkinicio_Click(object sender, EventArgs e)
        {
            if (Global._form_activo == 0)
            {
                pnlprincipal.Visible = true;
                lnkinicio.Visible = false;
            }
            else
            {
                Global._form_activo = 0;
                NotaCredito activo = new NotaCredito();                
                activo.Close();
                //menu.ActivarFormulario(new PanelPedidos());
            }
        }

        private void btndcomprobante_Click(object sender, EventArgs e)
        {
            Form frm = new Duplicado_Tk();
            frm.Show();
        }

        private void btndguia_Click(object sender, EventArgs e)
        {
            //Form frm = new Duplicado_Guia();
            //frm.Show();
        }

        private void btnc_Click(object sender, EventArgs e)
        {
            try
            {

                //Form activo = new Form();
                //activo = Basico._form_principal.ActiveMdiChild;
                //activo.Close();

                //PanelPedidos frm = new PanelPedidos() { Owner = this };
                //frm.MdiParent = this;
                //frm.BringToFront();
                //frm.Show();
                //frm.ShowDialog();
                menu.ActivarFormulario(new NotaCredito());
                //menu.ActivarFormulario(frm);
                pnlprincipal.Visible = false;
                lnkinicio.Visible = true;
            }
            catch
            {
            }
        }

        private void btnanulardoc_Click(object sender, EventArgs e)
        {
            try
            {

                //Form activo = new Form();
                //activo = Basico._form_principal.ActiveMdiChild;
                //activo.Close();

                //PanelPedidos frm = new PanelPedidos() { Owner = this };
                //frm.MdiParent = this;
                //frm.BringToFront();
                //frm.Show();
                //frm.ShowDialog();
                menu.ActivarFormulario(new Anular_Documento());
                //menu.ActivarFormulario(frm);
                pnlprincipal.Visible = false;
                lnkinicio.Visible = true;
            }
            catch
            {
            }
        }

        private void btnanularnc_Click(object sender, EventArgs e)
        {
            try
            {

                //Form activo = new Form();
                //activo = Basico._form_principal.ActiveMdiChild;
                //activo.Close();

                //PanelPedidos frm = new PanelPedidos() { Owner = this };
                //frm.MdiParent = this;
                //frm.BringToFront();
                //frm.Show();
                //frm.ShowDialog();
                menu.ActivarFormulario(new Anular_NCredito());
                //menu.ActivarFormulario(frm);
                pnlprincipal.Visible = false;
                lnkinicio.Visible = true;
            }
            catch
            {
            }
        }

       
        
        
    
       
    }
}

