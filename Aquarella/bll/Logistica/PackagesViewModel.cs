using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.Data;
namespace Aquarella.bll
{
    class PackagesViewModel
    {
        /// <summary>
        /// Recuperar el nombre de la conexion a la base de datos
        /// </summary>       

        /// <summary>
        /// 
        /// </summary>
        private ObservableCollection<Packages>
            _PackagesOC = new ObservableCollection<Packages>();


        /// <summary>
        /// Interfaz de consulta de id del paquete, entre la vista y el controlador
        /// </summary>
        /// <param name="pav_co"></param>
        /// <param name="lhv_liquidation_no"></param>
        /// <param name="_idUser"></param>
        /// <returns></returns>
        public Decimal addOrGetPackage(String lhv_liquidation_no, String _idUser)
        {
            ///
            return Venta.insertar_leer_paquete(lhv_liquidation_no);            
        }

        /// <summary>
        /// Consultar el numero maximo de los paquetes contenidos en una liquidacion
        /// </summary>
        /// <param name="pav_co"></param>
        /// <param name="lhv_liquidation_no"></param>
        /// <returns></returns>
        public ObservableCollection<Packages> getMaxNoPackageByLiqui( String lhv_liquidation_no)
        {
            try
            {
                ///
                //DataSet dsPackage = Packages.getMaxNoPackageByLiqui( lhv_liquidation_no);
                /////
                //if (dsPackage != null)
                //{
                //    foreach (DataRow dr in dsPackage.Tables[0].Rows)
                //    {
                //        ///
                        _PackagesOC.Add(new Packages
                        {
                            _pan_no = Venta.leer_maxnopaqliq(lhv_liquidation_no).ToString(), //dr["maxpackno"].ToString(),
                        });
                //    }
                //}
                ///
                return _PackagesOC;
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pav_co"></param>
        /// <param name="lhv_liquidation_no"></param>
        /// <param name="pdn_packageid"></param>
        /// <returns></returns>
        public ObservableCollection<Packages> getPackagesByPrimaryKey( String lhv_liquidation_no, Decimal pdn_packageid)
        {
            try
            {
                ///
                //DataSet dsPackage = Packages.getPackagesByPrimaryKey( lhv_liquidation_no, pdn_packageid);
                /////
                //if (dsPackage != null)
                //{
                //    foreach (DataRow dr in dsPackage.Tables[0].Rows)
                //    {
                        ///
                        _PackagesOC.Add(new Packages
                        {
                            _pan_no =Packages._paq_no,
                            _pdn_packageid =Packages._paq_id
                        });
                //    }
                //}
                ///
                return _PackagesOC;
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pav_co"></param>
        /// <param name="lhv_liquidation_no"></param>
        /// <param name="pdn_packageid"></param>
        /// <returns></returns>
        public DataSet getPackage(String lhv_liquidation_no, Decimal pdn_packageid)
        {
            try
            {
                ///
                return Packages.getPackage( lhv_liquidation_no, pdn_packageid);
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// Actualizar el peso final de un paquete
        /// </summary>
        /// <param name="pav_co"></param>
        /// <param name="pdn_packageid"></param>
        /// <param name="pan_weigth"></param>
        /// <returns></returns>
        public String updatePackageWeigth(string _liq_id)
        {
            ///
            return Venta.insertar_leer_paquete(_liq_id).ToString();//Packages.updatePackageWeigth(pdn_packageid, pan_weigth);

        }

    }
}
