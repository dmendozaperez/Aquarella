using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;


namespace Aquarella.bll
{
    class Liquidation_Dtl
    {
        public String _ldv_liquidation { set; get; }
        public String _ldv_article { set; get; }
        public String _ldv_size { set; get; }
        public Decimal _ldn_qty { set; get; }
        public Decimal _ldn_pqty { set; get; }
        public Decimal _ldn_pan_no { set; get; }
        public Decimal _ldn_pdn_packageid { set; get; }
        public String _ldv_article_name { set; get; }
        public String _ldv_article_color { set; get; }
        public String _ldv_article_brand { set; get; }

        public string _calidad { set; get; }
        /// Opcional
        public List<Articles_Sizes> _ldv_article_sizes { set; get; }

        public static DataSet getArticlesPackByLiq( String ldv_liquidation)
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
        public static DataSet getArtSizesLiquiNotPacking( String ldv_liquidation, String ldv_article)
        {
            try
            {

                return null;
            }
            catch { return null; }
        }

    }
}
