using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using www.aquarella.com.pe.bll;
using System.Data;
namespace www.aquarella.com.pe.Aquarella.Admonred
{
    public partial class Contactenos : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                enviar.Attributes.Add("onfocus", "cambiarEstilo();");
                inicio();
            }
        }
        private void inicio()
        {
            DataSet ds_contactenos = www.aquarella.com.pe.bll.Contactenos_Data.leer_contactenos_data();
            gvcontactenos.DataSource = ds_contactenos;
            gvcontactenos.DataBind();


            DataTable dt = www.aquarella.com.pe.bll.Contactenos_Data.departamento_contacto();
            dwdepartamento.DataSource = dt;
            dwdepartamento.DataBind();

            dwdepartamento.SelectedValue = "15";

            fcombo_prov("15");
            dwprovincia.SelectedValue = "128";

            fcombo_dis("128");

            // Ciudad
            //dwdistrito.SelectedValue = "1248";
            dwdistrito.SelectedValue = "-1";
        }
        protected void fcombo_prov(string var_id_dpto)
        {
            try
            {
                DataSet ds = Basic_Data.getinfoprovincia(var_id_dpto);
                dwprovincia.Items.Clear();
                //dwprovincia.DataSource = null;
                dwdistrito.Items.Clear();
                ListItem vlista = new ListItem();
                vlista.Text = "--Seleccionar de la lista--";
                vlista.Value = "-1";
                dwprovincia.Items.Add(vlista);
                dwprovincia.DataSource = ds.Tables[0];
                dwprovincia.DataBind();

                dwdistrito.Items.Add(vlista);
                dwdistrito.DataBind();
            }
            catch (Exception e) { throw new Exception(e.Message, e.InnerException); }
        }
        protected void dwdepartamento_SelectedIndexChanged(object sender, EventArgs e)
        {
            string viddepartemtno = dwdepartamento.SelectedValue;
            if (!(dwdepartamento.SelectedValue == "-1"))
            {
                // dwprovincia.Items.Clear();
                fcombo_prov(viddepartemtno);
            }
            else
            {
                dwprovincia.Items.Clear();
                dwdistrito.Items.Clear();
                ListItem vlista = new ListItem();
                vlista.Text = "--Seleccionar de la lista--";
                vlista.Value = "-1";
                dwprovincia.Items.Add(vlista);
                dwprovincia.DataBind();

                dwdistrito.Items.Add(vlista);
                dwdistrito.DataBind();
            }
        }

        protected void dwprovincia_SelectedIndexChanged(object sender, EventArgs e)
        {
            string vidprovincia = dwprovincia.SelectedValue;
            if (!(dwprovincia.SelectedValue == "-1"))
            {
                fcombo_dis(vidprovincia);
            }
            else
            {
                dwdistrito.Items.Clear();
                ListItem vlista = new ListItem();
                vlista.Text = "--Seleccionar de la lista--";
                vlista.Value = "-1";
                dwdistrito.Items.Add(vlista);
                dwdistrito.DataBind();
            }
        }
        protected void fcombo_dis(string var_id_prov)
        {
            try
            {
                DataSet ds = Basic_Data.getinfodistrito(var_id_prov);
                dwdistrito.Items.Clear();
                ListItem vlista = new ListItem();
                vlista.Text = "--Seleccionar de la lista--";
                vlista.Value = "-1";
                dwdistrito.Items.Add(vlista);
                dwdistrito.DataSource = ds.Tables[0];
                dwdistrito.DataBind();
            }
            catch (Exception e) { throw new Exception(e.Message, e.InnerException); }
        }

        protected void enviar_Click(object sender, EventArgs e)
        {
            if (hdestado.Value == "1") return;

           

            string _nombre = txtnombres.Text; string _apellidos = txtapellidos.Text; string _telefono = txttelefono.Text;
            string _email = txtemail.Text; string comentario = txtcomentario.Text; 
            string _direccion = dwdepartamento.SelectedItem.Text + " ," +dwprovincia.SelectedItem.Text + " ," + dwdistrito.SelectedItem.Text ;
            string _comentario = txtcomentario.Text;

            System.Threading.Thread.Sleep(2000);
            www.aquarella.com.pe.bll.Contactenos_Data._enviar_contactenos(_nombre,_apellidos,_telefono,_email,_comentario,_direccion);

           

            limpiar();

        }
        private void limpiar()
        {
            txtnombres.Text = "";
            txtapellidos.Text = "";
            txttelefono.Text = "";
            txtemail.Text = "";
            txtcomentario.Text = "";
            DataTable dt = www.aquarella.com.pe.bll.Contactenos_Data.departamento_contacto();
            dwdepartamento.DataSource = dt;
            dwdepartamento.DataBind();

            dwdepartamento.SelectedValue = "15";

            fcombo_prov("15");
            dwprovincia.SelectedValue = "128";

            fcombo_dis("128");

            // Ciudad
            //dwdistrito.SelectedValue = "1248";
            dwdistrito.SelectedValue = "-1";
        }
     
    }
}