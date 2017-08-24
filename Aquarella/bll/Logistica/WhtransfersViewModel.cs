using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aquarella.bll
{
    class WhtransfersViewModel
    {
        public String[] saveWhTransference(String lhv_liquidation)
        {
            return Whtransfer.saveWhTransference(lhv_liquidation);
        }
    }
}
