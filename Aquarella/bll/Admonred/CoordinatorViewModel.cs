using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.Data;
namespace Aquarella.bll
{
    class CoordinatorViewModel
    {        
        /// </summary>
        private ObservableCollection<Coordinator>
            _CoordinatorOC = new ObservableCollection<Coordinator>();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cov_co"></param>
        /// <param name="con_coord_id"></param>
        /// <returns></returns>
        public ObservableCollection<Coordinator> getInfoCoordinator(Decimal con_coord_id)
        {
            try
            {
                ///
                //DataSet dsCoord = Coordinator.getInfoCoordinator( con_coord_id);
                /////
                //if (dsCoord != null)
                //{
                //    foreach (DataRow dr in dsCoord.Tables[0].Rows)
                //    {
                        ///
                        _CoordinatorOC.Add(new Coordinator
                        {
                            _cov_document =Coordinator._bas_documento,
                            _cov_name = Coordinator._bas_nombre,
                            _cov_addres =Coordinator._direccion,
                            _cov_city =Coordinator._lugar
                            //_cov_coordinator_type = dr["cov_coordinator_type"].ToString()
                        });
                //    }
                //}
                ///
                return _CoordinatorOC;
            }
            catch
            {
                return null;
            }
        }

    }
}
