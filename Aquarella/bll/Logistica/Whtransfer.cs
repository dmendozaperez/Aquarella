using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aquarella.bll
{
    class Whtransfer
    {
        #region < METODOS PUBLICOS >

        /// <summary>
        /// Realizar un traspaso entre bodegas
        /// </summary>
        /// <param name="_nameConn"></param>
        /// <param name="_company"></param>
        /// <param name="_liquidation"></param>
        /// <returns></returns>
        public static String[] saveWhTransference(String lhv_liquidation)
        {
            ///
            String resultDoc_out = "";
            String resultDoc_in = "";
            try
            {
              
                String[] docs = new String[2];
                docs[0] = resultDoc_out;
                docs[1] = resultDoc_in;
                ///
                return docs;
            }
            catch
            {
                return null;
            }
        }

        #endregion
    }
}
