using System;
using System.Collections.ObjectModel;
using System.Data;

namespace Aquarella.bll
{
    class Packages_DtlViewModel
    {        
        /// 
        /// </summary>
        private ObservableCollection<Packages_Dtl>
            _PackDtlOC = new ObservableCollection<Packages_Dtl>();

        /// <summary>
        /// Interfaz de empacado de aticulos
        /// </summary>
        /// <param name="pdn_co"></param>
        /// <param name="pdn_package"></param>
        /// <param name="lhv_liquidation"></param>
        /// <param name="pdv_article"></param>
        /// <param name="pdv_size"></param>
        /// <param name="pdn_qty"></param>
        /// <returns></returns>
        public String addArticlesToPackage(Decimal pdn_package, String lhv_liquidation, String pdv_article, String pdv_size, Decimal pdn_qty)
        {
            ///
            return Packages_Dtl.addArticlesToPackage(pdn_package, lhv_liquidation, pdv_article, pdv_size, pdn_qty);
        }

        /// <summary>
        /// Consultar articulos totales empacados en un determinado paquete
        /// </summary>
        /// <param name="pdv_co"></param>
        /// <param name="lhv_liquidation"></param>
        /// <param name="pdn_package"></param>
        /// <returns></returns>
        public ObservableCollection<Packages_Dtl>  getArticlesPackingByNoPackage(
           String lhv_liquidation, Decimal pdn_package)
        {
            try {
                ///
                DataTable dtPackDtl = Venta.leer_articulopacking_paquete(lhv_liquidation, pdn_package);
                ///
                if (dtPackDtl != null)
                {
                    foreach (DataRow dr in dtPackDtl.Rows)
                    {
                        ///
                        _PackDtlOC.Add(new Packages_Dtl
                        {
                            ///SUM (ldn_qty) qtystotliq
                            _pdn_package = pdn_package,
                            _pdv_article = dr["Art_Id"].ToString(),
                            _pdv_size = dr["Liq_Det_TalId"].ToString(),
                            _pdn_qty = Convert.ToDecimal(dr["cant_paq"]),
                            _pdv_article_name = dr["Art_Descripcion"].ToString(),
                            _pdv_article_brand = dr["Mar_Descripcion"].ToString(),
                            _pdv_article_color = dr["Col_Descripcion"].ToString(),
                            _calidad = dr["calidad"].ToString()
                        });
                    }
                }
                ///
                return _PackDtlOC;
            }
            catch { return null; }
        }


        /// <summary>
        /// Interfaz de eliminado de aticulos del detalle de un paquete
        /// </summary>
        /// <param name="pdv_co"></param>
        /// <param name="pdn_package"></param>
        /// <param name="pdv_article"></param>
        /// <param name="pdv_size"></param>
        /// <returns></returns>
        public String deleteLineFromPackagesDtl( Decimal pdn_package, String pdv_article, String pdv_size)
        {
            ///
            return Venta.borrar_lineapaquete(pdn_package, pdv_article, pdv_size);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pdv_co"></param>
        /// <param name="lhv_liquidation"></param>
        /// <param name="pdn_package"></param>
        /// <returns></returns>
        public DataSet getpackagedtl(String lhv_liquidation, Decimal pdn_package)
        {
            try
            {
                ///
                return Packages_Dtl.getpackagedtl( lhv_liquidation, pdn_package);
            }
            catch { return null; }
        }

    }
}
