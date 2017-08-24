using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace www.aquarella.com.pe.bll.Ventas
{
    public class ReporteVentas
    {
        #region < ATRIBUTOS >

        /// <summary>
        /// Atributos, Informacion de la venta
        /// </summary>
        public String lider;
        public String mes;
        public int dia;
        public String cliente;
        public Decimal totalparesmonto;
        public String validaNumero;
        public String año;
        public String semana;
        public string resumen;
        public string asesor;
        public string dni;

        #endregion
        #region < CONSTRUCTOR DE LA CLASE >

        public ReporteVentas(String lider, String mes, int dia, String cliente,
                Decimal totalparesmonto, string validanumero, string año, string semana,string resumen,string asesor,string dni)
        {
            //
            // TODO: Agregar aquí la lógica del constructor
            //
            _lider = lider;
            _mes = mes;
            _dia = dia;
            _cliente = cliente;
            _totalparesmonto = totalparesmonto;
            _validanumero = validanumero;
            _año = año;
            _semana = semana;
            _resumen = resumen;
            _asesor = asesor;
            _dni = dni;
        }

        #endregion

        #region < SETTER's - GETTER's>
        public string _dni
        {
            get { return dni; }
            set { dni = value; }
        }
        public string _asesor
        {
            get { return asesor; }
            set { asesor = value; }
        }
        public string _resumen
        {
            get { return resumen; }
            set { resumen = value; }
        }
        public string _año
        {
            get { return año; }
            set { año = value; }
        }
        public String _semana
        {
            get { return semana; }
            set { semana = value; }
        }
        public string _validanumero
        {
            get { return validaNumero; }
            set { validaNumero = value; }
        }
        public String _lider
        {
            get { return lider; }
            set { lider = value; }
        }
        public String _mes
        {
            get { return mes; }
            set { mes = value; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int _dia
        {
            get { return dia; }
            set { dia = value; }
        }
        /// <summary>
        /// 
        /// </summary>
        public String _cliente
        {
            get { return cliente; }
            set { cliente = value; }
        }
        /// <summary>
        /// 
        /// </summary>
        public Decimal _totalparesmonto
        {
            get { return totalparesmonto; }
            set { totalparesmonto = value; }
        }

        #endregion
    }
}