using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
namespace Aquarella.bll
{
    class Coordinator
    {

        //variables globales
        public static string _bas_documento { set;get; }
        public static string _bas_nombre { set; get; }
        public static string _direccion { set; get; }
        public static string _lugar { set;get; }



        public String _cov_document { set; get; }
        public String _cov_name { set; get; }
        public String _cov_addres { set; get; }
        public String _cov_city { set; get; }
        public String _cov_coordinator_type { set; get; }

        public static DataSet getInfoCoordinator(Decimal con_coord_id)
        {
            try
            {
                // CURSOR REF
                //object results = new object[1];
                /////
                //Database db = DatabaseFactory.CreateDatabase(_nameConn);
                //String sqlCommand = "admonred.sp_get_BasicInfoCoordinator";
                //DbCommand dbCommandWrapper = db.GetStoredProcCommand(sqlCommand, cov_co, con_coord_id, results);
                /////
                return null;
            }
            catch
            {
                return null;
            }

        }
    }
}
