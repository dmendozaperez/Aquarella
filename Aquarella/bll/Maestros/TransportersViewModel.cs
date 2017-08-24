using System;
using System.Collections.ObjectModel;
using System.Data;

namespace Aquarella.bll///.Maestros
{
    class TransportersViewModel
    {
        /// <summary>
        /// Recuperar el nombre de la conexion a la base de datos
        /// </summary>       

        /// <summary>
        /// 
        /// </summary>
        private ObservableCollection<Transporters>
            _TransOC = new ObservableCollection<Transporters>();

        /// <summary>
        /// Consultar todas las transportadoras
        /// </summary>
        /// <param name="trv_co"></param>
        /// <returns></returns>
        public ObservableCollection<Transporters> getAllTransportsByCompany()
        {
            try
            {
                ///
                DataTable dtTrans = Basico.leertrasnportador();// Transporters.getAllTransportsByCompany();
                ///
                if (dtTrans != null)
                {
                    foreach (DataRow dr in dtTrans.Rows)
                    {
                        ///
                        _TransOC.Add(new Transporters
                        {
                            _trv_address = dr["tra_descripcion"].ToString(),
                            //_trv_name = dr["tra_id"].ToString(),
                            ////_trv_phone = dr["trv_phone"].ToString(),
                            _trv_transporters_id = dr["tra_id"].ToString()
                        });
                    }
                }
                ///
                return _TransOC;
            }
            catch
            {
                return new ObservableCollection<Transporters>();
            }
        }
    }
}
