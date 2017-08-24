using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using www.aquarella.com.pe.bll;
using www.aquarella.com.pe.bll.Util;
using www.aquarella.com.pe.bll.Control;
using www.aquarella.com.pe.Bll.Util;
//using Bata.Aquarella.BLL;
//using Bata.Aquarella.BLL.Control;
//using Bata.Aquarella.BLL.Util;
//using www.Aquarella.com.pe.Bll.Util;

namespace www.aquarella.com.pe.Design
{
    public partial class Site : System.Web.UI.MasterPage
    {

        string _node;
        /// <summary>
        /// Load de la pagina
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            //SingleTon.ClearCache();
            // The Controls collection cannot be modified because the control contains code blocks (i.e. <% ... %>).
            // ISSUE:http://leedumond.com/blog/the-controls-collection-cannot-be-modified-because-the-control-contains-code-blocks/
            Page.Header.DataBind();

            if (!IsPostBack)
            {
                // Carga de objeto del usuario en sesion
                Users user = (Users)Session[Constants.NameSessionUser];
                _node = (string)(Session["_NODE"]) != null ? (string)(Session["_NODE"]) : "";

                if (user != null)
                {
                    /// Nombre del usuario actualmente en sessiom
                    lblWhoIs.Text = user._nombre;// . _usv_name;
                    // Bodega y area a las cuales pertenece
                    //lblAreaWh.Text = user. user._usv_area_name + " - " + user._usv_warehouse_name;
                    lblAreaWh.Text = user._usu_tip_nombre;
                    //Se carga el menu
                    LoadMenu();
                }

                //Solo si hay un nodo en la session se selecciona.
                if (!_node.Equals(string.Empty))
                {
                    //Recuperacion del estado anterior a la selecion de un nodo del arbol
                    new TreeViewState().RestoreTreeView(MenuPrin, "estados");
                    //Busqueda del nodo en el arbol
                    TreeNode tn = MenuPrin.FindNode(_node);
                    //Seleccion del nodo en el arbol para que quede marcado y el usuario 
                    //no se confunda
                    if (tn != null)
                    {
                        tn.Selected = true;
                    }
                }
            }
        }

        private void LoadMenu()
        {
            List<ApplicationFunctions> colappfunctions = new List<ApplicationFunctions>();
            colappfunctions = (List<ApplicationFunctions>)Session["_MENU"];
            foreach (ApplicationFunctions app in colappfunctions)
            {

                //esta condicion indica q son elementos padre.
                if (app._id == app._idpadre)
                {
                    TreeNode mnuMenuItem = new TreeNode();
                    //MenuItem mnuMenuItem = new MenuItem();
                    mnuMenuItem.Value = app._id.ToString() + "¬" + app._url;
                    mnuMenuItem.Text = " " + app._name;
                    //mnuMenuItem.ImageUrl = row[6].ToString();
                    //mnuMenuItem.NavigateUrl = app._url; // +"?titulo=" + row[7].ToString();
                    //mnuMenuItem.Target = "_self";

                    //Hacemos un llamada al método recursivo encargado de generar el arbol del menú
                    bool bChilds = this.AddMenuItem(ref mnuMenuItem, colappfunctions);
                    //Agregoamos el item al menu
                    if (bChilds || !app._url.Equals(""))
                        MenuPrin.Nodes.Add(mnuMenuItem);
                }
            }
        }

        public bool AddMenuItem(ref TreeNode mnuMenuItem, List<ApplicationFunctions> colappfunctions)
        {
            bool bChilds = false;
            string[] strValue = mnuMenuItem.Value.Split('¬');
            //recorremos cada elemento del datatable para poder determinar cuales son elementos hijos
            //del menuitem dado pasado como parametro 
            foreach (ApplicationFunctions app in colappfunctions)
            {
                //if (row[3].ToString().Equals(mnuMenuItem.Value) && !row[0].ToString().Equals(row[3].ToString()))
                if (app._idpadre.ToString().Equals(strValue[0]) && !app._id.ToString().Equals(app._idpadre.ToString()))
                {
                    bChilds = true;
                    TreeNode mnuNewMenuItem = new TreeNode();
                    mnuNewMenuItem.Value = app._id.ToString() + "¬" + app._url;
                    mnuNewMenuItem.Text = " " + app._name;
                    //mnuNewMenuItem.ImageUrl = row[4].ToString();                    
                    //mnuNewMenuItem.NavigateUrl = app._url;
                    //mnuNewMenuItem.Target = "_self";                      
                    //llamada recursiva para ver si el nuevo menú ítem aun tiene elementos hijos.
                    bool bSubChilds = this.AddMenuItem(ref mnuNewMenuItem, colappfunctions);

                    //Agregamos el Nuevo MenuItem al MenuItem que viene de un nivel superior.
                    if (bSubChilds || !app._url.Equals(""))
                        mnuMenuItem.ChildNodes.Add(mnuNewMenuItem);
                }
            }

            return bChilds;
        }

        /// <summary>
        /// Al momento de cerrado de sesion
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void HeadLoginStatus_LoggedOut(object sender, EventArgs e)
        {
            ///
            Session.Clear();
            ///
            Session.Abandon();
        }

        protected void MenuPrin_SelectedNodeChanged(object sender, EventArgs e)
        {
            //MenuPrin.
            if (MenuPrin.SelectedNode.Value != string.Empty)
            {
                if (!extraerURL(MenuPrin.SelectedNode.Value).Equals(string.Empty))
                {
                    //Response.Redirect(MenuPrin.SelectedNode.NavigateUrl);

                    Session["_NODE"] = MenuPrin.SelectedNode.ValuePath;

                    new TreeViewState().SaveTreeView(MenuPrin, "estados");

                    Response.Redirect(extraerURL(MenuPrin.SelectedNode.Value), true);
                }
            }
        }

        /// <summary>
        /// Extrae la ruta url del value de un nodo de palicacion si es necesario
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        private string extraerURL(string value)
        {
            string[] url;
            try
            {
                url = value.Split('¬');
            }
            catch { return string.Empty; }
            if (url.Length > 1)
                return url[1];
            else
                return string.Empty;
        }


      

    }

}