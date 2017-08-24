using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.Data;
namespace Aquarella.bll
{
    class Liquidation_DtlViewModel
    {
       
        /// 
        /// </summary>
        private ObservableCollection<Liquidation_Dtl>
            _LiqDtlOC = new ObservableCollection<Liquidation_Dtl>();


        /// <summary>
        /// 
        /// </summary>
        /// <param name="ldv_co"></param>
        /// <param name="ldv_liquidation"></param>
        /// <returns></returns>
        public ObservableCollection<Liquidation_Dtl> getArticlesPackByLiq(String ldv_liquidation)
        {
            try
            {
                ///
                DataTable dtLiqDtl = Venta.leerarticulopaqliq( ldv_liquidation);
                ///
                if (dtLiqDtl != null)
                {
                    foreach (DataRow dr in dtLiqDtl.Rows)
                    {
                        ///
                        _LiqDtlOC.Add(new Liquidation_Dtl
                        {
                            _ldv_liquidation = dr["Liq_Det_Id"].ToString(),
                            _ldn_qty = Convert.ToDecimal(dr["Liq_Det_Cantidad"]),
                            _ldv_size = dr["Liq_Det_TalId"].ToString(),
                            _ldv_article = dr["Art_Id"].ToString(),
                            _ldn_pqty = Convert.ToDecimal(dr["Paq_Cantidad"]),
                            _ldn_pan_no = Convert.ToDecimal(dr["Paq_no"]),
                            _ldn_pdn_packageid = Convert.ToDecimal(dr["Paq_Id"]),
                            _ldv_article_brand = dr["Mar_Descripcion"].ToString(),
                            _ldv_article_name = dr["Art_Descripcion"].ToString(),
                            _ldv_article_color = dr["Col_Descripcion"].ToString(),
                            _calidad = dr["Calidad"].ToString()
                        });
                    }
                }
                ///
                return _LiqDtlOC;
            }
            catch
            {
                return null;
            }
        }


        public ObservableCollection<Liquidation_Dtl> getArtSizesLiquiNotPacking( String ldv_liquidation, String ldv_article)
        {
            try
            {
                ///
                DataTable dtLiqDtl = Venta.Datos_art_tallaemp(ldv_liquidation, ldv_article); // Liquidation_Dtl.getArtSizesLiquiNotPacking( ldv_liquidation, ldv_article);
                ///
                if (dtLiqDtl != null)
                {
                    /// Select Distinc Articles
                    var queryDistArticles = (from dRow in dtLiqDtl.AsEnumerable()
                                             select new
                                             {
                                                 ldv_liquidation = dRow["liq_id"],
                                                 ldv_article = dRow["Articulo"],
                                                 ldv_article_name = dRow["ART_DES"],
                                                 ldv_brand = dRow["Marca"],
                                                 ldv_color = dRow["Color"],
                                             }).Distinct();

                    /// Reccorrer todos los articulos del detalle de la orden
                    foreach (var row in queryDistArticles)
                    {
                        ///
                        String codeArticle = row.ldv_article.ToString();

                        List<Articles_Sizes> sizesArticle = new List<Articles_Sizes>();
                        sizesArticle = ((from myRow in dtLiqDtl.AsEnumerable()
                                         where myRow.Field<String>("Articulo") == codeArticle
                                         select new Articles_Sizes
                                         {
                                             _ASV_SIZE_DISPLAY = myRow["Tallas"].ToString(),
                                             _ASV_QTY = Convert.ToDecimal(myRow["cantidad"])
                                         })).ToList<Articles_Sizes>();
                        ///
                        _LiqDtlOC.Add(new Liquidation_Dtl
                        {
                            _ldv_liquidation = row.ldv_liquidation.ToString(),
                            _ldv_article_sizes = sizesArticle,
                            _ldv_article = codeArticle,
                            _ldv_article_brand = row.ldv_brand.ToString(),
                            _ldv_article_name = row.ldv_article_name.ToString(),
                            _ldv_article_color = row.ldv_color.ToString(),
                        });

                    }
                } /// Fin if de verificacion de retorno de datos nulos
                ///
                return _LiqDtlOC;
            }
            catch { return null; }
        }
    }
}
