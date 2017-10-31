using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using www.aquarella.pe.Data.Util;

namespace www.aquarella.pe.Data.Control
{
    public class Menu
    {
        #region<REGION DE VARIABLES>

        public Int32 fun_id { get; set; }
        public string fun_nombre { get; set; }
        public string fun_descripcion { get; set; }
        public Int32 fun_padre { get; set; }
        public string apl_url { get; set; }
        public string apl_comentario { get; set; }
        public Int32 fun_orden { get; set; }
        public Int32 apl_id { get; set; }

        public string apl_controller { get; set; }
        public string apl_action { get; set; }
        #endregion

        #region<REGION DE PROPIEDADES>
        public List<Menu> Menu_Acceso(Decimal _bas_id)
        {
            DataTable dt = null;
            List<Menu> menu = null;
            try
            {
                dt = dt_menu(_bas_id);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        menu = new List<Menu>();
                        for (Int32 i = 0; i < dt.Rows.Count; ++i)
                        {
                            Menu items = new Menu
                            {
                                fun_id = Convert.ToInt32(dt.Rows[i]["fun_id"]),
                                fun_nombre = dt.Rows[i]["fun_nombre"].ToString(),
                                fun_descripcion = dt.Rows[i]["fun_descripcion"].ToString(),
                                fun_padre = Convert.ToInt32(dt.Rows[i]["fun_padre"]),
                                apl_url = dt.Rows[i]["apl_url"].ToString(),
                                apl_comentario = dt.Rows[i]["apl_comentario"].ToString(),
                                fun_orden = Convert.ToInt32(dt.Rows[i]["fun_orden"]),
                                apl_id = Convert.ToInt32(dt.Rows[i]["apl_id"]),
                                apl_controller =dt.Rows[i]["apl_controller"].ToString(),
                                apl_action =dt.Rows[i]["apl_action"].ToString(),
                            };
                            menu.Add(items);
                        }

                    }
                }
            }
            catch
            {
                menu = null;
            }
            return menu;
        }
        private DataTable dt_menu(Decimal _bas_id)
        {
            string sqlquery = "[USP_Leer_Funcion_Arbol]";
            DataTable dt = null;
            try
            {
                using (SqlConnection cn = new SqlConnection(Conexion.conexion_sql))
                {
                    using (SqlCommand cmd = new SqlCommand(sqlquery, cn))
                    {
                        cmd.CommandTimeout = 0;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@bas_id", _bas_id);
                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            dt = new DataTable();
                            da.Fill(dt);
                        }
                    }
                }
            }
            catch
            {

            }
            return dt;
        }
        #endregion
    }
    public class Menu_Items
    {
        public int Id { get; set; }
        public string nameOption { get; set; }
        public string controller { get; set; }
        public string action { get; set; }
        public string area { get; set; }
        public string imageClass { get; set; }
        public string activeli { get; set; }
        public bool estatus { get; set; }
        public int parentId { get; set; }
        public bool isParent { get; set; }
        public bool hasChild { get; set; }
    }
    public class Data_Menu
    {
        public IEnumerable<Menu_Items> navbarItems(Decimal _bas_id)
        {

            Menu menu_data = new Menu();

            List<Menu> menu_acceso = menu_data.Menu_Acceso(_bas_id);

            /*select a padre de menu*/
            //var menu_padre = menu_acceso.Where(menupadre => menupadre.fun_id == menupadre.fun_padre);            

            var menu = new List<Menu_Items>();

            //menu = null;

            if (menu_acceso != null)
            {
                //Int32 _id = 1;
                //Int32 _id_pad = 1;
                //recorre el padre de menu
                foreach (Menu app_padre in menu_acceso.Where(menupadre => menupadre.fun_id == menupadre.fun_padre))
                {
                    var menu_padre = menu_acceso.Where(menuv => menuv.fun_padre == app_padre.fun_id);
                    if (menu_padre.Count() != 1)
                    {
                        menu.Add(new Menu_Items { Id = app_padre.fun_id, nameOption = app_padre.fun_nombre, controller = "Home", action = "Index", imageClass = "fa fa-fw fa-dashboard", estatus = true, isParent = true, parentId = app_padre.fun_padre, activeli = "submenu" });
                    }
                    //{ 
                    //     menu.Add(new Navbar { Id = app_padre.fun_id, nameOption = app_padre.fun_nombre, controller = "Home", action = "Index", imageClass = "fa fa-fw fa-dashboard", estatus = true, isParent = true, parentId = app_padre.fun_padre, activeli = "submenu" });
                    //}
                    //_id += 1;
                    //en este caso vamos a ver el submenu del menu principal
                    //var sub_menu_padre = menu_acceso.Where(submenupadre => submenupadre.fun_padre == app_padre.fun_id && submenupadre.fun_id == 0);

                    foreach (Menu app_sub_padre in menu_acceso.Where(submenupadre => submenupadre.fun_padre == app_padre.fun_id && submenupadre.fun_padre != submenupadre.fun_id))
                    {
                        if (app_sub_padre.fun_id == 0)
                        {
                            menu.Add(new Menu_Items { Id = app_sub_padre.fun_id, nameOption = app_sub_padre.fun_nombre, controller = app_sub_padre.apl_controller, action = app_sub_padre.apl_action, imageClass = "fa fa-fw fa-dashboard", estatus = true, isParent = false, parentId = app_sub_padre.fun_padre });
                        }
                        else
                        {
                            menu.Add(new Menu_Items { Id = app_sub_padre.fun_id, nameOption = app_sub_padre.fun_nombre, controller = "Home", action = "Index", imageClass = "fa fa-fw fa-dashboard", estatus = true, isParent = true, parentId = app_sub_padre.fun_padre, activeli = "submenu" });

                            /*sumenu nivel 2 del menu*/
                            foreach (Menu app_sub_menu in menu_acceso.Where(submenu => submenu.fun_padre == app_sub_padre.fun_id))
                            {
                                menu.Add(new Menu_Items { Id = app_sub_menu.fun_id, nameOption = app_sub_menu.fun_nombre, controller = app_sub_menu.apl_controller, action = app_sub_menu.apl_action, imageClass = "fa fa-fw fa-dashboard", estatus = true, isParent = false, parentId = app_sub_menu.fun_padre });
                            }

                        }
                        //_id += 1;
                    }
                    //_id += 1;
                    //_id_pad += 1;

                }

            }




            return menu;
        }
    }
}