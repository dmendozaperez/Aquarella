//using Bukimedia.PrestaSharp.Entities;
using CapaDato.Bll.Ecommerce;
using CapaEntidad.Bll.Ecommerce;
using CapaEntidad.Bll.Util;
using MySql.Data.MySqlClient;
using Servicios.Ecommerce;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Integrado.Prestashop
{
    public class LeerPedidos
    {
        #region<EXTRAE LOS PEDIDOS PENDIENTES DE PRESTASHOP>
        EcommerceBL oEcommerce = new EcommerceBL();

        //List<order> pedidosEcommerce = new List<order>();

        //public List<order> ListaPedidosPagados()
        //{
        //    return oEcommerce.ListaPedidosPagados();
        //}

        //public customer GetCliente(int id)
        //{
        //    return oEcommerce.GetCliente(id);
        //}

        //public address GetDireccion(int id)
        //{
        //    return oEcommerce.GetDireccion(id);
        //}

        //public order_payment GetPayment(string reference)
        //{
        //    return oEcommerce.GetPayment(reference);
        //}

        //private DataTable PrepararPedidos()
        //{
        //    DataTable Prestashop = new DataTable("Ordenes");
        //    DataColumn column;
        //    DataRow row;
        //    pedidosEcommerce = ListaPedidosPagados();

        //    customer cliente = new customer();
        //    address direccion_cli = new address();
        //    address direccion_ent = new address();
        //    order_payment pago = new order_payment();

        //    column = new DataColumn();
        //    column.DataType = System.Type.GetType("System.Int32");
        //    column.ColumnName = "ped_id";
        //    column.Caption = "ped_id";
        //    Prestashop.Columns.Add(column);

        //    column = new DataColumn();
        //    column.DataType = System.Type.GetType("System.String");
        //    column.ColumnName = "ped_ref";
        //    column.Caption = "ped_ref";
        //    Prestashop.Columns.Add(column);

        //    column = new DataColumn();
        //    column.DataType = System.Type.GetType("System.String");
        //    column.ColumnName = "ped_fecha";
        //    column.Caption = "ped_fecha";
        //    Prestashop.Columns.Add(column);

        //    column = new DataColumn();
        //    column.DataType = System.Type.GetType("System.String");
        //    column.ColumnName = "ped_ubigeo_ent";
        //    column.Caption = "ped_ubigeo_ent";
        //    Prestashop.Columns.Add(column);

        //    column = new DataColumn();
        //    column.DataType = System.Type.GetType("System.String");
        //    column.ColumnName = "ped_dir_ent";
        //    column.Caption = "ped_dir_ent";
        //    Prestashop.Columns.Add(column);

        //    column = new DataColumn();
        //    column.DataType = System.Type.GetType("System.Decimal");
        //    column.ColumnName = "ped_total_sigv";
        //    column.Caption = "ped_total_sigv";
        //    Prestashop.Columns.Add(column);

        //    column = new DataColumn();
        //    column.DataType = System.Type.GetType("System.Decimal");
        //    column.ColumnName = "ped_total_cigv";
        //    column.Caption = "ped_total_cigv";
        //    Prestashop.Columns.Add(column);

        //    column = new DataColumn();
        //    column.DataType = System.Type.GetType("System.Decimal");
        //    column.ColumnName = "ped_dcto_sigv";
        //    column.Caption = "ped_dcto_sigv";
        //    Prestashop.Columns.Add(column);

        //    column = new DataColumn();
        //    column.DataType = System.Type.GetType("System.Decimal");
        //    column.ColumnName = "ped_dcto_cigv";
        //    column.Caption = "ped_dcto_cigv";
        //    Prestashop.Columns.Add(column);

        //    column = new DataColumn();
        //    column.DataType = System.Type.GetType("System.Decimal");
        //    column.ColumnName = "ped_ship_sigv";
        //    column.Caption = "ped_ship_sigv";
        //    Prestashop.Columns.Add(column);

        //    column = new DataColumn();
        //    column.DataType = System.Type.GetType("System.Decimal");
        //    column.ColumnName = "ped_ship_cigv";
        //    column.Caption = "ped_ship_cigv";
        //    Prestashop.Columns.Add(column);

        //    column = new DataColumn();
        //    column.DataType = System.Type.GetType("System.Int32");
        //    column.ColumnName = "cli_id";
        //    column.Caption = "cli_id";
        //    Prestashop.Columns.Add(column);

        //    column = new DataColumn();
        //    column.DataType = System.Type.GetType("System.String");
        //    column.ColumnName = "cli_nombres";
        //    column.Caption = "cli_nombres";
        //    Prestashop.Columns.Add(column);

        //    column = new DataColumn();
        //    column.DataType = System.Type.GetType("System.String");
        //    column.ColumnName = "cli_apellidos";
        //    column.Caption = "cli_apellidos";
        //    Prestashop.Columns.Add(column);

        //    column = new DataColumn();
        //    column.DataType = System.Type.GetType("System.String");
        //    column.ColumnName = "cli_fec_nac";
        //    column.Caption = "cli_fec_nac";
        //    Prestashop.Columns.Add(column);

        //    column = new DataColumn();
        //    column.DataType = System.Type.GetType("System.String");
        //    column.ColumnName = "cli_email";
        //    column.Caption = "cli_email";
        //    Prestashop.Columns.Add(column);

        //    column = new DataColumn();
        //    column.DataType = System.Type.GetType("System.String");
        //    column.ColumnName = "cli_ubigeo";
        //    column.Caption = "cli_ubigeo";
        //    Prestashop.Columns.Add(column);

        //    column = new DataColumn();
        //    column.DataType = System.Type.GetType("System.String");
        //    column.ColumnName = "cli_direc";
        //    column.Caption = "cli_direc";
        //    Prestashop.Columns.Add(column);

        //    column = new DataColumn();
        //    column.DataType = System.Type.GetType("System.String");
        //    column.ColumnName = "cli_telf";
        //    column.Caption = "cli_telf";
        //    Prestashop.Columns.Add(column);

        //    column = new DataColumn();
        //    column.DataType = System.Type.GetType("System.String");
        //    column.ColumnName = "cli_dni";
        //    column.Caption = "cli_dni";
        //    Prestashop.Columns.Add(column);

        //    column = new DataColumn();
        //    column.DataType = System.Type.GetType("System.Int32");
        //    column.ColumnName = "det_artic";
        //    column.Caption = "det_artic";
        //    Prestashop.Columns.Add(column);

        //    column = new DataColumn();
        //    column.DataType = System.Type.GetType("System.String");
        //    column.ColumnName = "det_artic_ref";
        //    column.Caption = "det_artic_ref";
        //    Prestashop.Columns.Add(column);

        //    column = new DataColumn();
        //    column.DataType = System.Type.GetType("System.String");
        //    column.ColumnName = "det_desc_artic";
        //    column.Caption = "det_desc_artic";
        //    Prestashop.Columns.Add(column);

        //    column = new DataColumn();
        //    column.DataType = System.Type.GetType("System.Int32");
        //    column.ColumnName = "det_cant";
        //    column.Caption = "det_cant";
        //    Prestashop.Columns.Add(column);

        //    column = new DataColumn();
        //    column.DataType = System.Type.GetType("System.Double");
        //    column.ColumnName = "det_prec_sigv";
        //    column.ColumnName = "det_prec_sigv";
        //    Prestashop.Columns.Add(column);

        //    column = new DataColumn();
        //    column.DataType = System.Type.GetType("System.Double");
        //    column.ColumnName = "det_dcto_sigv";
        //    column.ColumnName = "det_dcto_sigv";
        //    Prestashop.Columns.Add(column);

        //    column = new DataColumn();
        //    column.DataType = System.Type.GetType("System.String");
        //    column.ColumnName = "pag_metodo";
        //    column.ColumnName = "pag_metodo";
        //    Prestashop.Columns.Add(column);

        //    column = new DataColumn();
        //    column.DataType = System.Type.GetType("System.String");
        //    column.ColumnName = "pag_nro_trans";
        //    column.ColumnName = "pag_nro_trans";
        //    Prestashop.Columns.Add(column);

        //    column = new DataColumn();
        //    column.DataType = System.Type.GetType("System.String");
        //    column.ColumnName = "pag_nro_tarj";
        //    column.ColumnName = "pag_nro_tarj";
        //    Prestashop.Columns.Add(column);

        //    column = new DataColumn();
        //    column.DataType = System.Type.GetType("System.String");
        //    column.ColumnName = "pag_fecha";
        //    column.ColumnName = "pag_fecha";
        //    Prestashop.Columns.Add(column);

        //    column = new DataColumn();
        //    column.DataType = System.Type.GetType("System.Double");
        //    column.ColumnName = "pag_monto";
        //    column.ColumnName = "pag_monto";
        //    Prestashop.Columns.Add(column);

        //    foreach (order orden in pedidosEcommerce)
        //    {
        //        int idCliente = Convert.ToInt32((orden).id_customer);
        //        cliente = GetCliente(idCliente);

        //        int idDireccion = Convert.ToInt32((orden).id_address_delivery);
        //        direccion_ent = GetDireccion(idDireccion);

        //        int idDireccion2 = Convert.ToInt32((orden).id_address_invoice);
        //        direccion_cli = GetDireccion(idDireccion);

        //        string idPago = Convert.ToString((orden).reference);
        //        pago = GetPayment(idPago);

        //        foreach (order_row detalle in orden.associations.order_rows)
        //        {
        //            row = Prestashop.NewRow();
        //            row["ped_id"] = orden.id;
        //            row["ped_ref"] = orden.reference;
        //            row["ped_fecha"] = orden.date_add;
        //            row["ped_ubigeo_ent"] = direccion_ent.id_state;
        //            row["ped_dir_ent"] = direccion_ent.address1;
        //            row["ped_total_sigv"] = orden.total_paid_tax_excl;
        //            row["ped_total_cigv"] = orden.total_paid_real;
        //            row["ped_dcto_sigv"] = orden.total_discounts_tax_excl;
        //            row["ped_dcto_cigv"] = orden.total_discounts;
        //            row["ped_ship_sigv"] = orden.total_shipping_tax_excl;
        //            row["ped_ship_cigv"] = orden.total_shipping;
        //            row["cli_id"] = orden.id_customer;
        //            row["cli_nombres"] = cliente.firstname;
        //            row["cli_apellidos"] = cliente.lastname;
        //            row["cli_fec_nac"] = cliente.birthday;
        //            row["cli_email"] = cliente.email;
        //            row["cli_ubigeo"] = direccion_cli.id_state;
        //            row["cli_direc"] = direccion_cli.address1;
        //            row["cli_telf"] = direccion_cli.phone;
        //            row["cli_dni"] = direccion_cli.dni;
        //            row["det_artic"] = detalle.product_id;
        //            row["det_artic_ref"] = detalle.product_reference;
        //            row["det_desc_artic"] = detalle.product_name;
        //            row["det_cant"] = detalle.product_quantity;
        //            row["det_prec_sigv"] = detalle.product_price;
        //            row["det_dcto_sigv"] = detalle.unit_price_tax_excl;
        //            row["pag_metodo"] = pago.payment_method;
        //            row["pag_nro_trans"] = pago.transaction_id;
        //            row["pag_nro_tarj"] = pago.card_number;
        //            row["pag_fecha"] = pago.date_add;
        //            row["pag_monto"] = pago.amount;
        //            //row["det_dcto_sigv"] = detalle.unit_price_tax_excl;
        //            Prestashop.Rows.Add(row);
        //        }
        //    }
        //    return Prestashop;

        //}

        public DataTable PrepararPedidos()
        {
            DataTable Prestashop = new DataTable();

            try
            {
                MySqlConnection mysql;
                Conexion oConexionMySql = new Conexion();
                mysql = oConexionMySql.getConexionMySQL();
                mysql.Open();
                MySqlCommand cmd = new MySqlCommand();

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = mysql;
                cmd.CommandText = "USP_LISTA_PEDIDOS";

                //asignar paramentros
                cmd.Parameters.AddWithValue("estado", 2);

                MySqlDataAdapter MySqlData = new MySqlDataAdapter(cmd);
                MySqlData.Fill(Prestashop);

            }
            catch (Exception)
            {
                Prestashop = null;
                throw;
            }
            return Prestashop;

        }

        /// <summary>
        /// Se usa para obtener los registros de Pagos de los Pedidos
        /// </summary>
        /// <returns></returns>
        public DataTable PrepararPedidos_Pagos()
        {
            DataTable Prestashop = new DataTable();

            try
            {
                MySqlConnection mysql;
                Conexion oConexionMySql = new Conexion();
                mysql = oConexionMySql.getConexionMySQL();
                mysql.Open();
                MySqlCommand cmd = new MySqlCommand();

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = mysql;
                cmd.CommandText = "USP_LISTA_PEDIDOS_PAGOS";

                //asignar paramentros
                cmd.Parameters.AddWithValue("estado", 2);

                MySqlDataAdapter MySqlData = new MySqlDataAdapter(cmd);
                MySqlData.Fill(Prestashop);

            }
            catch (Exception)
            {
                Prestashop = null;
                throw;
            }
            return Prestashop;

        }

        public string ImportaDataPrestaShop()
        {
            string _error = "";
            DataTable dtpedidos = null;
            DataTable dtpedidospag = null;
            try
            {
                dtpedidos = PrepararPedidos();
                dtpedidospag = PrepararPedidos_Pagos();

                if (dtpedidos!=null && dtpedidospag != null)
                {
                    Dat_PrestaShop update_psth = new Dat_PrestaShop();
                    

                    /*agrupamos los pedidos*/
                    var grupo_pedido = from item in dtpedidos.AsEnumerable()
                                           //where item.Field<string>("ped_id") == "73" //|| item.Field<Int32>("ped_id") ==40
                                       group item by
                                       new
                                       {
                                           ped_id = Convert.ToInt32(item["ped_id"]),
                                           ped_ref = item["ped_ref"].ToString(),
                                           ped_fecha = Convert.ToDateTime(item["ped_fecha"]),
                                           ped_ubigeo_ent= item["ped_ubigeo_ent"].ToString(),
                                           ped_dir_ent = item["ped_dir_ent"].ToString(),
                                           ped_ref_ent = item["ped_ref_ent"].ToString(),
                                           // Modificado por : Henry Morales - 21/05/2018
                                           // Se agergaron los campos de nombre y telefono de referencia para la entrega
                                           ped_nom_ent = item["ped_nom_ent"].ToString(),
                                           ped_tel_ent = item["ped_tel_ent"].ToString(),
                                           //det_peso =Convert.ToDecimal(item["det_peso"]),
                                           ped_total_sigv = Convert.ToDecimal(item["ped_total_sigv"]),
                                           ped_total_cigv = Convert.ToDecimal(item["ped_total_cigv"]),
                                           ped_dcto_sigv = Convert.ToDecimal(item["ped_dcto_sigv"]),
                                           ped_dcto_cigv = Convert.ToDecimal(item["ped_dcto_cigv"]),
                                           cli_id = item["cli_id"].ToString(),
                                           cli_nombres = item["cli_nombres"].ToString(),
                                           cli_apellidos = item["cli_apellidos"].ToString(),
                                           cli_email = item["cli_email"].ToString(),
                                           cli_direc = item["cli_direc"].ToString(),
                                           cli_telf = item["cli_telf"].ToString(),
                                           cli_telf_mov = item["cli_telf_mov"].ToString(),
                                           cli_dni = item["cli_dni"].ToString(),
                                           cli_ubigeo = item["cli_ubigeo"].ToString(),
                                           ped_ship_sigv = Convert.ToDecimal(item["ped_ship_sigv"]),
                                           // Modificado por : Henry Morales - 19/06/2018
                                           // Se modificó para tomar los pagos en diferentes formas de pago (DataTable dtpedidospag)
                                           /*pag_metodo = item["pag_metodo"].ToString(),
                                           pag_nro_trans = item["pag_nro_trans"].ToString(),
                                           pag_fecha = Convert.ToDateTime(item["pag_fecha"]),
                                           pag_monto = Convert.ToDecimal(item["pag_monto"]),*/
                                       }
                                       into G
                                       select new
                                       {
                                           pedido = G.Key.ped_id,
                                           ped_ref = G.Key.ped_ref,
                                           ped_fecha = G.Key.ped_fecha,
                                           ped_ubigeo_ent=G.Key.ped_ubigeo_ent,
                                           ped_dir_ent = G.Key.ped_dir_ent,
                                           ped_ref_ent = G.Key.ped_ref_ent,
                                           // Modificado por : Henry Morales - 21/05/2018
                                           // Se agergaron los campos de nombre y telefono de referencia para la entrega
                                           ped_nom_ent = G.Key.ped_nom_ent,
                                           ped_tel_ent = G.Key.ped_tel_ent,
                                           cli_telf_mov =G.Key.cli_telf_mov,
                                           //det_peso=G.Key.det_peso,
                                           ped_total_sigv = G.Key.ped_total_sigv,
                                           ped_total_cigv = G.Key.ped_total_cigv,
                                           ped_dcto_sigv = G.Key.ped_dcto_sigv,
                                           ped_dcto_cigv = G.Key.ped_dcto_cigv,
                                           cli_id = G.Key.cli_id,
                                           cli_nombres = G.Key.cli_nombres,
                                           cli_apellidos = G.Key.cli_apellidos,
                                           cli_email = G.Key.cli_email,
                                           cli_direc = G.Key.cli_direc,
                                           cli_telef = G.Key.cli_telf,
                                           cli_dni = G.Key.cli_dni,
                                           cli_ubigeo = G.Key.cli_ubigeo,
                                           ped_ship_sigv = G.Key.ped_ship_sigv,
                                           // Modificado por : Henry Morales - 19/06/2018
                                           // Se modificó para tomar los pagos en diferentes formas de pago (DataTable dtpedidospag)
                                           /*pag_metodo = G.Key.pag_metodo,
                                           pag_nro_trans = G.Key.pag_nro_trans,
                                           pag_fecha = G.Key.pag_fecha,
                                           pag_monto = G.Key.pag_monto*/
                                       };
                    

                    /*recorremos los pedidos para agregar al pedido*/
                    foreach (var key in grupo_pedido)
                    {

                        /*verifica si pedido existe*/
                        Boolean _existe = update_psth.Existe_Pedido_Prestashop(key.pedido.ToString());
                        if (!_existe)
                        {                       
                        /*capturamos el detalle */
                        var ped_det = from item in dtpedidos.AsEnumerable()
                                      where  item.Field<string>("ped_id") ==Convert.ToString(key.pedido)
                                      //&& item.Field<string>("det_artic_ref").Length == 11
                                      select new
                                      {
                                          det_artic_ref = item["det_artic_ref"].ToString(),
                                          det_desc_artic = Convert.ToString(item["det_desc_artic"]),
                                          det_cant = Convert.ToInt32(item["det_cant"]),
                                          det_prec_sigv = Convert.ToDecimal(item["det_prec_sigv"]),
                                          det_peso = Convert.ToDecimal(item["det_peso"]),
                                          //det_prec_cigv = Convert.ToDecimal(item["det_prec_cigv"]),
                                          det_dcto_sigv = Convert.ToDecimal(item["det_dcto_sigv"]),
                                       

                                      };
                        /*Recorremos el detalle*/
                        List<Order_Dtl> items_det = new List<Order_Dtl>();



                        Decimal _tot_peso = 0;
                        foreach (var key_det in ped_det)
                        {
                            Order_Dtl dtl = new Order_Dtl();
                            string articulo_talla = key_det.det_artic_ref.ToString().Trim().Replace("-", "");
                            string articulo = articulo_talla.Substring(0, articulo_talla.Length - 2);
                            string talla = articulo_talla.Substring(articulo_talla.Length - 2, 2);

                            dtl._code = articulo;
                            dtl._size = talla;
                            dtl._qty = Convert.ToInt32(key_det.det_cant);
                            dtl._priceigv = key_det.det_prec_sigv;
                            dtl._price = Convert.ToDecimal(Math.Round(Convert.ToDouble(key_det.det_prec_sigv), 2, MidpointRounding.AwayFromZero));
                            dtl._commissionPctg = 0;
                            dtl._commissionigv = 0;
                            dtl._det_dcto_sigv = Math.Round(key_det.det_dcto_sigv, 2, MidpointRounding.AwayFromZero);
                            dtl._commission = Convert.ToDecimal(Math.Round(Convert.ToDouble((dtl._det_dcto_sigv * dtl._qty)), 2, MidpointRounding.AwayFromZero));
                            dtl._ofe_porc = 0;
                            dtl._dsctoVale = 0;
                            dtl._ofe_id = 0;
                            dtl._art_des = key_det.det_desc_artic;
                            dtl._art_peso = key_det.det_peso;
                           _tot_peso += key_det.det_peso * key_det.det_cant;
                            items_det.Add(dtl);                            
                        }

                            #region <AJUSTE DESCUENTO>
                            Decimal ajuste_subtotal = key.ped_total_sigv;
                            var subtot = items_det.Sum(a => (a._price * a._qty) - a._commission);

                            subtot += key.ped_ship_sigv;

                            Decimal saldo = ajuste_subtotal - subtot;

                            if (saldo!=0)
                            {
                                

                                if (items_det[0]._commission!=0)
                                {
                                    items_det[0]._commission = items_det[0]._commission - saldo;
                                }
                                else
                                {
                                    Int32 item_ult = items_det.Count() - 1;
                                    items_det[item_ult]._price = items_det[item_ult]._price + saldo;
                                }

                                // ¨****** Verificar
                                // Cambiar si el Saldo es diferente de 0,sumarlo al Pet_Det_Precio (dtl._price) 
                            }

                            #endregion


                            /*si esta lleno el list entonces agregamos el pedido en este ,metodo*/
                            if (items_det.Count > 0)
                            {
                                /*datos del cliente*/
                                Cliente cl = new Cliente();
                                cl.cli_nombres = key.cli_nombres;
                                cl.cli_apellidos = key.cli_apellidos;
                                cl.cli_email = key.cli_email;
                                cl.cli_ubigeo = key.cli_ubigeo;
                                cl.cli_direc = key.cli_direc;
                                cl.cli_telf = key.cli_telef;
                                cl.cli_telf_mov = key.cli_telf_mov;
                                cl.cli_dni = key.cli_dni;
                                /*********************/
                                /*metodo de pago*/
                                Pagos pg = new Pagos();
                                // Modificado por : Henry Morales - 19/06/2018
                                // Se modificó para tomar los pagos en diferentes formas de pago (DataTable dtpedidospag)
                                /*pg.pag_metodo = key.pag_metodo;
                                pg.pag_nro_trans = key.pag_nro_trans;
                                pg.pag_fecha = key.pag_fecha;
                                pg.pag_monto = key.pag_monto;*/
                                DataTable pago_ped = new DataTable();
                                pago_ped = dtpedidospag.Clone();
                                pago_ped.Clear();

                                foreach (DataRow row in dtpedidospag.Rows)
                                {
                                    if(row["ped_id"].ToString() == key.pedido.ToString())
                                    {
                                        pago_ped.ImportRow(row);
                                    }
                                }
                                /**/

                                decimal igv_monto = key.ped_dcto_cigv - key.ped_dcto_sigv;
                                //string[] pedido_update=

                                // Modificado por : Henry Morales - 19/06/2018
                                // Se agergo la tabla dtpedidospag, para enviar la información de diferentes formas de pago
                                // Modificado por : Henry Morales - 21/05/2018
                                // Se agergaron los campos de nombre y telefono de referencia para la entrega ( key.ped_nom_ent ; key.ped_tel_ent)
                                string[] result= update_psth.Update_Pedido_Prestashop(Ent_Global._bas_id_codigo, 9219, "", 0, 0, "", "", items_det, 0, 1, "", "", 0, 0, "", "", 0, null,
                                              false, 0, null, key.pedido, key.ped_ref, key.ped_ship_sigv, cl, pg, key.ped_fecha, key.ped_total_cigv,key.ped_ubigeo_ent,
                                              key.ped_dir_ent,key.ped_ref_ent, key.ped_nom_ent, key.ped_tel_ent, _tot_peso, pago_ped);
                            if (result[0].ToString()=="-1")
                            {
                                    _error += result[1].ToString();
                            }

                        }
                     }
                    }
                }

            }
            catch (Exception exc)
            {
                _error=exc.Message;
            }
            return _error;
        }
       
        #endregion
    }
}
