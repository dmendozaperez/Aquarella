using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Integrado.Bll
{
    class ReportInvoiceSummary
    {
        #region < ATRIBUTOS >

        /// <summary>
        /// Atributos, Informacion de la bodega donde se realiza la devolucion
        /// </summary>
        public String _varId_Package;
        public String _varNunPaquete;
        public decimal _cantidadBultos;


        #endregion

        #region < CONSTRUCTOR DE LA CLASE >

        public ReportInvoiceSummary(string varId_Package, string varNunPaquete, decimal varCantidadBultos)
        {
            //
            // TODO: Agregar aquí la lógica del constructor
            //
            Id_Package = varId_Package;
            NunPaquete = varNunPaquete;
            cantidadBultos = varCantidadBultos;
        }

        #endregion

        #region < SETTER's - GETTER's>


        /// Informacion de la persona o nombre del facturado
        ///
        /// <summary>
        /// 
        /// </summary>
        public String Id_Package
        {
            get { return _varId_Package; }
            set { _varId_Package = value; }
        }
        /// <summary>
        /// 
        /// </summary>
        public String NunPaquete
        {
            get { return _varNunPaquete; }
            set { _varNunPaquete = value; }
        }
        /// <summary>
        /// 

        public Decimal cantidadBultos
        {
            get { return _cantidadBultos; }
            set { _cantidadBultos = value; }
        }

        #endregion
    }
}
