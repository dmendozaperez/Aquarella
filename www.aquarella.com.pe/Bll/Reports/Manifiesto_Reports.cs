using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace www.aquarella.com.pe.bll.Reports
{
    public class Manifiesto_Reports
    {
        #region < Atributos >
        public decimal _id_man { get; set; }
        public DateTime _fecha_man { get; set; }
        public string _guia_man { get; set; }
        public string _doc_man { get; set; }
        public string _lider_man { get; set; }
        public string _promotor_man { get; set; }
        public Int32 _pares_man { get; set; }
        public string _agencia_man { get; set; }
        public string _destino_man { get; set; }

        #endregion

        public Manifiesto_Reports(Decimal idman,DateTime fechaman,string guiaman,string docman,string liderman,
                                  string promotorman,Int32 paresman,string agenciaman,string destinoman )
        {
            _id_man = idman;
            _fecha_man = fechaman;
            _guia_man = guiaman;
            _doc_man = docman;
            _lider_man = liderman;
            _promotor_man = promotorman;
            _pares_man = paresman;
            _agencia_man = agenciaman;
            _destino_man = destinoman;
        }
    }
}