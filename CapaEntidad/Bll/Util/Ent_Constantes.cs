using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CapaEntidad.Bll.Util
{
    public class Ent_Constantes
    {
        public static readonly string IdSystemAquarella = "1";

        /// <summary>
        /// Id del sistema de retail
        /// </summary>
        public static readonly string IdSystemRetail = "2";

        public static readonly string NameSessionCompany = "_COMPANY";
        public static readonly string NameSessionMachine = "_MACHINE";
        public static readonly string NameSessionUser = "_USERSOBJ";
        public static readonly string NameSessionUserName = "_USERNAME";
        public static readonly string NameSessionUserId = "_USERID";
        public static readonly string NameSessionSecurity = "_SECURITY";

        /// <summary>
        /// Estado de pedido para recoleccion en cedi
        /// </summary>
        public static readonly string IdStatusLiqRecolCed = "PRCS";

        /// <summary>
        /// Estado de una liquidacion para facturar
        /// </summary>
        public static readonly string IdStatusLiqForInvoice = "PF";

        /// <summary>
        /// Id tipo de transporte personalmente
        /// </summary>
        public static readonly decimal IdTypeTransportPerson = 1;

        /// <summary>
        /// 
        /// </summary>
        public static readonly string IdStatusActive = "A";


        public static readonly string IdStatusPasswordExpiration = "CC";
        public static readonly string IdStatusFinalized = "F";


        /// <summary>
        /// Id tipo de empleado habilitado para marcacion
        /// </summary>
        public static readonly string IdTypeEmployeePicking = "6";

        /// <summary>
        /// Id de articulo importado
        /// </summary>
        public static readonly string IdOriginImported = "I";

        /// <summary>
        /// Modulo para estados de pagos
        /// </summary>
        public static readonly string IdStatusPayments = "4";

        //***********************modulo de pago al credito
        public static readonly string IdStatuscredito = "5";
        //****************************************
        /// <summary>
        /// Acronimo inventario activo
        /// </summary>
        public static readonly string StatusActiveInvent = "IA";

        /// <summary>
        /// Devolucion en espera de aprbacion
        /// </summary>
        public static readonly string StatusReturnForAprob = "DEA";
    }
}
