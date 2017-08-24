using System;
using System.Data;
using System.Data.Common;


namespace Aquarella.bll
{
    class Transporters_Guides
    {

        public static decimal _guia_id { set; get; }
        public static string _guia { set; get; }
        public static string _transporte { set; get; }

        public Decimal _tgn_guide_id { set; get; }
        public String _tgv_guide { set; get; }
        public String _tgv_transport { set; get; }

        public static String addGuide(String tgv_guide,
                              String tgn_transport, String tgn_addressee)
        {
            ///
            try
            {
                String newIdGuia = "";
              
                ///
                return newIdGuia;
            }
            catch { return "-1"; }
        }
        public static DataSet getGuidesByPrimaryKey( Decimal tgn_guide_id)
        {
            try
            {
                // CURSOR REF
                //object results = new object[1];
                /////
                //Database db = DatabaseFactory.CreateDatabase(_nameConn);
                /////
                //String sqlCommand = "logistica.sp_getguidebyprimarykey";
                /////
                //DbCommand dbCommandWrapper = db.GetStoredProcCommand(sqlCommand, tgv_co, tgn_guide_id, results);
                /////
                //return db.ExecuteDataSet(dbCommandWrapper);
                return null;
            }
            catch
            {
                return null;
            }
        }

    }
}
