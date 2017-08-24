using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Collections.ObjectModel;
using System.Data;

namespace Aquarella.bll
{
    class Transporters_GuidesViewModel
    {        
        /// <summary>
        /// 
        /// </summary>
        private ObservableCollection<Transporters_Guides>
            _TranspGuidesOC = new ObservableCollection<Transporters_Guides>();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tgv_co"></param>
        /// <param name="tgn_guide_id"></param>
        /// <returns></returns>
        public ObservableCollection<Transporters_Guides> getGuidesByPrimaryKey( Decimal tgn_guide_id)
        {
            try
            {
                ///
                //DataSet dsTranspGuides = Transporters_Guides.getGuidesByPrimaryKey(tgn_guide_id);
                /////
                //if (dsTranspGuides != null)
                //{
                //    foreach (DataRow dr in dsTranspGuides.Tables[0].Rows)
                //    {
                        ///
                        _TranspGuidesOC.Add(new Transporters_Guides
                        {
                            _tgv_guide =Transporters_Guides._guia,
                            _tgn_guide_id =Transporters_Guides._guia_id,
                            _tgv_transport = Transporters_Guides._transporte
                        });
                //    }
                //}
                ///
                return _TranspGuidesOC;
            }
            catch
            {
                return _TranspGuidesOC;
            }
        }

        /// <summary>
        /// Crea el registro de una nueva guia
        /// </summary>
        /// <param name="tgv_co"></param>
        /// <param name="tgv_guide"></param>
        /// <param name="tgn_transport"></param>
        /// <param name="tgn_addressee"></param>
        /// <returns></returns>
        public String addGuide(String tgv_guide,
                                String tgn_transport, String tgn_addressee)
        {
            return Transporters_Guides.addGuide(tgv_guide, tgn_transport, tgn_addressee);
        }

    }
}
