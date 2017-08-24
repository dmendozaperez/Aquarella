using System;
using System.Configuration;

///
namespace Aquarella.bll
{
    public static class ValuesDB
    {
        /// <summary>
        /// Nombre de la cadena de conexion a Oracle
        /// </summary>
      
        /// <summary>
        /// Titulo de la ventana emergente de error
        /// </summary>
        public static readonly String captionHeaderErrorWindow = "Aquarella - Mensaje De Error";

        /// <summary>
        /// Titulo de la ventana emergente de advertencia
        /// </summary>
        public static readonly String captionHeaderWarningWindow = "Aquarella - Mensaje De Advertencia";

        /// <summary>
        /// Acronimo del estado de un pedido digitado
        /// </summary>
        public static readonly String acronymStatusOrderFactured = "PF";

        /// <summary>
        /// Tipo de coordinador = 4 cliente tipo cedi
        /// </summary>
        public static readonly String typeCoordCedi = "4";

        /// <summary>
        /// Acronimo de estado Activo
        /// </summary>
        public static readonly String acronymStatusActive = "A";

        /// <summary>
        /// Acronimo de estado Finalizado
        /// </summary>
        public static readonly String acronymStatusFinalized = "F";
        

    }
}
