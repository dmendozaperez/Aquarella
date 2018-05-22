﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Integrado.Bll
{
    class ReportInvoiceClass
    {
        #region < ATRIBUTOS >

        /// <summary>
        /// Atributos, Informacion de la bodega donde se realiza la devolucion
        /// </summary>
        public String wavDescription;
        public String wavAddress;
        public String wavPhone;
        public String wavUbication;
        public String ubicalugar;
        /// Informacion de la persona o nombre del facturado
        /// 

        /// <summary>
        /// 
        /// </summary>
        public String destinatario;
        /// <summary>
        /// 
        /// </summary>
        public String ubicacionDestinatario;
        /// <summary>
        /// 
        public string varPuntoLlegada;
        /// </summary>
        public String telefono;
        /// <summary>
        /// 
        /// </summary>
        public String celular;
        /// <summary>
        /// 
        /// </summary>
        public String email;
        /// <summary>
        /// 
        /// </summary>
        public String cedula;
        /// <summary>
        /// 
        /// </summary>
        public String nit;


        /// Informacion adicional
        /// 

        /// <summary>
        /// 
        /// </summary>
        public String numFactura;
        /// <summary>
        /// 
        /// </summary>
        public String numLiquidacion;
        /// <summary>
        /// 
        /// </summary>
        public DateTime fechaRemision;
        /// <summary>
        /// 
        /// </summary>
        public String numeroRemision;
        /// <summary>
        /// 
        /// </summary>
        public String enviadoPor;
        /// <summary>
        /// 
        /// </summary>
        public String esCopia;

        /// <summary>
        /// 
        /// </summary>
        public String typeresolution;


        /// Atributos del detalle de la factura
        /// 


        ///
        String codigoArticulo;
        /// <summary>
        /// 
        /// </summary>
        public String nomArticulo;
        /// <summary>
        /// 
        /// </summary>
        public String descripcionArtic;
        /// <summary>
        /// 
        /// </summary>
        public Decimal cantidad;
        /// <summary>
        /// 
        /// </summary>
        public String talla;
        /// <summary>
        /// 
        /// </summary>
        public Decimal precio;
        /// <summary>
        /// 
        /// </summary>
        public Decimal descuentoArticulo;
        /// <summary>
        /// 
        /// </summary>
        public Decimal valorLinea;
        /// <summary>
        /// 
        /// </summary>
        public Decimal comisionLineal;

        /// <summary>
        /// 
        /// </summary>
        public String marca;

        /// Totalizado
        /// 

        ///
        public Decimal iva;
        /// <summary>
        /// 
        /// </summary>
        public Decimal descuentoGnral;
        /// <summary>
        /// 
        /// </summary>
        public Decimal flete;

        /// Informacion Transportes
        /// 

        /// <summary>
        /// 
        /// </summary>
        public String numeroGuia;
        /// <summary>
        /// 
        /// </summary>
        public String trasportadora;

        /// Informacion adicional

        /// <summary>
        /// 
        /// </summary>
        public String msgs;

        public string namelider;
        public String dirlider;

        public string direccionf;

        public string agencia;

        public string agencia_ruc;

        #endregion

        #region < CONSTRUCTOR DE LA CLASE >

        public ReportInvoiceClass(String destinatario, String ubicacionDestinatario, String telefono, String celular,
                String email, String cedula, String nit, String numLiquidacion, String numFactura, DateTime fechaRemision,
                String numeroRemision, String enviadoPor, String esCopia, String typeresolution, String codigoArticulo, String nomArticulo,
                String descripcionArtic, Decimal cantidad, String talla, Decimal precio, Decimal descuentoArticulo, Decimal comisionLineal, String marca,
                Decimal valorLinea, Decimal iva, Decimal flete,
                String numeroGuia, String trasportadora, String msgs, Decimal descuentoGnral,
                String wavDescription, String wavAddress, String wavPhone, String wavUbication, string puntoLlegada, String ubicalugar, 
                string vdireccionf, string vnamelider, String vdirlider,string vagencia,string vagencia_ruc)
        {
            //
            // TODO: Agregar aquí la lógica del constructor
            //
            _direccionf = vdireccionf;
            _destinatario = destinatario;
            _ubicacionDestinatario = ubicacionDestinatario;
            _telefono = telefono;
            _celular = celular;
            _email = email;
            _cedula = cedula;
            _nit = nit;
            _numLiquidacion = numLiquidacion;
            _numFactura = numFactura;
            _fechaRemision = fechaRemision;
            _numeroRemision = numeroRemision;
            _enviadoPor = enviadoPor;
            _esCopia = esCopia;
            _typeresolution = typeresolution;
            _puntoLlegada = puntoLlegada;

            _codigoArticulo = codigoArticulo;
            _nomArticulo = nomArticulo;
            _descripcionArtic = descripcionArtic;
            _cantidad = cantidad;
            _talla = talla;
            _precio = precio;
            _descuentoArticulo = descuentoArticulo;
            _comisionLineal = comisionLineal;
            _marca = marca;
            _valorLinea = valorLinea;
            _iva = iva;
            _flete = flete;
            _numeroGuia = numeroGuia;
            _trasportadora = trasportadora;
            _msgs = msgs;
            _descuentoGnral = descuentoGnral;
            _namelider = vnamelider;
            _dirlider = vdirlider;
            /// <summary>
            /// Atributos, Informacion de la bodega donde se realiza la facturacion
            /// </summary>
            _wavDescription = wavDescription;
            _wavAddress = wavAddress;
            _wavPhone = wavPhone;
            _wavUbication = wavUbication;
            _ubicalugar = ubicalugar;

            _agencia = vagencia;
            _agencia_ruc = vagencia_ruc;
        }

        #endregion

        #region < SETTER's - GETTER's>


        /// Informacion de la persona o nombre del facturado
        ///
        /// <summary>
        /// 
        /// </summary>
        /// 
        public string _agencia_ruc
        {
            get { return agencia_ruc; }
            set { agencia_ruc = value; }
        }
        public string _agencia
        {
            get { return agencia; }
            set { agencia = value; }
        }
        public String _namelider
        {
            get { return namelider; }
            set { namelider = value; }
        }
        public String _dirlider
        {
            get { return dirlider; }
            set { dirlider = value; }
        }
        public string _direccionf
        {
            get { return direccionf; }
            set { direccionf = value; }
        }
        public String _ubicalugar
        {
            get { return ubicalugar; }
            set { ubicalugar = value; }
        }
        public String _destinatario
        {
            get { return destinatario; }
            set { destinatario = value; }
        }
        /// <summary>
        /// 
        /// </summary>
        public String _ubicacionDestinatario
        {
            get { return ubicacionDestinatario; }
            set { ubicacionDestinatario = value; }
        }
        /// <summary>
        /// 
        public String _puntoLlegada
        {
            get { return varPuntoLlegada; }
            set { varPuntoLlegada = value; }
        }

        /// 
        /// </summary>
        public String _telefono
        {
            get { return telefono; }
            set { telefono = value; }
        }
        /// <summary>
        /// 
        /// </summary>
        public String _celular
        {
            get { return celular; }
            set { celular = value; }
        }
        /// <summary>
        /// 
        /// </summary>
        public String _email
        {
            get { return email; }
            set { email = value; }
        }
        /// <summary>
        /// 
        /// </summary>
        public String _cedula
        {
            get { return cedula; }
            set { cedula = value; }
        }
        /// <summary>
        /// 
        /// </summary>
        public String _nit
        {
            get { return nit; }
            set { nit = value; }
        }


        /// Informacion adicional
        /// 

        /// <summary>
        /// 
        /// </summary>
        public String _numLiquidacion
        {
            get { return numLiquidacion; }
            set { numLiquidacion = value; }
        }
        public String _numFactura
        {
            get { return numFactura; }
            set { numFactura = value; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime _fechaRemision
        {
            get { return fechaRemision; }
            set { fechaRemision = value; }
        }
        /// <summary>
        /// 
        /// </summary>
        public String _numeroRemision
        {
            get { return numeroRemision; }
            set { numeroRemision = value; }
        }
        /// <summary>
        /// 
        /// </summary>
        public String _enviadoPor
        {
            get { return enviadoPor; }
            set { enviadoPor = value; }
        }
        /// <summary>
        /// 
        /// </summary>
        public String _esCopia
        {
            get { return esCopia; }
            set { esCopia = value; }
        }


        public String _typeresolution
        {
            get { return typeresolution; }
            set { typeresolution = value; }
        }
        /// Atributos del detalle de la factura
        /// 


        public String _codigoArticulo
        {
            get { return codigoArticulo; }
            set { codigoArticulo = value; }
        }
        /// <summary>
        /// 
        /// </summary>
        public String _nomArticulo
        {
            get { return nomArticulo; }
            set { nomArticulo = value; }
        }
        /// <summary>
        /// 
        /// </summary>
        public String _descripcionArtic
        {
            get { return descripcionArtic; }
            set { descripcionArtic = value; }
        }
        /// <summary>
        /// 
        /// </summary>
        public Decimal _cantidad
        {
            get { return cantidad; }
            set { cantidad = value; }
        }
        /// <summary>
        /// 
        /// </summary>
        public String _talla
        {
            get { return talla; }
            set { talla = value; }
        }
        /// <summary>
        /// 
        /// </summary>
        public Decimal _precio
        {
            get { return precio; }
            set { precio = value; }
        }
        /// <summary>
        /// 
        /// </summary>
        public Decimal _descuentoArticulo
        {
            get { return descuentoArticulo; }
            set { descuentoArticulo = value; }
        }
        /// <summary>
        /// 
        /// </summary>
        public Decimal _comisionLineal
        {
            get { return comisionLineal; }
            set { comisionLineal = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public String _marca
        {
            get { return marca; }
            set { marca = value; }
        }
        /// <summary>
        /// 
        /// </summary>
        public Decimal _valorLinea
        {
            get { return valorLinea; }
            set { valorLinea = value; }
        }

        /// Totalizado
        /// 

        /// <summary>
        /// 
        /// </summary>
        public Decimal _iva
        {
            get { return iva; }
            set { iva = value; }
        }
        public Decimal _flete
        {
            get { return flete; }
            set { flete = value; }
        }
        /// <summary>
        /// 
        /// </summary>
        public Decimal _descuentoGnral
        {
            get { return descuentoGnral; }
            set { descuentoGnral = value; }
        }


        /// Informacion Transportes
        /// 


        /// <summary>
        /// 
        /// </summary>
        public String _numeroGuia
        {
            get { return numeroGuia; }
            set { numeroGuia = value; }
        }
        /// <summary>
        /// 
        /// </summary>
        public String _trasportadora
        {
            get { return trasportadora; }
            set { trasportadora = value; }
        }

        /// Informacion adicional
        /// 

        public String _msgs
        {
            get { return msgs; }
            set { msgs = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public String _wavDescription
        {
            get { return wavDescription; }
            set { wavDescription = value; }
        }
        public String _wavAddress
        {
            get { return wavAddress; }
            set { wavAddress = value; }
        }
        public String _wavPhone
        {
            get { return wavPhone; }
            set { wavPhone = value; }
        }
        public String _wavUbication
        {
            get { return wavUbication; }
            set { wavUbication = value; }
        }

        #endregion
    }
}