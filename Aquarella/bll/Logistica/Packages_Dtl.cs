using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace Aquarella.bll
{
    class Packages_Dtl
    {
        public Decimal _pdn_package { set; get; }
        public String _pdv_article { set; get; }
        public String _pdv_size { set; get; }
        public Decimal _pdn_qty { set; get; }
        public String _pdv_article_name { set; get; }
        public String _pdv_article_color { set; get; }
        public String _pdv_article_brand { set; get; }

        public string _calidad { set; get; }

        #region < METODOS PUBLICOS >

        /// <summary>
        /// Empacado de articulos
        /// </summary>
        /// <param name="_nameConn"></param>
        /// <param name="pdn_co"></param>
        /// <param name="pdn_package"></param>
        /// <param name="lhv_liquidation"></param>
        /// <param name="pdv_article"></param>
        /// <param name="pdv_size"></param>
        /// <param name="pdn_qty"></param>
        /// <returns></returns>
        public static String addArticlesToPackage(Decimal pdn_package,
            String lhv_liquidation, String pdv_article, String pdv_size, Decimal pdn_qty)
        {
            ///
            try
            {
               
                return null;
            }
            catch
            {
                return "-1";
            }
        }

        /// <summary>
        /// Consultar todos los articuloe empacados en determinado paquete
        /// </summary>
        /// <param name="_nameConn"></param>
        /// <param name="lhv_liquidation"></param>
        /// <param name="pdn_package"></param>
        /// <returns></returns>
        public static DataSet getArticlesPackingByNoPackage(
            String lhv_liquidation, Decimal pdn_package)
        {
            try
            {
              
                ///
                return null;
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// Eliminacion de un articulo del detalle de un paquete determinado
        /// </summary>
        /// <param name="_nameConn"></param>
        /// <param name="pdv_co"></param>
        /// <param name="pdn_package"></param>
        /// <param name="pdv_article"></param>
        /// <param name="pdv_size"></param>
        /// <returns></returns>
        public static String deleteLineFromPackagesDtl( Decimal pdn_package,
            String pdv_article, String pdv_size)
        {
            ///
            try
            {
                ///
               
                return "1";
            }
            catch
            {
                return "-1";
            }
        }


        /// <summary>
        /// Consulta el detalle del paquete
        /// </summary>
        /// <param name="_nameConn"></param>
        /// <param name="pdv_co"></param>
        /// <param name="lhv_liquidation"></param>
        /// <param name="pdn_package"></param>
        /// <returns></returns>
        public static DataSet getpackagedtl(
            String lhv_liquidation, Decimal pdn_package)
        {
            try
            {
               
                ///
                return null;
            }
            catch
            {
                return null;
            }
        }


        #endregion
    }
}
