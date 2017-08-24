using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

namespace www.aquarella.com.pe.bll.Util
{
    public class ValuesDB
    {

        /// <summary>
        /// Acronimo de estado Activo
        /// </summary>
        public static readonly String acronymStatusActive = "A";

        /// <summary>
        /// Acronimo de estado Finalizado
        /// </summary>
        public static readonly String acronymStatusFinalized = "F";

        /// <summary>
        /// Acronimo de estado Cancelado
        /// </summary>
        public static readonly String acronymStatusCanceled = "K";

        /// <summary>
        /// Acronimo de estado Confirmado
        /// </summary>
        public static readonly String acronymStatusConfirmed = "C";

        /// <summary>
        /// Acronimo de estado Parcial
        /// </summary>
        public static readonly String acronymStatusPartial = "P";

        /// <summary>
        /// Acronimo de estado Inactivo
        /// </summary>
        public static readonly String acronymStatusInactive = "I";

        /// <summary>
        /// Acronimo del estado borrador
        /// </summary>
        public static readonly String acronymStatusEraser = "PB";

        /// <summary>
        /// Acronimo del estado de un pedido separado
        /// </summary>
        public static readonly String acronymStatusOrdersSeparated = "PS";

        /// <summary>
        /// Acronimo del estado de un pedido digitado
        /// </summary>
        public static readonly String acronymStatusOrderDigited = "PD";

        /// <summary>
        /// Pedido para marcar.
        /// </summary>
        public static readonly String acronymStatusOrderForPicking = "PM";

        /// <summary>
        /// Acronimo del estado de un pedido digitado
        /// </summary>
        public static readonly String acronymStatusOrderFactured = "PF";

        /// <summary>
        /// Acronimo del estado de un inventario activo
        /// </summary>
        public static readonly String acronymStatusInventoryActive = "IA";

        /// <summary>
        /// Valor del id del codigo de storage de produccion
        /// </summary>
        public static readonly String idStorageProduction = "4";


        /// <summary>
        /// Acronimo del estado de un pedido confirmado
        /// </summary>
        public static readonly String acronymStatusOrderConfirmed = "PC";

        /// <summary>
        /// Id del rol que rotula un usuario como coordinador
        /// </summary>
        public static readonly String idRolForCoordinators = "7";

        /// <summary>
        /// Id del rol que rotula un usuario como coordinador
        /// </summary>
        public static readonly String idDefaultPersonToInvoiceForCoordinators = "0";


        ///
        /////////////////////////////////////////////////////////////
        /// <summary>
        /// Valor del id (Codigo) del storage de Traspasos
        /// </summary>
        public static readonly String idStorageTransfer = "5";

        /// <summary>
        /// Valor del id (Codigo) del storage de estanterias
        /// </summary>
        public static readonly String idStorageEstanterias = "22";
        ///
        //////////////////////////////////////////////////////////////

        /// <summary>
        /// Tipo de coordinador = 4 cliente tipo cedi
        /// </summary>
        public static readonly String typeCoordCedi = "4";

        /// <summary>
        /// Conceptos para tipo de depositos
        /// </summary>
        public static readonly string typesDepositType = "F2";

    }
}