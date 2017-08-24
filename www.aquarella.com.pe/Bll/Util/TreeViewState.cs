using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI.WebControls;

namespace www.aquarella.com.pe.bll.Util
{
    /// <summary>
    /// TreeViewState es una clase que se encarga de volver transparente</br>
    /// el manejo del estado de un arbol de menu de un usario.
    /// </summary>
    public class TreeViewState
    {
        #region < VARIABLES >

        private int RestoreTreeViewIndex;

        #endregion

        #region < CONSTRUCTORES >
        #endregion

        #region < PROPIEDADES >
        #endregion

        #region < METODOS PUBLICOS >

        /// <summary>
        /// Guarda en la session el estado de un arbol 
        /// </summary>
        /// <param name="treeView">Arbol para guardar</param>
        /// <param name="key">Identificador del arbol por si hay mas arboles</param>
        public void SaveTreeView(TreeView treeView, string key)
        {

            List<bool?> list = new List<bool?>();

            SaveTreeViewExpandedState(treeView.Nodes, list);

            HttpContext.Current.Session[key + treeView.ID] = list;
        }
        /// <summary>
        /// Restablece el treeview al estado antrior guardo en la session
        /// </summary>
        /// <param name="treeView">Arbol restablecer</param>
        /// <param name="key">un identificador por si hay mas arboles</param>
        public void RestoreTreeView(TreeView treeView, string key)
        {
            RestoreTreeViewIndex = 0;

            RestoreTreeViewExpandedState(treeView.Nodes,

                (List<bool?>)HttpContext.Current.Session[key + treeView.ID] ?? new List<bool?>());
        }

        #endregion

        #region < METODOS PRIVADOS >

        /// <summary>
        /// Guarda lso estados de los nodos si estan o no expandidos para qeu cuando se recargue
        ///la pagina estos no se pierdan.
        /// </summary>
        /// <param name="nodes"></param>
        /// <param name="list"></param>
        private void SaveTreeViewExpandedState(TreeNodeCollection nodes, List<bool?> list)
        {
            foreach (TreeNode node in nodes)
            {
                list.Add(node.Expanded);

                if (node.ChildNodes.Count > 0)
                {
                    SaveTreeViewExpandedState(node.ChildNodes, list);
                }
            }
        }
        /// <summary>
        /// Expande los nodos cuando se recarga el arbol.
        /// </summary>
        /// <param name="nodes"></param>
        /// <param name="list"></param>
        private void RestoreTreeViewExpandedState(TreeNodeCollection nodes, List<bool?> list)
        {
            foreach (TreeNode node in nodes)
            {
                if (RestoreTreeViewIndex >= list.Count) break;

                node.Expanded = list[RestoreTreeViewIndex++];

                if (node.ChildNodes.Count > 0)
                {
                    RestoreTreeViewExpandedState(node.ChildNodes, list);
                }
            }
        }

        #endregion

        #region < METODOS ESTATICOS >
        #endregion
    }
}