using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using informativa.aquarella.com.oe.Models;
namespace informativa.aquarella.com.oe.Data
{
    public class LugarBL
    {
        private LugarDA lugar = new LugarDA();
        public List<Lugar> Listar()
        {
            return lugar.Listar();
        }
    }
}