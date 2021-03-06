﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CapaEntidad.Bll.Ecommerce
{
    /// <summary>
    /// Clase para la representación de Productos para GuiaUrbano
    /// </summary>
    public class Ent_Producto
    {
        //Datos de producto requeridos por proveedor logistico    
        public string cod_sku { get; set; }  //Código único de producto
        public string descr_sku { get; set; } //Descripción de producto
        public string modelo_sku { get; set; } //SKU de producto padre
        public string marca_sku { get; set; } //Marca de producto
        public string peso_sku { get; set; } //Peso de producto (0.3 / por defecto)
        public string cantidad_sku { get; set; } //Cantidad de bultos
    }
}
